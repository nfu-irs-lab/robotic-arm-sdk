using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.TMRobot
{
    public class TMRobotArm : ArmActionFactory, IDevice
    {
        private readonly string _ip;
        private readonly int _port;

        public TMRobotArm(string ip, int port, IMessage message) : base(message)
        {
            _ip = ip;
            _port = port;
        }

        public override IConnection Connection()
        {
            throw new System.NotImplementedException();
        }

        public override IMotion Motion()
        {
            throw new System.NotImplementedException();
        }

        // IDevice 在這層實作是爲了遵守介面隔離原則。

        #region IDevice

        public bool Connected => Connection().IsOpen;

        public bool Connect()
        {
            Connection().Open();
            return Connected;
        }

        public bool Disconnect()
        {
            Connection().Close();
            return !Connected;
        }

        #endregion IDevice
    }
}