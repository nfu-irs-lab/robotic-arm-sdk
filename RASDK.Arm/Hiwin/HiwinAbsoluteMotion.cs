using SDKHrobot;
using Basic.Message;
using RASDK.Arm.Type;

namespace RASDK.Arm.Hiwin
{
    public class HiwinAbsoluteMotion : HiwinBasicMotion, IAbsoluteMotion
    {
        public HiwinAbsoluteMotion(double xJ1,
                                   double yJ2,
                                   double zJ3,
                                   double aJ4,
                                   double bJ5,
                                   double cJ6,
                                   int id,
                                   IMessage message,
                                   out int returnCode,
                                   ref bool waitingState,
                                   AdditionalMotionParameters additionalPara = null)
            : base(xJ1, yJ2, zJ3, aJ4, bJ5, cJ6, id, message, ref waitingState, additionalPara)
        {
            returnCode = Action();
        }

        private int Action()
        {
            int returnCode = 0;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == base.MotionType.PointToPoint:
                    returnCode = HRobot.ptp_pos(_id,
                                                _smoothTypeCode,
                                                _position);
                    break;

                case base.CoordinateType.Descartes when MotionType == base.MotionType.Linear:
                    returnCode = HRobot.lin_pos(_id,
                                                _smoothTypeCode,
                                                SmoothValue,
                                                _position);
                    break;

                case base.CoordinateType.Joint when MotionType == base.MotionType.PointToPoint:
                    returnCode = HRobot.ptp_axis(_id,
                                                 _smoothTypeCode,
                                                 _position);
                    break;

                case base.CoordinateType.Joint when MotionType == base.MotionType.Linear:
                    returnCode = HRobot.lin_axis(_id,
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