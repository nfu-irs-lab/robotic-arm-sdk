using Basic.Message;

namespace Arm.Hiwin
{
    public class HiwinArmActionFactory : ArmActionFactory
    {
        private readonly string _ip;
        private int _id;
        private bool _waiting = false;

        public HiwinArmActionFactory(string ip, IMessage message) : base(message)
        {
            _ip = ip;
        }

        public int Id => _id;

        public override IConnect GetConnect()
        {
            return new HiwinConnect(_ip, _message, out _id, out _connected, ref _waiting);
        }

        public override IDisconnect GetDisconnect()
        {
            return new HiwinDisconnect(_id, _message, out _connected);
        }
    }
}