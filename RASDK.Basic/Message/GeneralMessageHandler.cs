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
    public class GeneralMessageHandler : MessageHandler
    {
        /// <summary>
        /// 一般的訊息處理器，會顯示訊息及記錄 Log 檔案。
        /// </summary>
        /// <param name="logHandler">日誌處理器。</param>
        public GeneralMessageHandler(LogHandler logHandler) : base(logHandler)
        {
        }

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="message">訊息內容。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public override DialogResult Show(string message,
                                          LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            _logHandler.Write(message, loggingLevel);
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
        public override DialogResult Show(Exception ex,
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

            _logHandler.Write(text, loggingLevel);
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
        public override DialogResult Show(string message,
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

            _logHandler.Write(text, loggingLevel);
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
        public override DialogResult Show(string text,
                                          string caption,
                                          MessageBoxButtons buttons,
                                          MessageBoxIcon icon,
                                          LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            _logHandler.Write($"{caption}: {text}", loggingLevel);
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