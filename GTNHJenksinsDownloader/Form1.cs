using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;

namespace GTNHJenksinsDownloader
{
    public partial class MainForm : Form
    {
        DateTime LastUpdate = DateTime.Now;

        SettingsForm settingsform = new SettingsForm();
        private System.Threading.Timer timer;

        bool closing = false;
        bool afterupgrade = false;

        class downloadstatus_t
        {
            public string filename = "";
            public int status = 0;
        }
        downloadstatus_t downloadstatus = new downloadstatus_t();

        class modsList_t
        {
            public long lastbuildtime = 0;
            public string name = "";
            public string filename = "";
            public string localfilename = "";
            public string downloadurl = "";
            public string hash = "";
        }

        class modfile {
            public string path = "";
            public string filename = "";
            public string hash = "";
        }

        List<modsList_t> clientmodlist = new List<modsList_t>();
        List<modsList_t> servermodlist = new List<modsList_t>();


        public MainForm()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsform.ShowDialog();
            UpdateSettings();
        }


        public void updateStatusLabel(bool invoke, string newstatus, Color color, int progress = -1)
        {
            Action act = () => {
                statuslabel.Text = newstatus;
                statuslabel.ForeColor = color;
                if (progress != -1)
                {
                    statuspercentage.Visible = true;
                    statuspercentage.Value = progress;
                }
                else
                    statuspercentage.Visible = false;
                
            };
            if (invoke)
                Invoke(act);
            else
                act();
        }

        public void updateTotalStatusLabel(bool invoke, string newstatus2, Color color, int progress2 = -1)
        {
            Action act = () =>
            {
                if (newstatus2 != "")
                {
                    totalstatuslabel.Visible = true;
                    totalstatuslabel.Text = newstatus2;
                    totalstatuslabel.ForeColor = color;
                }
                else
                    totalstatuslabel.Visible = false;
                if (progress2 != -1)
                {
                    totalstatusbar.Visible = true;
                    totalstatusbar.Value = progress2;
                }
                else
                    totalstatusbar.Visible = false;
            };
            if (invoke)
                Invoke(act);
            else
                act();
        }

        bool updatinginprogress = false;

        private void Updatebutton_Click(object sender, EventArgs e)
        {

            if (updatinginprogress)
                return;
            updatinginprogress = true;

            new Thread(()=> {
#if !DEBUG
            try
            {
#endif
                updateModsList();
                LastUpdate = DateTime.Now;
                Invoke((Action)(() => UpdateSettings()));
                updatinginprogress = false;

#if !DEBUG
            }
            catch
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                updatinginprogress = false;
            }
#endif
            }).Start();

        }

        private modfile[] getModList(string[] pathtomodfile)
        {
            double progress = 0d;
            double progressperoperation = 50d / (double)pathtomodfile.Length;
            modfile[] modfiles = new modfile[pathtomodfile.Length];
            int i = 0;
            Console.WriteLine("Building local mod list");
            foreach (string pathtomod in pathtomodfile)
            {
                updateStatusLabel(true, "Building mods list: " + Path.GetFileName(pathtomod), Color.Black, (int)progress);
                modfile modfile = new modfile();
                modfile.path = pathtomod;
                modfile.hash = Utility.calculateMD5(pathtomod);
                modfile.filename = Path.GetFileName(pathtomod);
                modfiles[i++] = modfile;
                progress += progressperoperation;
                Console.WriteLine(modfile.filename + " - " + modfile.hash);
            }
            Console.WriteLine("Local mod list done");
            return modfiles;
        }

        List<string> git = new List<string>();

        private void updateModsList()
        {
            List<modsList_t> clientupdates = null;
            List<modsList_t> serverupdates = null;
            updateStatusLabel(true, "Downloading jenkins list", Color.Black);
            //string a = Utility.Get(Settings.options.jenkinspath + "api/json?tree=jobs[name,lastSuccessfulBuild[result,timestamp,artifacts[fileName,relativePath],url,fingerprint[hash]],url]");
            string a = Utility.Get(Settings.options.jenkinspath + "api/json?tree=jobs[name,lastSuccessfulBuild[actions[buildsByBranchName[*],remoteUrls[*]],result,artifacts[fileName,relativePath],url],url]");
            int p = 1;

            if(git.Count == 0)
                while(true)
                {
                    string gitresponse;
                    try
                    {
                        gitresponse = Utility.Get("https://api.github.com/orgs/GTNewHorizons/repos?page=" + p.ToString() + "&per_page=100");
                    }
                    catch(WebException ex)
                    {
                        if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Forbidden)
                            MessageBox.Show("Exceeded limit on github api requests ! Please try again in 1 hour", Settings.appname, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Error", Settings.appname, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        git.Clear();
                        return;
                    }
                    catch
                    {
                        MessageBox.Show("Error", Settings.appname, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        git.Clear();
                        return;
                    }
                    
                    if (((Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(gitresponse)).Count == 0)
                        break;
                    git.Add(gitresponse);
                    p++;
                }
            
            int maxprogress = 100;
            if (Settings.options.client && Settings.options.server)
                maxprogress = 75;
            if (Settings.options.client)
            {
                Console.WriteLine("Updating client mod list");
                string[] clientmods = Directory.GetFiles(Settings.options.modpath);
                clientupdates = UpdateModsList(clientmodslistview, getModList(clientmods), a, git, ref clientmodlist, 50, maxprogress);
            }
            if (Settings.options.server)
            {
                Console.WriteLine("Updating server mod list");
                string[] servermods = Directory.GetFiles(Settings.options.servermodpath);
                serverupdates = UpdateModsList(servermodslistview, getModList(servermods), a, git, ref servermodlist, (maxprogress == 75 ? 75 : 50), 100);
            }
            
            List<modsList_t> updates = new List<modsList_t>();
            if (clientupdates != null)
                foreach (modsList_t up in clientupdates)
                    updates.Add(up);
            if (clientupdates == null && serverupdates != null)
                foreach (modsList_t up in serverupdates)
                    updates.Add(up);
            if (clientupdates != null && serverupdates != null)
                foreach (modsList_t up in serverupdates)
                    if (!updates.Exists((modsList_t arg) => { return arg.name == up.name; }))
                        updates.Add(up);
            Invoke((Action)(()=>
            {
                if (updates.Count > 0)
                {
                    string ballontext = "New updates for " + updates.Count.ToString() + " mods: \n";
                    foreach (modsList_t up in updates)
                    {
                        ballontext += up.name + ", ";
                    }
                    notifyIcon1.BalloonTipText = ballontext;
                    notifyIcon1.BalloonTipTitle = "New updates detected !";
                    notifyIcon1.ShowBalloonTip(int.MaxValue);
                }
                upgrademods.Enabled = true;
                label1.Visible = true;
            }));

        }

        private List<modsList_t> UpdateModsList(ListView LW, modfile[] mymods, string a, List<string> git, ref List<modsList_t> list, int actualprogress, int maxprogress)
        {

            List<modsList_t> updates = new List<modsList_t>();
            List<string> names = new List<string>();

            Invoke((Action)(() => LW.Items.Clear()));
            dynamic parsed = JsonConvert.DeserializeObject(a);
            List<Newtonsoft.Json.Linq.JArray> gits = new List<Newtonsoft.Json.Linq.JArray>();
            foreach (string item in git)
                gits.Add((Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(item));

            Newtonsoft.Json.Linq.JArray jobs = parsed.jobs;

            double progress = (double)actualprogress;
            double progressperoperation = ((double)maxprogress - progress) / jobs.Count;
            Console.WriteLine("Searching mods in jenkins list");
            for (int i = 0; i < jobs.Count; i++)
            {
                updateStatusLabel(true, "Updating list", Color.Black, (int)progress);
                progress += progressperoperation;
                if (jobs[i]["lastSuccessfulBuild"].HasValues)
                {
                    bool found = false;
                    modfile mymod = null;
                    foreach (modfile l in mymods)
                        if (Path.GetExtension(l.path).ToLower() == ".jar" && Utility.checkifsamemod((string)jobs[i]["lastSuccessfulBuild"]["artifacts"][0]["fileName"], l.filename))
                        {
                            Console.WriteLine("Found " + l.filename + " on jenkins");
                            mymod = l;
                            found = true;
                            break;
                        }
                    if (found)
                    {
                        //check for hash instead of time

                        //determine latest default build

                        string defaultbranch = "";

                        Console.WriteLine("Searching for default branch");

                        int builddatai = 0;

                        for (int j = 0; j < 4; j++)
                            if ((string)jobs[i]["lastSuccessfulBuild"]["actions"][j]["_class"] == "hudson.plugins.git.util.BuildData")
                            {
                                builddatai = j;
                                break;
                            }
                        {
                            bool outerbreak = false;
                            for (int x = 0; x < gits.Count; x++)
                            {
                                for (int y = 0; y < gits[x].Count; y++)
                                {
                                    //Console.WriteLine((string)gits[j]["clone_url"]);
                                    //Console.WriteLine((string)jobs[i]["lastSuccessfulBuild"]["actions"][2]["remoteUrls"][0]);

                                    if ((string)gits[x][y]["clone_url"] == (string)jobs[i]["lastSuccessfulBuild"]["actions"][builddatai]["remoteUrls"][0])
                                    {
                                        defaultbranch = (string)gits[x][y]["default_branch"];
                                        Console.WriteLine("Default branch in git found: " + defaultbranch);
                                        outerbreak = true;
                                        break;
                                    }
                                }
                                if (outerbreak)
                                    break;
                            }
                        }
                        if(defaultbranch == "")
                        {
                            Console.WriteLine("Cannot find default branch in git, skipping");
                            continue;
                        }

                        //Newtonsoft.Json.Linq.JArray builds = ((dynamic)JsonConvert.DeserializeObject(Utility.Get((string)jobs[i]["url"] + "api/json?tree=builds[fingerprint[hash]]"))).builds;
                        int latestbuildnumber = -1;
                        var o = jobs[i]["lastSuccessfulBuild"]["actions"][builddatai]["buildsByBranchName"]["origin/" + defaultbranch];
                        var o2 = jobs[i]["lastSuccessfulBuild"]["actions"][builddatai]["buildsByBranchName"]["refs/remotes/origin/" + defaultbranch];
                        if (o == null && o2 == null)
                        {
                            Console.WriteLine("Cannot find default branch in jenkins, skipping");
                            continue;
                        }
                        if (o != null)
                            latestbuildnumber = (int)o["buildNumber"];
                        if (o2 != null)
                        {
                            int ln = (int)o2["buildNumber"];
                            if (ln > latestbuildnumber)
                                latestbuildnumber = ln;
                        }
                        Console.WriteLine("Found latest build number: " + latestbuildnumber);
                        dynamic lastbuild;

                        try
                        {
                            lastbuild = JsonConvert.DeserializeObject(Utility.Get((string)jobs[i]["url"] + latestbuildnumber.ToString() + "/api/json?tree=fingerprint[hash],timestamp,artifacts[fileName,relativePath],url"));
                        }
                        catch(WebException ex)
                        {
                            if(((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                            {
                                Console.WriteLine("Latest build on default branch responded 404 on jenkins, skipping");
                                continue;
                            }
                            throw;
                        }
                        bool newversion = true;
                        foreach (var fg in (Newtonsoft.Json.Linq.JArray)lastbuild["fingerprint"])
                        {
                            if ((string)fg["hash"] == mymod.hash)
                            {
                                newversion = false;
                                break;
                            }
                        }
                        if (!newversion)
                        {
                            Console.WriteLine("Newest version installed, skipping");
                            continue;
                        }

                        Console.WriteLine("Newer version found");

                        long lastbuildtime = (long)lastbuild["timestamp"];
                        DateTime time1 = Utility.UnixTimeStampToDateTime(lastbuildtime / 1000);
                        DateTime time2 = File.GetLastWriteTime(mymod.path);
                        double diff = (time1 - time2).TotalMinutes;
                        //if (Math.Abs(diff) < 1d)
                        //    continue; // same file
                        //if (diff < 0)
                        //    continue; // we have newer file ??
                        ListViewItem item = new ListViewItem();
                        item.Text = (string)jobs[i]["name"];
                        item.SubItems.Add(time1.ToString());
                        item.SubItems.Add(time2.ToString());
                        int artifact = 0;
                        Newtonsoft.Json.Linq.JArray artifacts = (Newtonsoft.Json.Linq.JArray)lastbuild["artifacts"];
                        Newtonsoft.Json.Linq.JArray fingerprint = (Newtonsoft.Json.Linq.JArray)lastbuild["fingerprint"];
                        if (artifacts.Count > 1)
                        {
                            Console.WriteLine("Detected more than one artifact. Searching for the right one");
                            artifact = -1;
                            for (int j = 0; j < artifacts.Count; j++)
                                if (Utility.trimnumbers((string)artifacts[j]["fileName"]).ToLower().Length == Utility.trimnumbers(mymod.filename).ToLower().Length)
                                {
                                    Console.WriteLine("Found artifact: " + (string)artifacts[j]["fileName"]);
                                    artifact = j;
                                    break;
                                }
                            if(artifact == -1)
                            {
                                Console.WriteLine("Can't find artifact through name, searching for hash instead");
                                //MessageBox.Show(mymod.filename);
                                //search for file
                                Newtonsoft.Json.Linq.JArray builds = ((dynamic)JsonConvert.DeserializeObject(Utility.Get((string)jobs[i]["url"] + "api/json?tree=builds[fingerprint[hash]]"))).builds;
                                foreach (var build in builds)
                                {
                                    Newtonsoft.Json.Linq.JArray fg = (Newtonsoft.Json.Linq.JArray)build["fingerprint"];
                                    for (int j = 0; j < fg.Count; j++)
                                        if((string)fg[j]["hash"] == mymod.hash)
                                        {
                                            Console.WriteLine("Found artifact: " + artifacts[j]["fileName"]);
                                            artifact = j;
                                            break;
                                        }
                                    if (artifact != -1)
                                        break;
                                }
                            }
                            if (artifact == -1)
                            {
                                Console.WriteLine("Can't find artifact ! Skipping mod");
                                continue; // cant find right mod
                            }
                        }
                        if (!Settings.options.blacklist.Contains(item.Text))
                        {
                            int index = list.FindIndex((modsList_t arg) => { return arg.name == item.Text; });
                            if (index != -1)
                            {// update
                                list[index].filename = Path.Combine(Path.GetDirectoryName(mymod.path), (string)artifacts[artifact]["fileName"]);
                                list[index].downloadurl = (string)lastbuild["url"] + "artifact/" + (string)artifacts[artifact]["relativePath"];
                                list[index].hash = (string)fingerprint[artifact]["hash"];
                                if (list[index].lastbuildtime != lastbuildtime)
                                    updates.Add(list[index]);
                                list[index].lastbuildtime = lastbuildtime;
                            }
                            else if (index == -1)
                            {// update also
                                modsList_t mod = new modsList_t();
                                mod.name = item.Text;
                                mod.localfilename = mymod.path;
                                mod.lastbuildtime = lastbuildtime;
                                mod.filename = Path.Combine(Path.GetDirectoryName(mymod.path), (string)artifacts[artifact]["fileName"]);
                                mod.downloadurl = (string)lastbuild["url"] + "artifact/" + (string)artifacts[artifact]["relativePath"];
                                mod.hash = (string)fingerprint[artifact]["hash"];
                                list.Add(mod);
                                updates.Add(mod);
                            }
                            item.Checked = true;
                        }
                        else
                        {
                            Console.WriteLine("Mod is blacklisted");
                            item.BackColor = Color.Gray;
                            item.ForeColor = Color.DarkRed;
                        }
                        if(item.ForeColor != Color.DarkRed)
                            names.Add(item.Text);
                        Invoke((Action)(() => { LW.Items.Add(item); }));
                    }
                }
            }

            for (int i = 0; i < list.Count; i++)
                while (true)
                    if (!names.Contains(list[i].name))
                        list.RemoveAt(i);
                    else
                        break;

            return updates;
        }

        private void SetUpTimer(TimeSpan alertTime)
        {
            if (timer != null) timer.Dispose();
            DateTime current = DateTime.Now;
            TimeSpan timeToGo = alertTime - current.TimeOfDay;
            if (timeToGo < TimeSpan.Zero)
                timertick();
            timer = new System.Threading.Timer(x => timertick(), null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        private void timertick()
        {
            Invoke((Action)(() => { LastUpdate = DateTime.Now; Updatebutton_Click(null, null); }));
        }

        

        

        private void MainForm_Load(object sender, EventArgs e)
        {
            //GTNewHorizonsCoreMod-1.7.10-1.8.03.jar == GTNewHorizonsCoreMod-1.7.10-1.8.00.jar
            //MessageBox.Show(trimnumbers("GTNewHorizonsCoreMod-1.7.10-1.8.03.jar") + " == " + trimnumbers("GTNewHorizonsCoreMod-1.7.10-1.8.00.jar"));

            Settings.Load();

            Logger.Init();

            versionlabel.Text = Settings.version;

            if (!Directory.Exists(Settings.backupdirectory))
                Directory.CreateDirectory(Settings.backupdirectory);

            UpdateSettings();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closing = true;
            Application.Exit();
        }


        public void UpdateSettings()
        {
            if (Settings.options.client)
            {
                serverupdates.Location = new Point(665, 60);
                clientupdates.Visible = true;
            }
            else
            {
                serverupdates.Location = new Point(12, 60);
                clientupdates.Visible = false;
            }
            serverupdates.Visible = Settings.options.server;
            Size newsize;
            if (Settings.options.client && Settings.options.server)
                newsize = new Size(1339, 655);
            else if (Settings.options.client || Settings.options.server)
                newsize = new Size(685, 655);
            else
                newsize = new Size(685, 123);
            if(afterupgrade)
            {
                label2.Visible = true;
                backup.Visible = true;
                newsize.Height = 692;
            }
            if(Size != newsize)
            {
                Point p = new Point((Size.Width - newsize.Width) / 2, (Size.Height - newsize.Height) / 2);
                Size = newsize;
                Location = new Point(Location.X + p.X, Location.Y + p.Y);
            }
            if (Settings.options.client)
                if (!Directory.Exists(Settings.options.modpath))
                {
                    Updatebutton.Enabled = false;
                    upgrademods.Enabled = false;
                    updateStatusLabel(false, "Invalid client path", Color.Red);
                    return;
                }
            if(Settings.options.server)
                if(!Directory.Exists(Settings.options.servermodpath))
                {
                    Updatebutton.Enabled = false;
                    upgrademods.Enabled = false;
                    updateStatusLabel(false, "Invalid server path", Color.Red);
                    return;
                }
            if(!Settings.options.server && !Settings.options.client)
            {
                Updatebutton.Enabled = false;
                upgrademods.Enabled = false;
                updateStatusLabel(false, "Automatic list checking disabled", Color.Blue);
                return;
            }
            Updatebutton.Enabled = true;
            if (!Settings.options.autoupdates)
            {
                if (timer != null) timer.Dispose();
                updateStatusLabel(false, "Automatic list checking disabled", Color.Black);
            }
            else if(Settings.options.autoupdateinterval == AutoUpdateSelector.onstartup)
            {
                if (timer != null) timer.Dispose();
                updateStatusLabel(false, "Next auto list checking on next startup", Color.Black);
            }
            else if (Settings.options.autoupdateinterval == AutoUpdateSelector.everyhour)
            {
                SetUpTimer(LastUpdate.AddHours(1).TimeOfDay);
                updateStatusLabel(false, "Next auto list checking " + LastUpdate.AddHours(1).ToString(), Color.Black);
            }
        }

        private void upgrademods_Click(object sender, EventArgs e)
        {
            if((Settings.options.client && clientmodlist.Count > 0) || (Settings.options.server &&  servermodlist.Count > 0))
            {
                if (timer != null) timer.Dispose();
                Updatebutton.Enabled = false;
                upgrademods.Enabled = false;
                ControlBox = false;
                quitToolStripMenuItem.Enabled = false;
                fileToolStripMenuItem.Enabled = false;
                statuspercentage.Visible = true;
                totalstatusbar.Visible = true;
                totalstatuslabel.Visible = true;

                foreach (ListViewItem item in clientmodslistview.Items)
                {
                    if(!item.Checked && !Settings.options.blacklist.Contains(item.Text))
                    {
                        int index = clientmodlist.FindIndex((modsList_t arg)=> { return arg.name == item.Text; });
                        if (index != -1)
                            clientmodlist.RemoveAt(index);
                    }
                }
                foreach (ListViewItem item in servermodslistview.Items)
                {
                    if (!item.Checked && !Settings.options.blacklist.Contains(item.Text))
                    {
                        int index = servermodlist.FindIndex((modsList_t arg) => { return arg.name == item.Text; });
                        if (index != -1)
                            servermodlist.RemoveAt(index);
                    }
                }

                new Thread(()=> {
                    backupcreator clientcreator = new backupcreator();
                    backupcreator servercreator = new backupcreator();


                    string temppath = Path.Combine(Path.GetTempPath(), "GTNHJenkinsDownloader\\");
                    if (!Directory.Exists(temppath)) Directory.CreateDirectory(temppath);
                    string modfile = Path.Combine(temppath, "mod.jar");
                    if (File.Exists(modfile)) File.Delete(modfile);

                    List<modsList_t> failed = new List<modsList_t>();

                    if (Settings.options.server && servermodlist.Count > 0)
                        servercreator.Begin();

                    if (Settings.options.client && clientmodlist.Count > 0)
                    {
                        Console.WriteLine("Upgrading client mods");
                        clientcreator.Begin();
                        int counter = 0;
                        foreach (modsList_t mod in clientmodlist)
                        {
                            int percent = (int)(((double)counter / (double)clientmodlist.Count) * 100d);
                            updateTotalStatusLabel(true, percent.ToString() + "%", Color.Black, percent);
                            counter++;
                            bool faileddownloading = false;
                            Console.WriteLine("Upgrading " + mod.name);
                            while (true)
                            {
                                downloadstatus.filename = mod.name;
                                downloadstatus.status = 0;
                                DownloadMOD(mod.downloadurl, modfile);
                                Console.WriteLine("Downloading " + mod.downloadurl + " as " + modfile);
                                while (downloadstatus.status == 0)
                                    Thread.Sleep(200);
                                if (downloadstatus.status == -1)
                                {
                                    Console.WriteLine("Download failed ! Skipping");
                                    faileddownloading = true;
                                    failed.Add(mod);
                                    break;
                                }
                                if (Utility.calculateMD5(modfile) == mod.hash)
                                    break;
                                Console.WriteLine("Downloaded file hash doesn't match ! Repeating");
                            }
                            if (faileddownloading)
                                continue;

                            Console.WriteLine("Download success");

                            clientcreator.Add(mod.localfilename);

                            File.Delete(mod.localfilename);
                            File.Copy(modfile, mod.filename);
                            DateTime time = Utility.UnixTimeStampToDateTime(mod.lastbuildtime / 1000);
                            File.SetCreationTime(mod.filename, time);
                            File.SetLastWriteTime(mod.filename, time);

                            Console.WriteLine("Replaced mod: " + Path.GetFileName(mod.localfilename) + " -> " + Path.GetFileName(mod.filename));

                            if (Settings.options.server && servermodlist.Count > 0)
                            {
                                Console.WriteLine("Checking if the same mod appears in server list");
                                int index = servermodlist.FindIndex((modsList_t arg) => { return arg.name == mod.name; });
                                if (index != -1)
                                {
                                    Console.WriteLine("Found");
                                    servercreator.Add(servermodlist[index].localfilename);

                                    File.Delete(servermodlist[index].localfilename);
                                    File.Copy(modfile, servermodlist[index].filename);
                                    File.SetCreationTime(servermodlist[index].filename, time);
                                    File.SetLastWriteTime(servermodlist[index].filename, time);
                                    servermodlist.RemoveAt(index);
                                    Console.WriteLine("Replaced server mod: " + Path.GetFileName(servermodlist[index].localfilename) + " -> " + Path.GetFileName(mod.filename));
                                }
                            }
                            if (File.Exists(modfile)) File.Delete(modfile);
                        }
                        if (failed.Count > 0)
                        {
                            for (int i = 0; i < clientmodlist.Count; i++)
                            {
                                modsList_t mod = clientmodlist[i];
                                if (!failed.Exists((modsList_t arg) => { return arg.name == mod.name; }))
                                {
                                    clientmodlist.RemoveAt(i);
                                    i--;
                                }
                            }
                            failed.Clear();
                        }
                        else
                            clientmodlist.Clear();
                    }
                    clientcreator.End(backupcreator.backuptype.TYPE_CLIENT);

                    if (Settings.options.server && servermodlist.Count > 0)
                    {
                        int counter = 0;
                        foreach (modsList_t mod in servermodlist)
                        {
                            int percent = (int)(((double)counter / (double)servermodlist.Count) * 100d);
                            updateTotalStatusLabel(true, percent.ToString() + "%", Color.Black, percent);
                            bool faileddownloading = false;
                            Console.WriteLine("Upgrading " + mod.name);
                            while (true)
                            {
                                downloadstatus.filename = mod.name;
                                downloadstatus.status = 0;
                                DownloadMOD(mod.downloadurl, modfile);
                                Console.WriteLine("Downloading " + mod.downloadurl + " as " + modfile);
                                while (downloadstatus.status == 0)
                                    Thread.Sleep(200);
                                if (downloadstatus.status == -1)
                                {
                                    Console.WriteLine("Download failed ! Skipping");
                                    faileddownloading = true;
                                    failed.Add(mod);
                                    break;
                                }
                                if (Utility.calculateMD5(modfile) == mod.hash)
                                    break;
                                Console.WriteLine("Downloaded file hash doesn't match ! Repeating");
                            }
                            if (faileddownloading)
                                continue;

                            Console.WriteLine("Download success");

                            servercreator.Add(mod.localfilename);

                            File.Delete(mod.localfilename);
                            File.Copy(modfile, mod.filename);
                            DateTime time = Utility.UnixTimeStampToDateTime(mod.lastbuildtime / 1000);
                            File.SetCreationTime(mod.filename, time);
                            File.SetLastWriteTime(mod.filename, time);
                            if (File.Exists(modfile)) File.Delete(modfile);

                            Console.WriteLine("Replaced mod: " + Path.GetFileName(mod.localfilename) + " -> " + Path.GetFileName(mod.filename));
                        }
                        
                        if (failed.Count > 0)
                        {
                            for (int i = 0; i < servermodlist.Count; i++)
                            {
                                modsList_t mod = servermodlist[i];
                                if (!failed.Exists((modsList_t arg) => { return arg.name == mod.name; }))
                                {
                                    servermodlist.RemoveAt(i);
                                    i--;
                                }
                            }
                            failed.Clear();
                        }
                        else
                            servermodlist.Clear();
                    }
                    servercreator.End(backupcreator.backuptype.TYPE_SERVER);

                    afterupgrade = true;
                    Invoke((Action)(()=> {
                        
                        Updatebutton.Enabled = true;
                        upgrademods.Enabled = false;
                        ControlBox = true;
                        quitToolStripMenuItem.Enabled = true;
                        fileToolStripMenuItem.Enabled = true;
                        statuspercentage.Visible = false;
                        totalstatusbar.Visible = false;
                        totalstatuslabel.Visible = false;

                        Updatebutton_Click(null, null);

                        UpdateSettings();

                        MessageBox.Show("Update has been done\nList has been refreshed\nIf there is any mod appeard again then it has failed", "GTNH Jenkins Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));


                }).Start();
            }
        }

        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
#if DEBUG
            return; // dont hide to tray when debugging
#endif
            if (closing || Settings.options.quitonclose)
                return;
            e.Cancel = true;
            notifyIcon1.Visible = true;
            Hide();
            if(!Settings.options.nevershowtrayhide)
            {
                DialogResult dr = MessageBox.Show("Hidden to tray, do you want to never see this message again ??", "GTNH Jenkins Downloader", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    Settings.options.nevershowtrayhide = true;
                    Settings.Save();
                }
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
        }

        private void hideToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            Hide();
            if(!Settings.options.nevershowtrayhide)
            {
                DialogResult dr = MessageBox.Show("Hidden to tray, do you want to never see this message again ??", "GTNH Jenkins Downloader", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    Settings.options.nevershowtrayhide = true;
                    Settings.Save();
                }
            }
        }

        private void afterload_Tick(object sender, EventArgs e)
        {
            afterload.Enabled = false;
            if (Settings.options.startintray)
            {
                notifyIcon1.Visible = true; Hide();
            }
            if (Settings.options.autoupdates)
                Updatebutton_Click(null, null);
            Opacity = 1d;
        }

        public void DownloadMOD(string address, string location)
        {
            WebClient client = new WebClient();
            Uri Uri = new Uri(address);

            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);
            client.DownloadFileAsync(Uri, location);
        }

        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            updateStatusLabel(true, "Downloading " + downloadstatus.filename + " " + e.ProgressPercentage.ToString() + "%", Color.Black, e.ProgressPercentage);
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                downloadstatus.status = -1;
            }
            else
            {
                downloadstatus.status = 1;
            }
        }

        private void updatesstrip_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip contextmenu = (ContextMenuStrip)sender;
            ListView lw = (ListView)contextmenu.SourceControl;
            bool enabled = lw.SelectedIndices.Count > 0;
            for(int i = 0; i < contextmenu.Items.Count - 2; i++)
                contextmenu.Items[i].Enabled = enabled;
            selectAllToolStripMenuItem.Text = (lw.CheckedIndices.Count == lw.Items.Count ? "Uncheck all" : "Check all");
        }

        private void toggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem stripmenuitem = (ToolStripMenuItem)sender;
            ListView lw = (ListView)updatesstrip.SourceControl;
            foreach (ListViewItem item in lw.SelectedItems)
            {
                item.Checked = !item.Checked;
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem stripmenuitem = (ToolStripMenuItem)sender;
            ListView lw = (ListView)updatesstrip.SourceControl;
            bool uncheck = lw.CheckedIndices.Count == lw.Items.Count;
            foreach (ListViewItem item in lw.Items)
            {
                item.Checked = !uncheck;
            }
        }

        private void toggleAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem stripmenuitem = (ToolStripMenuItem)sender;
            ListView lw = (ListView)updatesstrip.SourceControl;
            foreach (ListViewItem item in lw.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        private void blacklistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blacklistform form = new blacklistform();
            form.ShowDialog();
            upgrademods.Enabled = false;
        }

        private void addToBlacklistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem stripmenuitem = (ToolStripMenuItem)sender;
            ListView lw = (ListView)updatesstrip.SourceControl;
            foreach (ListViewItem item in lw.SelectedItems)
            {
                if(Settings.options.blacklist.Contains(item.Text))
                {
                    Settings.options.blacklist.Remove(item.Text);
                    item.ForeColor = Color.Black;
                    item.BackColor = Color.White;
                }
                else
                {
                    Settings.options.blacklist.Add(item.Text);
                    item.ForeColor = Color.DarkRed;
                    item.BackColor = Color.Gray;
                }

                
            }
            Settings.Save();
            upgrademods.Enabled = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by kuba6000#2271\n" + Settings.version + " - Many bugs included", "GTNH Jenksins Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void backup_Click(object sender, EventArgs e)
        {
            backupsToolStripMenuItem_Click(null, null);
        }

        private void backupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backupform f = new backupform();
            f.ShowDialog();
        }
    }
}
