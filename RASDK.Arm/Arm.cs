using Basic.Message;

namespace RASDK.Arm
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
            _arm.Connect();
        }

        public void Disconnect()
        {
            _arm.Disconnect();
        }
    }
}