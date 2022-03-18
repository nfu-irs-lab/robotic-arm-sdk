using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RASDK.Basic.Message
{
    /// <summary>
    /// 訊息處理器。
    /// </summary>
    public abstract class MessageHandler
    {
        /// <summary>
        /// 日誌處理器。
        /// </summary>
        protected readonly LogHandler _logHandler;

        /// <summary>
        /// 訊息處理器。
        /// </summary>
        public MessageHandler(LogHandler logHandler)
        {
            _logHandler = logHandler;
        }

        /// <summary>
        /// Log.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loggingLevel"></param>
        public virtual void Log(string message, LoggingLevel loggingLevel)
        {
            _logHandler.Write(message, loggingLevel);
        }

        /// <summary>
        /// Log.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="loggingLevel"></param>
        public virtual void Log(Exception ex, LoggingLevel loggingLevel)
        {
            _logHandler.Write(ex, loggingLevel);
        }

        /// <summary>
        /// 方法開始時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="parameterName">參數名稱。</param>
        /// <param name="parameterValue">參數值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void LogMethodStart(string methodName,
                                           string parameterName,
                                           string parameterValue,
                                           string additionalMessage = "",
                                           LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            _logHandler.WriteMethodStart(methodName, parameterName, parameterValue, additionalMessage, loggingLevel);
        }

        /// <summary>
        /// 方法開始時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="parameterNames">參數名稱。</param>
        /// <param name="parameterValues">參數值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void LogMethodStart(string methodName,
                                           List<string> parameterNames,
                                           List<string> parameterValues,
                                           string additionalMessage = "",
                                           LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            _logHandler.WriteMethodStart(methodName, parameterNames, parameterValues, additionalMessage, loggingLevel);
        }

        /// <summary>
        /// 方法結束時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="returnValue">回傳值。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void LogMethodEnd(string methodName,
                                         string returnValue,
                                         string additionalMessage = "",
                                         LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            _logHandler.WriteMethodEnd(methodName, returnValue, additionalMessage, loggingLevel);
        }

        /// <summary>
        /// 方法結束時寫入日誌。
        /// </summary>
        /// <param name="methodName">方法名稱。</param>
        /// <param name="additionalMessage">額外訊息。</param>
        /// <param name="loggingLevel">日誌等級。</param>
        public virtual void LogMethodEnd(string methodName,
                                         string additionalMessage = "",
                                         LoggingLevel loggingLevel = LoggingLevel.Trace)
        {
            _logHandler.WriteMethodEnd(methodName, additionalMessage, loggingLevel);
        }

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loggingLevel"></param>
        public abstract DialogResult Show(string message,
                                          LoggingLevel loggingLevel = LoggingLevel.Trace);

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="loggingLevel"></param>
        public abstract DialogResult Show(Exception ex,
                                          LoggingLevel loggingLevel = LoggingLevel.Trace);

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="loggingLevel"></param>
        public abstract DialogResult Show(string message,
                                          Exception ex,
                                          LoggingLevel loggingLevel = LoggingLevel.Trace);

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="loggingLevel"></param>
        public abstract DialogResult Show(string text,
                                          string caption,
                                          MessageBoxButtons buttons,
                                          MessageBoxIcon icon,
                                          LoggingLevel loggingLevel = LoggingLevel.Trace);
    }
}