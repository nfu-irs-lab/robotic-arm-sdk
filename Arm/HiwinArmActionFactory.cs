using Basic.Message;

namespace Arm
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

        public override void Do(IArmDo armDo)
        { }

        public override Connect GetConnect()
        {
            var hc = new HiwinConnect(_ip, _message, out _id, out var connected, ref _waiting);
            Connected = connected;
            return hc;
        }

        public override Disconnect GetDisconnect()
        {
            var hd = new HiwinDisconnect(_id, _message, out var connected);
            Connected = connected;
            return hd;
        }
    }
}