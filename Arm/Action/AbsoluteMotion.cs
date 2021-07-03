using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arm.Type;
using SDKHrobot;

namespace Arm.Action
{
    /// <summary>
    /// The absolute motion of arm.
    /// </summary>
    public class AbsoluteMotion : ArmMotion
    {
        /// <summary>
        /// The absolute motion of arm.
        /// </summary>
        /// <param name="xJ1"></param>
        /// <param name="yJ2"></param>
        /// <param name="zJ3"></param>
        /// <param name="aJ4"></param>
        /// <param name="bJ5"></param>
        /// <param name="cJ6"></param>
        public AbsoluteMotion(double xJ1,
                              double yJ2,
                              double zJ3,
                              double aJ4,
                              double bJ5,
                              double cJ6)
            : base(xJ1, yJ2, zJ3, aJ4, bJ5, cJ6)
        { }

        /// <summary>
        /// The absolute motion of arm.
        /// </summary>
        /// <param name="position"></param>
        public AbsoluteMotion(double[] position) : base(position)
        { }

        protected override PositionType PositionType => PositionType.Absolute;

        public override bool Do()
        {
            int returnCode;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_pos(ArmId,
                                                SmoothTypeCode,
                                                Position);
                    break;

                case CoordinateType.Descartes when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_pos(ArmId,
                                                SmoothTypeCode,
                                                SmoothValue,
                                                Position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_axis(ArmId,
                                                 SmoothTypeCode,
                                                 Position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_axis(ArmId,
                                                 SmoothTypeCode,
                                                 SmoothValue,
                                                 Position);
                    break;

                default:
                    return false;
            }
            return IsSuccessful(returnCode);
        }
    }
}