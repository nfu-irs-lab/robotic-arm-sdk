using SDKHrobot;
using Arm.Type;
using Basic.Message;

namespace Arm.Hiwin
{
    public class HiwinRelativeMotion : HiwinBasicMotion, IRelativeMotion
    {
        public HiwinRelativeMotion(double xJ1,
                                   double yJ2,
                                   double zJ3,
                                   double aJ4,
                                   double bJ5,
                                   double cJ6,
                                   int id,
                                   IMessage message,
                                   out int returnCode,
                                   ref bool waitingState)
            : base(xJ1, yJ2, zJ3, aJ4, bJ5, cJ6, id, message, ref waitingState)
        {
            returnCode = Action();
        }

        private int Action()
        {
            int returnCode = 0;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_pos(_id,
                                                    _smoothTypeCode,
                                                    _position);
                    break;

                case CoordinateType.Descartes when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_pos(_id,
                                                    _smoothTypeCode,
                                                    SmoothValue,
                                                    _position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_axis(_id,
                                                     _smoothTypeCode,
                                                     _position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_axis(_id,
                                                     _smoothTypeCode,
                                                     SmoothValue,
                                                     _position);
                    break;
            }

            WaitForMotionComplete(returnCode);
            return returnCode;
        }
    }
}