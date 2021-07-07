using Basic.Message;

namespace Arm
{
    public class Arm
    {
        private readonly ArmActionFactory _arm;

        public Arm(ArmActionFactory arm)
        {
            _arm = arm;
        }

        public void Connect()
        {
            _arm.GetConnect();
        }


        public void Disconnect()
        {
            _arm.GetDisconnect();
        }
    }
}