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
    /// 日誌處理器。
    /// </summary>
    public abstract class LogHandler
    {
        /// <summary>
        /// 日誌處理器。
        /// </summary>
        public LogHandler(string path = "")
        {
            Path = path;
        }

        /// <summary>
        /// 日誌檔案路徑。
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="message">訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public abstract void Write(string message, LoggingLevel loggingLevel);

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void Write(Exception ex, LoggingLevel loggingLevel)
        {
            Write($"{ex.Message}. {ex.StackTrace}", loggingLevel);
        }

        /// <summary>
        /// 方法開始時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="parameterName">參數名稱。</param>
        /// <param name="parameterValue">參數值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void WriteMethodStart(string methodName,
                                             string parameterName,
                                             string parameterValue,
                                             string additionalMessage = "",
                                             LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            Write($"執行：{methodName}()，參數 {parameterName}：{parameterValue}，{additionalMessage}。",
                  loggingLevel);
        }

        /// <summary>
        /// 方法開始時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="parameterNames">參數名稱。</param>
        /// <param name="parameterValues">參數值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void WriteMethodStart(string methodName,
                                             List<string> parameterNames,
                                             List<string> parameterValues,
                                             string additionalMessage = "",
                                             LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            string paramText = "";
            for (int i = 0; i < parameterNames.Count; i++)
            {
                paramText += $"參數 {parameterNames[i]}：{parameterValues[i]}，";
            }
            Write($"執行：{methodName}()，{paramText.TrimEnd('，')}，{additionalMessage}。", loggingLevel);
        }

        /// <summary>
        /// 方法結束時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="returnValue">回傳值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void WriteMethodEnd(string methodName,
                                           string returnValue,
                                           string additionalMessage = "",
                                           LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            Write($"完成：{methodName}()，回傳：{returnValue}，{additionalMessage}。",
                  loggingLevel);
        }

        /// <summary>
        /// 方法結束時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void WriteMethodEnd(string methodName,
                                           string additionalMessage = "",
                                           LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            Write($"完成：{methodName}()，{additionalMessage}。",
                  loggingLevel);
        }
    }

    /// <summary>
    /// 空的日誌處理器。不實際進行動作。
    /// </summary>
    public class EmptyLogHandler : LogHandler
    {
        /// <summary>
        /// 空的日誌處理器。不實際進行動作。
        /// </summary>
        public EmptyLogHandler() : base("")
        {
        }

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="message">訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public override void Write(string message, LoggingLevel loggingLevel)
        { }
    }

    /// <summary>
    /// 一般的日誌處理器。
    /// </summary>
    public class GeneralLogHandler : LogHandler
    {
        /// <summary>
        /// 要記錄的日誌等級。
        /// </summary>
        private readonly LoggingLevel _loggingLevel;

        private string _filename;

        /// <summary>
        /// 一般的日誌處理器。
        /// </summary>
        /// <param name="path">日誌檔案路徑。</param>
        /// <param name="loggingLevel">要記錄的日誌等級。</param>
        public GeneralLogHandler(string path = "",
                                 LoggingLevel loggingLevel = LoggingLevel.Trace)
            : base(path)
        {
            _loggingLevel = loggingLevel;
            CreateFile();
        }

        /// <summary>
        /// 解構子。
        /// </summary>
        ~GeneralLogHandler()
        {
            Write("LogHandler destructed, stop logging.", LoggingLevel.Fatal);
        }

        /// <summary>
        /// 寫入日誌。
        /// </summary>
        /// <param name="message">訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public override void Write(string message, LoggingLevel loggingLevel)
        {
            if (loggingLevel >= _loggingLevel)
            {
                string text = DateTime.Now.ToString("HH:mm:ss.ffff") +
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
                    _filename = targetFilename;
                    break;
                }
            }

            var sw = MakeStreamWriter();
            sw.WriteLine($"{dateTimeNow:yyyy-MM-dd_HH:mm:ss}  " +
                         $"Log Level: {_loggingLevel}\r\n---");
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
                return System.IO.File.AppendText(Path + _filename);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Path);
                return System.IO.File.AppendText(Path + _filename);
            }
        }
    }
}