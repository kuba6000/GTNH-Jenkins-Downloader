using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace GTNHJenksinsDownloader
{
    

    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        /// 

        [STAThread]
        static void Main(string[] args)
        {
            if (Array.Exists(args, x => x == "updated"))
            {
                MessageBox.Show("Updated", Settings.appname, MessageBoxButtons.OK, MessageBoxIcon.Information);
                string temppath = Path.Combine(Path.GetTempPath(), "GTNHJenkinsDownloader\\");
                string file = Path.Combine(temppath, "temp.exe");
                if (File.Exists(file)) File.Delete(file);
                if (Directory.Exists(temppath)) Directory.Delete(temppath);
            }
            
            if (Array.Exists(args, x => x == "wait"))
                Thread.Sleep(500);
            if (Array.Exists(args, x => x == "update"))
            {
                Thread.Sleep(500);
                string file = Path.Combine(Path.GetTempPath(), "GTNHJenkinsDownloader\\temp.exe");
                File.Copy(file, Settings.apppath, true);
                Process.Start(Settings.apppath, "updated");
                return;
            }
            
#if !DEBUG
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Settings.apppath)).Length > 1)
            {
                MessageBox.Show("App alredy running !", "GTNH Jenkins Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dirname = Path.GetDirectoryName(exepath);
            if (!dirname.EndsWith("\\")) dirname += "\\";
            if (dirname != Settings.appdirectory)
            {
                
                if (!Directory.Exists(Settings.appdirectory))
                    Directory.CreateDirectory(Settings.appdirectory);
                if (!File.Exists(Settings.apppath))
                    File.Copy(exepath, Settings.apppath, true);
                else
                {
                    string fver = FileVersionInfo.GetVersionInfo(Settings.apppath).ProductVersion;
                    if (Utility.GetBuildNumberFromVersionString(fver) < Settings.buildNumber ||
                        fver == "1.0.0.0" /* Before implementing versions */)
                    {
                        File.Copy(exepath, Settings.apppath, true);
                        Process.Start(Settings.apppath, "updated");
                        return;
                    }
                }
                Process.Start(Settings.apppath, "wait");
                return;
            }
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string dllpath = Path.Combine(Settings.appdirectory, "Newtonsoft.Json.dll");
            if (!File.Exists(dllpath))
            {
                Info info = new Info();
                info.Show();
                info.Update();
                string temppath = Path.Combine(Path.GetTempPath(), "GTNHJenkinsDownloader\\");
                if (!Directory.Exists(temppath)) Directory.CreateDirectory(temppath);
                string zipfile = Path.Combine(temppath, "temp.zip");
                if (File.Exists(zipfile)) File.Delete(zipfile);
                WebClient client = new WebClient();
                client.DownloadFile("https://github.com/JamesNK/Newtonsoft.Json/releases/download/13.0.1/Json130r1.zip", zipfile);
                ZipArchive arch = ZipFile.OpenRead(zipfile);
                arch.GetEntry("Bin/net45/Newtonsoft.Json.dll").ExtractToFile(dllpath);
                arch.Dispose();
                File.Delete(zipfile);
                Directory.Delete(temppath);
                info.Close();
            }

            if (update()) return;

            Application.Run(new MainForm());
        }

        static bool update()
        {
            string res = Utility.Get("https://api.github.com/repos/kuba6000/GTNH-Jenkins-Downloader/releases");
            Newtonsoft.Json.Linq.JArray arr = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(res);
            if (arr.Count > 0)
            {
                if (Utility.GetBuildNumberFromVersionString(((string)arr[0]["name"]).Substring(1)) > Settings.buildNumber)
                {
                    if (Utility.messageBoxYesNo("Do you want to update ?\n" + Settings.version + " -> " + (string)arr[0]["name"], "New update found !", MessageBoxIcon.Information))
                    {
                        string temppath = Path.Combine(Path.GetTempPath(), "GTNHJenkinsDownloader\\");
                        if (!Directory.Exists(temppath)) Directory.CreateDirectory(temppath);
                        string file = Path.Combine(temppath, "temp.exe");
                        if (File.Exists(file)) File.Delete(file);
                        WebClient client = new WebClient();
                        client.DownloadFile((string)arr[0]["assets"][0]["browser_download_url"], file);
                        Process.Start(file, "update");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
