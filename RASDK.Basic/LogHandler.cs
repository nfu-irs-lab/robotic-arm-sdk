using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RASDK.Basic
{
    /// <summary>
    /// 日誌等級。<br/>
    /// 數值越大表示越嚴重。
    /// </summary>
    public enum LoggingLevel : byte
    {
        /// <summary>
        /// 蹤跡。
        /// </summary>
        Trace = 0,

        /// <summary>
        /// 資訊。
        /// </summary>
        Info,

        /// <summary>
        /// 警告。
        /// </summary>
        Warn,

        /// <summary>
        /// 錯誤。
        /// </summary>
        Error,

        /// <summary>
        /// 致命。
        /// </summary>
        Fatal = byte.MaxValue
    }

    /// <summary>
    /// 日誌處理器介面。
    /// </summary>
    public interface ILogHandler
    {
        /// <summary>
        /// 日誌檔案路徑。
        /// </summary>
        string Path { get; }

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="message">訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        void Write(string message, LoggingLevel loggingLevel);

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        void Write(Exception ex, LoggingLevel loggingLevel);
    }

    /// <summary>
    /// 空的日誌處理器。不實際進行動作。
    /// </summary>
    public class EmptyLogHandler : ILogHandler
    {
        /// <summary>
        /// 日誌檔案路徑。
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="message">訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public void Write(string message, LoggingLevel loggingLevel)
        { }

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public void Write(Exception ex, LoggingLevel loggingLevel)
        { }
    }

    /// <summary>
    /// 一般的日誌處理器。
    /// </summary>
    public class LogHandler : ILogHandler
    {
        /// <summary>
        /// 要記錄的日誌等級。
        /// </summary>
        private readonly LoggingLevel LoggingLevel;

        private string Filename;

        /// <summary>
        /// 一般的日誌處理器。
        /// </summary>
        /// <param name="path">日誌檔案路徑。</param>
        /// <param name="loggingLevel">要記錄的日誌等級。</param>
        public LogHandler(string path = "",
                          LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            Path = path;
            LoggingLevel = loggingLevel;
            CreateFile();
        }

        /// <summary>
        /// 解構子。
        /// </summary>
        ~LogHandler()
        {
            Write("LogHandler destructed, stop logging.", LoggingLevel.Fatal);
        }

        /// <summary>
        /// 日誌檔案路徑。
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public void Write(Exception ex, LoggingLevel loggingLevel)
        {
            Write($"{ex.Message}. {ex.StackTrace}", loggingLevel);
        }

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="message">訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public void Write(string message, LoggingLevel loggingLevel)
        {
            if (loggingLevel >= LoggingLevel)
            {
                string text = DateTime.Now.ToString("HH:mm:ss") +
                              $"[{loggingLevel}]" +
                              $"{message.Replace("\r", "").Replace("\n", ";").Trim()}";

                var file = MakeStreamWriter();
                file.WriteLine(text);
                file.Close();
            }
        }

        private void CreateFile()
        {
            // 取得目前的時間。
            var dateTimeNow = DateTime.Now;
            var num = 1;

            // Update filename.
            while (true)
            {
                // 設定目標檔案名稱。
                var targetFilename = $"{dateTimeNow:MMMdd-HH}_{num}.log";

                // 判斷目前檔案是否已經存在。
                if (System.IO.File.Exists(Path + targetFilename))
                {
                    // 若目標檔案已經存在，遞增序號，使檔案名稱不重複 。
                    num++;
                }
                else
                {
                    // 若目標檔案不存在，使用此檔案名稱。
                    Filename = targetFilename;
                    break;
                }
            }

            var sw = MakeStreamWriter();
            sw.WriteLine($"{dateTimeNow:yyyy-MM-dd_HH:mm:ss}  " +
                         $"LogLv: {LoggingLevel}\r\n---");
            sw.Close();
        }

        /// <summary>
        /// Factory pattern。
        /// </summary>
        /// <returns></returns>
        private StreamWriter MakeStreamWriter()
        {
            try
            {
                return System.IO.File.AppendText(Path + Filename);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Path);
                return System.IO.File.AppendText(Path + Filename);
            }
        }
    }
}