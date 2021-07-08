using Basic.Message;

namespace Arm
{
    public abstract class ArmActionFactory
    {
        protected readonly IMessage _message;
        protected bool _connected = false;
        public bool Connected => _connected;

        public ArmActionFactory(IMessage message)
        {
            _message = message;
        }

        public abstract IConnect GetConnect();

        public abstract IDisconnect GetDisconnect();
    }
}