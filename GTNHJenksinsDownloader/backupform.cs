using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace GTNHJenksinsDownloader
{
    public partial class backupform : Form
    {
        private bool loading = false;
        public backupform()
        {
            InitializeComponent();
        }

        private void backupform_Load(object sender, EventArgs e)
        {
            loading = true;
            deleteold.Checked = Settings.options.b_deleteold;
            deleteafter.Checked = Settings.options.b_deleteafter;
            backup.refreshbackups(this);
            loading = false;
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utility.messageBoxYesNo("Are you sure to restore this backup ?"))
            {
                backup.restorebackup(backuplist.SelectedIndices[0]);
                backup.refreshbackups(this);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utility.messageBoxYesNo("Are you sure to remove this backup ?"))
            {
                backup.removebackup(backuplist.SelectedIndices[0]);
                backup.refreshbackups(this);
            }
        }

        private void extractToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                backup.restorebackup(backuplist.SelectedIndices[0], fbd.SelectedPath);
            }
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backup.showinfo(backuplist.SelectedIndices[0]);
        }

        private void deleteold_CheckedChanged(object sender, EventArgs e)
        {
            if (loading)
                return;
            Settings.options.b_deleteold = deleteold.Checked;
            Settings.Save();
        }

        private void deleteafter_CheckedChanged(object sender, EventArgs e)
        {
            if (loading)
                return;
            Settings.options.b_deleteafter = deleteafter.Checked;
            Settings.Save();
        }

        private void deleteall_Click(object sender, EventArgs e)
        {
            if (Utility.messageBoxYesNo("Are you sure to remove this backup ?"))
            {
                for(int i = 0; i < backuplist.Items.Count; i ++)
                    backup.removebackup(i);
                backup.refreshbackups(this);
            }
        }

        private void backupdir_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.backupdirectory);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool check = backuplist.SelectedIndices.Count == 1;
            foreach (ToolStripItem item in contextMenuStrip1.Items)
                item.Enabled = check;
        }
    }

    public class backupcreator
    {
        public enum backuptype : int
        {
            TYPE_CLIENT = 0,
            TYPE_SERVER
        }
        private string ongoingbackup = "";
        private List<string> mods = new List<string>();
        public void Begin()
        {
            string temppath = "";
            if (ongoingbackup != "")
            {
                temppath = Path.Combine(Path.GetTempPath(), ongoingbackup + '\\');
                if (Directory.Exists(temppath))
                    Directory.Delete(temppath, true);
            }
            do
            {
                ongoingbackup = Utility.randomString(10);
                temppath = Path.Combine(Path.GetTempPath(), ongoingbackup + '\\');
            }
            while (Directory.Exists(temppath));
            Directory.CreateDirectory(temppath);
            Console.WriteLine("[BACKUP " + ongoingbackup + "] Began at " + temppath);
        }

        public void Add(string path)
        {
            if (ongoingbackup == "")
                return;
            if (!File.Exists(path))
                return;
            if (Path.GetExtension(path) != ".jar")
                return;
            File.Copy(path, Path.Combine(Path.GetTempPath(), ongoingbackup + '\\' + mods.Count + ".jar"));
            mods.Add(Path.GetFileNameWithoutExtension(path));
            Console.WriteLine("[BACKUP " + ongoingbackup + "] Added mod: " + Path.GetFileName(path));
        }

        public void End(backuptype type)
        {
            if (ongoingbackup == "")
                return;
            if (mods.Count == 0)
            {
                Directory.Delete(Path.Combine(Path.GetTempPath(), ongoingbackup + '\\'), true);
                ongoingbackup = "";
                return;
            }
            File.WriteAllLines(Path.Combine(Path.GetTempPath(), ongoingbackup + "\\mods.txt"), mods);
            File.WriteAllLines(Path.Combine(Path.GetTempPath(), ongoingbackup + "\\data.txt"), new string[]{ ((int)type).ToString() });
            string name = Path.Combine(Settings.backupdirectory, (type == backuptype.TYPE_CLIENT ? "client-" : "server-") + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".zip");
            ZipFile.CreateFromDirectory(Path.Combine(Path.GetTempPath(), ongoingbackup + '\\'), name);
            Directory.Delete(Path.Combine(Path.GetTempPath(), ongoingbackup + '\\'), true);
            mods.Clear();

            Console.WriteLine("[BACKUP " + ongoingbackup + "] Created: " + name);

            ongoingbackup = "";
            if (Settings.options.b_deleteafter)
            {
                var a = Directory.EnumerateFiles(Settings.backupdirectory, "*.zip").ToList();
                if(a.Count > 2)
                {
                    int oldest = 0;
                    DateTime oldesttime = DateTime.Now;
                    for(int i = 0; i < a.Count; i++)
                        if(File.GetCreationTime(a[i]) < oldesttime)
                        {
                            oldest = i;
                            oldesttime = File.GetCreationTime(a[i]);
                        }
                    File.Delete(a[oldest]);
                }
            }
        }
    }

    public static class backup
    {
        private class b_
        {
            public string name;
            public string path;
            public int modcount;
            public DateTime creationtime;
        }
        private static List<b_> backuplist = new List<b_>();
        public static void showinfo(int index)
        {
            if (index >= backuplist.Count)
                return;
            if (!File.Exists(backuplist[index].path))
                return;
            string text = "Modlist: \n";
            ZipArchive arch = ZipFile.OpenRead(backuplist[index].path);
            StreamReader modsentry = new StreamReader(arch.GetEntry("mods.txt").Open());
            for (int i = 0; i < backuplist[index].modcount; i++)
                text += modsentry.ReadLine() + "\n";
            modsentry.Close();
            arch.Dispose();
            MessageBox.Show(text, backuplist[index].name);
        }
        public static void restorebackup(int index, string custompath = "")
        {
            if (index >= backuplist.Count)
                return;
            if (!File.Exists(backuplist[index].path))
                return;
            ZipArchive arch = ZipFile.OpenRead(backuplist[index].path);
            StreamReader modsentry = new StreamReader(arch.GetEntry("mods.txt").Open());
            StreamReader dataentry = new StreamReader(arch.GetEntry("data.txt").Open());
            string destination = Settings.options.modpath;
            if ((backupcreator.backuptype)int.Parse(dataentry.ReadLine()) == backupcreator.backuptype.TYPE_SERVER)
                destination = Settings.options.servermodpath;
            if (custompath != "")
                destination = custompath;
            dataentry.Close();
            var currentmods = Directory.EnumerateFiles(destination, "*.jar").ToList();
            for (int i = 0; i < backuplist[index].modcount; i++)
            {
                ZipArchiveEntry entry = arch.GetEntry(i.ToString() + ".jar");
                string modname = modsentry.ReadLine();
                string currentmodname = currentmods.Find(x => Utility.checkifsamemod(Path.GetFileNameWithoutExtension(x), modname));
                if (currentmodname != null)
                    File.Delete(currentmodname);
                entry.ExtractToFile(Path.Combine(destination, modname + ".jar"));
            }
            modsentry.Close();
            arch.Dispose();
            //DateTime time = backuplist[index].creationtime;
            if(custompath == "")
                File.Delete(backuplist[index].path);
        }
        public static void removebackup(int index)
        {
            if (index >= backuplist.Count)
                return;
            if (File.Exists(backuplist[index].path))
                File.Delete(backuplist[index].path);
        }

        public static void refreshbackups(backupform sender)
        {
            sender.backuplist.Items.Clear();
            backuplist.Clear();
            var files = Directory.EnumerateFiles(Settings.backupdirectory, "*.zip");
            foreach (string file in files)
            {
                ZipArchive arch;
                try
                {
                    arch = ZipFile.OpenRead(file);
                }
                catch { continue; }
                try
                {
                    if (!arch.Entries.Any(entry => entry.Name == "mods.txt"))
                    {
                        arch.Dispose();
                        continue;
                    }
                    if (!arch.Entries.Any(entry => entry.Name == "data.txt"))
                    {
                        arch.Dispose();
                        continue;
                    }
                    if (!arch.Entries.Any(entry => entry.Name == (arch.Entries.Count - 3).ToString() + ".jar"))
                    {
                        arch.Dispose();
                        continue;
                    }
                    StreamReader reader = new StreamReader(arch.GetEntry("data.txt").Open());
                    backupcreator.backuptype type = (backupcreator.backuptype)int.Parse(reader.ReadLine());
                    reader.Close();
                    backuplist.Add(new b_ { name = Path.GetFileNameWithoutExtension(file), path = file, modcount = arch.Entries.Count - 2, creationtime = File.GetCreationTime(file) });
                    ListViewItem item = new ListViewItem();
                    item.Text = File.GetCreationTime(file).ToString("yyyy-MM-dd HH.mm.ss");
                    item.SubItems.Add((arch.Entries.Count - 2).ToString());
                    item.SubItems.Add(type == backupcreator.backuptype.TYPE_CLIENT ? "client" : "server");
                    sender.backuplist.Items.Add(item);
                    arch.Dispose();
                }
                catch { arch.Dispose(); };
            }
        }
    }
}
