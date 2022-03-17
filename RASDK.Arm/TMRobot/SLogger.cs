using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RASDK.Arm.TMRobot
{
    internal class SLogger
    {
        private const string LOG_FOLDER = @".\TCP_Client";
        private const string LOG_PREFIX = "log_";
        private static object Locker = new object();
        private static bool RdEngMode = false;
        private static bool useSLogger = false;
        private static string versionHMI = Assembly.GetEntryAssembly().GetName().Version.ToString();

        public static void debug(string s)
        {
            log(s, "DEBUG");
        }

        public static void log(string s, string title = "TCP_Client")
        {
            if (useSLogger)
            {
                string path = Path.Combine(@".\TCP_Client", "log_" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                try
                {
                    string message = string.Format("{0}[{1}] {2}\r\n", DateTime.Now.ToString("HH:mm:ss:fff: "), title, s);
                    Debug.WriteLine(message);
                    lock (Locker)
                    {
                        if (File.Exists(path))
                        {
                            FileInfo info = new FileInfo(path);
                            string destFileName = "";
                            if (info.Length > 0x5f5e100L)
                            {
                                destFileName = Path.Combine(@".\TCP_Client", "log_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".log");
                                File.Move(path, destFileName);
                                string contents = string.Format("{0}[{1}] {2}\r\n",
                                                                DateTime.Now.ToString("HH:mm:ss:fff: "),
                                                                title,
                                                                versionHMI);
                                File.AppendAllText(path, contents);
                            }
                        }
                        File.AppendAllText(path, message);
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("Exception on log:" + exception.StackTrace.ToString());
                }
            }
        }

        public static void log(string s, string title, int Level, int CurrentLevel)
        {
            try
            {
                if ((CurrentLevel >= Level) || RdEngMode)
                {
                    log(s, title);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Exception on log:" + exception.StackTrace.ToString());
            }
        }

        public static void setRdEngMode(bool _val)
        {
            lock (Locker)
            {
                RdEngMode = _val;
            }
        }
    }
}