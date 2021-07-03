using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic.File;

namespace Basic.Message
{
    /// <summary>
    /// 顯示訊息及記錄 Log 檔案的訊息處理實作。
    /// </summary>
    public class GeneralMessage : IMessage
    {
        private readonly ILogHandler LogHandler = null;

        public GeneralMessage(ILogHandler logHandler)
        {
            LogHandler = logHandler;
        }

        public void Log(string message, LoggingLevel loggingLevel)
        {
            LogHandler.Write(message, loggingLevel);
        }

        public void Log(Exception ex, LoggingLevel loggingLevel)
        {
            LogHandler.Write(ex, loggingLevel);
        }

        public DialogResult Show(string message,
                                 LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            LogHandler.Write(message, loggingLevel);
            return MessageBox.Show(message,
                                   loggingLevel.ToString(),
                                   MessageBoxButtons.OK,
                                   ConvertLoggingLevelToMessageBoxIcon(loggingLevel));
        }

        public DialogResult Show(Exception ex,
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

        public DialogResult Show(string message,
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

        public DialogResult Show(string text,
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