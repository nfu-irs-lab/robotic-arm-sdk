using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Basic.File;

namespace Basic.Message
{
    /// <summary>
    /// 訊息處理介面。
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Log.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loggingLevel"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Log(string message, LoggingLevel loggingLevel);

        /// <summary>
        /// Log.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="loggingLevel"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Log(Exception ex, LoggingLevel loggingLevel);

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loggingLevel"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DialogResult Show(string message,
                          LoggingLevel loggingLevel = LoggingLevel.Trace);

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="loggingLevel"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DialogResult Show(Exception ex,
                          LoggingLevel loggingLevel = LoggingLevel.Trace);

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="loggingLevel"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DialogResult Show(string message,
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DialogResult Show(string text,
                          string caption,
                          MessageBoxButtons buttons,
                          MessageBoxIcon icon,
                          LoggingLevel loggingLevel = LoggingLevel.Trace);
    }
}