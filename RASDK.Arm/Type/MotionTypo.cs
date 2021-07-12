using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.Type
{
    /// <summary>
    /// 運動類型。
    /// </summary>
    public enum MotionType
    {
        /// <summary>
        /// 點對點運動。
        /// </summary>
        PointToPoint,

        /// <summary>
        /// 直線運動。
        /// </summary>
        Linear,

        /// <summary>
        /// 圓弧運動。
        /// </summary>
        Circle,

        /// <summary>
        /// 未知的類型。
        /// </summary>
        Unknown
    }
}