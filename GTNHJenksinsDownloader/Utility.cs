using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GTNHJenksinsDownloader
{
    public static class Utility
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int AllocConsole();
        //https://docs.microsoft.com/en-us/windows/console/getstdhandle
        private const Int32 StdInputHandle = -10;
        private const Int32 StdOutputHandle = -11;
        private const Int32 StdErrorHandle = -12;
        [DllImport("kernel32.dll")]
        private static extern void SetStdHandle(Int32 nStdHandle, IntPtr handle);
        
        [DllImport("kernel32.dll",
            CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFileW(
              string lpFileName,
              UInt32 dwDesiredAccess,
              UInt32 dwShareMode,
              IntPtr lpSecurityAttributes,
              UInt32 dwCreationDisposition,
              UInt32 dwFlagsAndAttributes,
              IntPtr hTemplateFile
            );

        private const UInt32 GENERIC_WRITE = 0x40000000;
        private const UInt32 GENERIC_READ = 0x80000000;
        private const UInt32 FILE_SHARE_READ = 0x00000001;
        private const UInt32 FILE_SHARE_WRITE = 0x00000002;
        private const UInt32 OPEN_EXISTING = 0x00000003;
        private const UInt32 FILE_ATTRIBUTE_NORMAL = 0x80;
        private const UInt32 ERROR_ACCESS_DENIED = 5;

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        public static string Get(string uri) //https://stackoverflow.com/questions/27108264/how-to-properly-make-a-http-web-get-request
        {

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

        public static int GetBuildNumberFromVersionString(string version)
        {
            return int.Parse(version.Replace(".", ""));
        }

        public static void CreateConsole()
        {
            if(!AttachConsole(-1))
                AllocConsole();

            IntPtr consoleOutputHandle = CreateFileW("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
            IntPtr consoleInputHandle = CreateFileW("CONIN$", GENERIC_READ, FILE_SHARE_READ, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
            SetStdHandle(StdOutputHandle, consoleOutputHandle);
            SetStdHandle(StdErrorHandle, consoleOutputHandle);
            SetStdHandle(StdInputHandle, consoleInputHandle);

            // reopen stdout
            {
                TextWriter writer = new StreamWriter(Console.OpenStandardOutput())
                { AutoFlush = true };
                Console.SetOut(writer);
                Console.SetError(writer); // Error stream is same as output
            }
            // reopen stdin
            {
                TextReader reader = new StreamReader(Console.OpenStandardInput());
                Console.SetIn(reader);
            }
        }
    }
}
