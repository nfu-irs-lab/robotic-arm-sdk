using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RASDK.Basic.Message
{
    /// <summary>
    /// 一般的訊息處理器，會顯示訊息及記錄 Log 檔案。
    /// </summary>
    public class GeneralMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 日誌處理器。
        /// </summary>
        protected readonly ILogHandler LogHandler = null;

        /// <summary>
        /// 一般的訊息處理器，會顯示訊息及記錄 Log 檔案。
        /// </summary>
        /// <param name="logHandler">日誌處理器。</param>
        public GeneralMessageHandler(ILogHandler logHandler)
        {
            LogHandler = logHandler;
        }

        /// <summary>
        /// 日誌記錄。
        /// </summary>
        /// <param name="message">內容。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public void Log(string message, LoggingLevel loggingLevel)
        {
            LogHandler.Write(message, loggingLevel);
        }

        /// <summary>
        /// 日誌記錄。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public void Log(Exception ex, LoggingLevel loggingLevel)
        {
            LogHandler.Write(ex, loggingLevel);
        }

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="message">訊息內容。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public virtual DialogResult Show(string message,
                                         LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            LogHandler.Write(message, loggingLevel);
            return MessageBox.Show(message,
                                   loggingLevel.ToString(),
                                   MessageBoxButtons.OK,
                                   ConvertLoggingLevelToMessageBoxIcon(loggingLevel));
        }

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public virtual DialogResult Show(Exception ex,
                                         LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            string text = "未處理的例外。\r\n\r\n";

            if (ex != null)
            {
                text += $"{ex.Message}\r\n\r\n" +
                        $"{ex.StackTrace}";
            }
            else
            {
                text += "null Exception.";
            }

            LogHandler.Write(text, loggingLevel);
            return MessageBox.Show(text,
                                   loggingLevel.ToString(),
                                   MessageBoxButtons.OK,
                                   ConvertLoggingLevelToMessageBoxIcon(loggingLevel));
        }

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="message">訊息內容。</param>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public virtual DialogResult Show(string message,
                                         Exception ex,
                                         LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            string text = $"{message} \r\n\r\n";

            if (ex != null)
            {
                text += $"{ex.Message} \r\n\r\n" +
                        $"{ex.StackTrace}";
            }
            else
            {
                text += "null Exception.";
            }

            LogHandler.Write(text, loggingLevel);
            return MessageBox.Show(text,
                                   loggingLevel.ToString(),
                                   MessageBoxButtons.OK,
                                   ConvertLoggingLevelToMessageBoxIcon(loggingLevel));
        }

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="text">訊息內容。</param>
        /// <param name="caption">標題。</param>
        /// <param name="buttons">按鈕。</param>
        /// <param name="icon">圖示。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public virtual DialogResult Show(string text,
                                 string caption,
                                 MessageBoxButtons buttons,
                                 MessageBoxIcon icon,
                                 LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            LogHandler.Write($"{caption}: {text}", loggingLevel);
            return MessageBox.Show(text, caption, buttons, icon);
        }

        private MessageBoxIcon ConvertLoggingLevelToMessageBoxIcon(LoggingLevel loggingLevel)
        {
            MessageBoxIcon messageBoxIcon;
            switch (loggingLevel)
            {
                case LoggingLevel.Info:
                    messageBoxIcon = MessageBoxIcon.Information;
                    break;

                case LoggingLevel.Warn:
                    messageBoxIcon = MessageBoxIcon.Warning;
                    break;

                case LoggingLevel.Error:
                case LoggingLevel.Fatal:
                    messageBoxIcon = MessageBoxIcon.Error;
                    break;

                case LoggingLevel.Trace:
                default:
                    messageBoxIcon = MessageBoxIcon.None;
                    break;
            }
            return messageBoxIcon;
        }
    }
}