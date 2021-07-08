using Arm.Type;
using SDKHrobot;
using Basic.Message;

namespace Arm.Hiwin
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
                                   out int returnCode)
            : base(xJ1, yJ2, zJ3, aJ4, bJ5, cJ6, id, message)
        {
            returnCode = Action();
        }

        public HiwinAbsoluteMotion(double[] position, int id, IMessage message, out int returnCode)
            : base(position, id, message)
        {
            returnCode = Action();
        }

        private int Action()
        {
            int returnCode = 0;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_pos(_id,
                                                _smoothTypeCode,
                                                _position);
                    break;

                case CoordinateType.Descartes when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_pos(_id,
                                                _smoothTypeCode,
                                                SmoothValue,
                                                _position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_axis(_id,
                                                 _smoothTypeCode,
                                                 _position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_axis(_id,
                                                 _smoothTypeCode,
                                                 SmoothValue,
                                                 _position);
                    break;
            }
            return returnCode;
        }
    }
}