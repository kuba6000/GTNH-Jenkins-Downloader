using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GTNHJenksinsDownloader
{
    static class Logger
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

        private static bool initialized = false;

        public static void Init()
        {
            if (initialized)
                return;
            initialized = true;

            bool debugshow = false;
#if DEBUG
            debugshow = true;
#endif
            if(Settings.options.showconsole || debugshow)
                if (!AttachConsole(-1))
                    AllocConsole();

            IntPtr consoleOutputHandle = CreateFileW("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
            SetStdHandle(StdOutputHandle, consoleOutputHandle);
            SetStdHandle(StdErrorHandle, consoleOutputHandle);
            IntPtr consoleInputHandle = CreateFileW("CONIN$", GENERIC_READ, FILE_SHARE_READ, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
            SetStdHandle(StdInputHandle, consoleInputHandle);

            // reopen stdout
            {
                if (!Directory.Exists(Settings.loggerdirectory))
                    Directory.CreateDirectory(Settings.loggerdirectory);
                Stream fileStream = File.Create(Path.Combine(Settings.loggerdirectory, DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss") + ".txt"));
                Stream consoleStream = Console.OpenStandardOutput();
                LoggerStreamManipulator writer = new LoggerStreamManipulator(consoleStream, fileStream)
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

        private sealed class LoggerStreamManipulator : TextWriter
        {
            private readonly Stream consoleStream;
            private readonly Stream fileStream;
            public bool AutoFlush;

            public LoggerStreamManipulator(Stream consoleStream, Stream fileStream)
            {
                this.consoleStream = consoleStream;
                this.fileStream = fileStream;
            }

            public override void Write(string text)
            {
                text = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + text;
                var buffer = Encoding.UTF8.GetBytes(text);
                consoleStream.Write(buffer, 0, buffer.Length);
                fileStream.Write(buffer, 0, buffer.Length);
                if (AutoFlush)
                    Flush();
            }

            public override void WriteLine(string value)
            {
                Write(value + "\r\n");
            }

            public override void Write(char value)
            {
                byte b = Encoding.UTF8.GetBytes(new char[] { value }, 0, 1)[0];
                consoleStream.WriteByte(b);
                fileStream.WriteByte(b);
                if (AutoFlush)
                    Flush();
            }

            public override void Flush()
            {
                fileStream.Flush();
                consoleStream.Flush();
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }
    }
}
