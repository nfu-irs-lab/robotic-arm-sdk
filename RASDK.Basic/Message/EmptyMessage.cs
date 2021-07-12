using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RASDK.Basic.Message
{
    /// <summary>
    /// 不執行任何動作的訊息處理實作。
    /// </summary>
    public class EmptyMessage : IMessage
    {
        public void Log(string content, LoggingLevel loggingLevel)
        { }

        public void Log(Exception ex, LoggingLevel loggingLevel)
        { }

        public DialogResult Show(string message, LoggingLevel loggingLevel = LoggingLevel.Trace) => DialogResult.None;

        public DialogResult Show(Exception ex, LoggingLevel loggingLevel = LoggingLevel.Trace) => DialogResult.None;

        public DialogResult Show(string message, Exception ex, LoggingLevel loggingLevel = LoggingLevel.Trace) => DialogResult.None;

        public DialogResult Show(string text,
                                 string caption,
                                 MessageBoxButtons buttons,
                                 MessageBoxIcon icon,
                                 LoggingLevel loggingLevel = LoggingLevel.Trace) => DialogResult.None;
    }
}