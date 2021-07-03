using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arm.Type
{
    /// <summary>
    /// 座標類型。
    /// </summary>
    public enum CoordinateType
    {
        /// <summary>
        /// 笛卡爾座標。
        /// </summary>
        Descartes,

        /// <summary>
        /// 關節座標。
        /// </summary>
        Joint,

        /// <summary>
        /// 未知的座標類型。
        /// </summary>
        Unknown
    }
}