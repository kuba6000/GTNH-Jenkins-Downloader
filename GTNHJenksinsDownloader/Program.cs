using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;

namespace GTNHJenksinsDownloader
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        /// 

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main()
        {
#if !DEBUG
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Settings.apppath)).Length > 1)
            {
                MessageBox.Show("App arledy running !", "GTNH Jenkins Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string exepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dirname = Path.GetDirectoryName(exepath);
            if (!dirname.EndsWith("\\")) dirname += "\\";
            if (dirname != Settings.appdirectory)
            {
                if (!Directory.Exists(Settings.appdirectory))
                    Directory.CreateDirectory(Settings.appdirectory);
                //if (!File.Exists(Settings.apppath))
                File.Copy(exepath, Settings.apppath, true); // updates
                Process.Start(Settings.apppath);
                return;
            }
#endif
            //https://github.com/JamesNK/Newtonsoft.Json/releases/download/13.0.1/Json130r1.zip

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

            
            Application.Run(new MainForm());
        }
    }
}
