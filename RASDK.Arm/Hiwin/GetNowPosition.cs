using System;
using RASDK.Arm.Type;
using RASDK.Basic.Message;

namespace RASDK.Arm.Hiwin
{
    internal class GetNowPosition : BasicAction
    {
        public GetNowPosition(int id, IMessage message) : base(id, message)
        { }

        public double[] Value(CoordinateType coordinateType = CoordinateType.Descartes)
        {
            var position = new double[6];
            Func<int, double[], int> action;

            switch (coordinateType)
            {
                case CoordinateType.Descartes:
                    action = HRobot.get_current_position;
                    break;

                case CoordinateType.Joint:
                    action = HRobot.get_current_joint;
                    break;

                default:
                    throw new ArgumentException("Unknown coordinator type.");
            }

            var returnCode = action(_id, position);
            ReturnCodeCheck.IsSuccessful(returnCode, _message);
            return position;
        }
    }
}