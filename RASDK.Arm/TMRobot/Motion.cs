using AELTA_test;
using RASDK.Arm.Type;

namespace RASDK.Arm.TMRobot
{
    public class Motion : IMotion
    {
        private CommandSender _commandSender;

        public Motion(SocketClientObject socketClientObject)
        {
            _commandSender = new CommandSender(socketClientObject);
        }

        public void Abort()
        {
            throw new System.NotImplementedException();
        }

        public void Absolute(double[] position, AdditionalMotionParameters addPara = null)
        {
            throw new System.NotImplementedException();
        }

        public void Absolute(double xJ1,
                             double yJ2,
                             double zJ3,
                             double aJ4,
                             double bJ5,
                             double cJ6,
                             AdditionalMotionParameters addPara = null)
        {
            throw new System.NotImplementedException();
        }

        public void Relative(double[] position, AdditionalMotionParameters addPara = null)
        {
            throw new System.NotImplementedException();
        }

        public void Relative(double xJ1,
                             double yJ2,
                             double zJ3,
                             double aJ4,
                             double bJ5,
                             double cJ6,
                             AdditionalMotionParameters addPara = null)
        {
            throw new System.NotImplementedException();
        }

        public void Homing(CoordinateType coordinateType = CoordinateType.Descartes, bool needWait = true)
        {
            int speed = 100;
            double sp_pc = 1.0;
            string command = @"1,PTP(""CPP"",519,-122,458,185,0,90," + string.Format("{0:000}", speed * sp_pc) + ",200,0,false)";
            _commandSender.Send(command);
        }

        public void Jog(string axis)
        {
            throw new System.NotImplementedException();
        }
    }
}