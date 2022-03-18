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
    public class EmptyMessageHandler : MessageHandler
    {
        /// <summary>
        /// 不執行任何動作的訊息處理器。
        /// </summary>
        public EmptyMessageHandler() : base(new EmptyLogHandler())
        {
        }

        /// <summary>
        /// 日誌記錄。
        /// </summary>
        /// <param name="content">內容。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public override void Log(string content, LoggingLevel loggingLevel)
        { }

        /// <summary>
        /// 日誌記錄。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public override void Log(Exception ex, LoggingLevel loggingLevel)
        { }

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="message">訊息內容。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public override DialogResult Show(string message, LoggingLevel loggingLevel = LoggingLevel.Trace)
            => DialogResult.None;

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public override DialogResult Show(Exception ex, LoggingLevel loggingLevel = LoggingLevel.Trace)
            => DialogResult.None;

        /// <summary>
        /// 顯示訊息框。
        /// </summary>
        /// <param name="message">訊息內容。</param>
        /// <param name="ex">例外情況。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        /// <returns>訊息框結果。</returns>
        public override DialogResult Show(string message, Exception ex, LoggingLevel loggingLevel = LoggingLevel.Trace)
            => DialogResult.None;

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
            => DialogResult.None;

        /// <summary>
        /// 方法開始時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="parameterName">參數名稱。</param>
        /// <param name="parameterValue">參數值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public override void LogMethodStart(string methodName,
                                           string parameterName,
                                           string parameterValue,
                                           string additionalMessage = "",
                                           LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
        }

        /// <summary>
        /// 方法開始時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="parameterNames">參數名稱。</param>
        /// <param name="parameterValues">參數值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public override void LogMethodStart(string methodName,
                                           List<string> parameterNames,
                                           List<string> parameterValues,
                                           string additionalMessage = "",
                                           LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
        }

        /// <summary>
        /// 方法結束時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="returnValue">回傳值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public override void LogMethodEnd(string methodName,
                                         string returnValue,
                                         string additionalMessage = "",
                                         LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
        }

        /// <summary>
        /// 方法結束時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public override void LogMethodEnd(string methodName,
                                         string additionalMessage = "",
                                         LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
        }
    }
}