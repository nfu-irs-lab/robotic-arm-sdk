using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm
{
    public class Arm : IDevice
    {
        private readonly ArmActionFactory _arm;

        public Arm(ArmActionFactory arm)
        {
            _arm = arm;
        }

        public bool Connected
        {
            get
            {
                _arm.GetConnectionState(out var connected);
                return connected;
            }
        }

        bool IDevice.Disconnect()
        {
            return !Connected;
        }

        bool IDevice.Connect()
        {
            _arm.Connection();
            return Connected;
        }
    }
}