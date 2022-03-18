using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RASDK.Basic.Message
{
    /// <summary>
    /// 只處理日誌的訊息處理器，會記錄 Log 檔案。
    /// </summary>
    public class LogOnlyMessageHandler : MessageHandler
    {
        private readonly DialogResult _defaultDialogResult;

        /// <summary>
        /// 只處理日誌的訊息處理器，會記錄 Log 檔案。
        /// </summary>
        /// <param name="logHandler">日誌處理器。</param>
        /// <param name="defaultDialogResult">預設的訊息框結果。</param>
        public LogOnlyMessageHandler(LogHandler logHandler, DialogResult defaultDialogResult = DialogResult.OK)
            : base(logHandler)
        {
            _defaultDialogResult = defaultDialogResult;
        }

        /// <summary>
        /// 假的顯示訊息框。只會記錄日誌，不會實際顯示訊息框。
        /// </summary>
        /// <param name="message">內容。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public override DialogResult Show(string message, LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            _logHandler.Write(message, loggingLevel);
            return _defaultDialogResult;
        }

        /// <summary>
        /// 假的顯示訊息框。只會記錄日誌，不會實際顯示訊息框。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public override DialogResult Show(Exception ex, LoggingLevel loggingLevel = LoggingLevel.Trace)
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
            return _defaultDialogResult;
        }

        /// <summary>
        /// 假的顯示訊息框。只會記錄日誌，不會實際顯示訊息框。
        /// </summary>
        /// <param name="message">內容。</param>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public override DialogResult Show(string message, Exception ex, LoggingLevel loggingLevel = LoggingLevel.Trace)
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
            return _defaultDialogResult;
        }

        /// <summary>
        /// 假的顯示訊息框。只會記錄日誌，不會實際顯示訊息框。
        /// </summary>
        /// <param name="text">內容。</param>
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
            return _defaultDialogResult;
        }
    }
}