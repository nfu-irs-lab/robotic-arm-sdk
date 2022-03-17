using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RASDK.Basic.Message
{
    /// <summary>
    /// 不執行任何動作的訊息處理器。
    /// </summary>
    public class EmptyMessageHandler : IMessageHandler
    {
        /// <summary>
        /// 日誌記錄。
        /// </summary>
        /// <param name="content">內容。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public void Log(string content, LoggingLevel loggingLevel)
        { }

        /// <summary>
        /// 日誌記錄。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public void Log(Exception ex, LoggingLevel loggingLevel)
        { }

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="message">訊息內容。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public DialogResult Show(string message, LoggingLevel loggingLevel = LoggingLevel.Trace) => DialogResult.None;

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public DialogResult Show(Exception ex, LoggingLevel loggingLevel = LoggingLevel.Trace) => DialogResult.None;

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="message">訊息內容。</param>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public DialogResult Show(string message, Exception ex, LoggingLevel loggingLevel = LoggingLevel.Trace) => DialogResult.None;

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="text">訊息內容。</param>
        /// <param name="caption">標題。</param>
        /// <param name="buttons">按鈕。</param>
        /// <param name="icon">圖示。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public DialogResult Show(string text,
                                 string caption,
                                 MessageBoxButtons buttons,
                                 MessageBoxIcon icon,
                                 LoggingLevel loggingLevel = LoggingLevel.Trace) => DialogResult.None;
    }
}