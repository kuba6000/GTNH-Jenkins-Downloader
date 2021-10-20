using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTNHJenksinsDownloader
{
    public static class Utility
    {
        

        private static Random random = new Random();

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        public static string Get(string uri) //https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
        {
            Console.WriteLine("Get: " + uri);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = "GTNHJenkinsDownloader";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static bool messageBoxYesNo(string text, string title = "", MessageBoxIcon icon = MessageBoxIcon.Question)
        {
            if (title == "")
                title = Settings.appname;
            DialogResult res = MessageBox.Show(text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return res == DialogResult.Yes;
        }

        public static string randomString(int length)
        {
            string s = "";
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            while(--length > 0)
                s += chars[random.Next(chars.Length)];
            return s;
        }

        public static int randomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static int randomInt(int max)
        {
            return random.Next(max);
        }

        public static string calculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static bool checkifsamemod(string a, string b)
        {
            return cropmodname(a) == cropmodname(b);
        }
        public static string cropmodname(string modname)
        {
            int _;
            if (modname.Contains("/"))
                modname = modname.Substring(modname.LastIndexOf('/') + 1);
            if (modname.Contains("\\"))
                modname = modname.Substring(modname.LastIndexOf('\\') + 1);
            for (int i = 0; i < modname.Length; i++)
                if (int.TryParse(modname[i].ToString(), out _))
                    return modname.Substring(0, i + 1);
            return modname.ToLower();
        }
        public static string trimnumbers(string modname)
        {
            string ret = "";
            for (int i = 0; i < modname.Length; i++)
                if (!int.TryParse(modname[i].ToString(), out _))
                    ret += modname[i];
            return ret;
        }

        public static int GetBuildNumberFromVersionString(string version)
        {
            return int.Parse(version.Replace(".", ""));
        }

        
    }
}
