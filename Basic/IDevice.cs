using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic
{
    /// <summary>
    /// 基本裝置介面。
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Connect state. <br/>
        /// • true: Connected. <br/>
        /// • false: Disconnected or unknown. <br/>
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Open connection.<br/>
        /// </summary>
        /// <returns>
        /// true: Connection successful.<br/>
        /// false: Connection unsuccessful.
        /// </returns>
        bool Connect();

        /// <summary>
        /// Close connection.<br/>
        /// </summary>
        /// <returns>
        /// true: Disconnection successful.<br/>
        /// false: Disconnection unsuccessful.
        /// </returns>
        bool Disconnect();
    }
}