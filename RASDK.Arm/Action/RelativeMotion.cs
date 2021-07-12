using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RASDK.Arm.Type;
using SDKHrobot;

namespace RASDK.Arm.Action
{
    /// <summary>
    /// The relative motion of arm.
    /// </summary>
    public class RelativeMotion : ArmMotion
    {
        /// <summary>
        /// The relative motion of arm.
        /// </summary>
        /// <param name="xJ1"></param>
        /// <param name="yJ2"></param>
        /// <param name="zJ3"></param>
        /// <param name="aJ4"></param>
        /// <param name="bJ5"></param>
        /// <param name="cJ6"></param>
        public RelativeMotion(double xJ1,
                              double yJ2,
                              double zJ3,
                              double aJ4 = 0,
                              double bJ5 = 0,
                              double cJ6 = 0)
            : base(xJ1, yJ2, zJ3, aJ4, bJ5, cJ6)
        { }

        /// <summary>
        /// The relative motion of arm.
        /// </summary>
        /// <param name="position"></param>
        public RelativeMotion(double[] position) : base(position)
        { }

        protected override PositionType PositionType => PositionType.Relative;

        public override bool Do()
        {
            int returnCode;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_pos(ArmId,
                                                    SmoothTypeCode,
                                                    Position);
                    break;

                case CoordinateType.Descartes when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_pos(ArmId,
                                                    SmoothTypeCode,
                                                    SmoothValue,
                                                    Position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_axis(ArmId,
                                                     SmoothTypeCode,
                                                     Position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_axis(ArmId,
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