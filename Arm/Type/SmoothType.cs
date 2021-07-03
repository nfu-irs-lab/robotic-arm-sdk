using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arm.Type
{
    /// <summary>
    /// 手臂平滑模式。
    /// </summary>
    public enum SmoothType
    {
        /// <summary>
        /// 關閉平滑功能。
        /// </summary>
        Disable = 0,

        /// <summary>
        /// 貝茲曲線平滑百分比。
        /// </summary>
        BezierCurveSmoothPercent = 1,

        /// <summary>
        /// 貝茲曲線平滑半徑。
        /// </summary>
        BezierCurveSmoothRadius = 2,

        /// <summary>
        /// 依兩線段速度平滑。
        /// </summary>
        TwoLinesSpeedSmooth = 3
    }
}