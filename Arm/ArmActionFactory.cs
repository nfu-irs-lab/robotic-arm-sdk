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

        public abstract IAbsoluteMotion AbsoluteMotion(double[] position);

        public abstract IAbsoluteMotion AbsoluteMotion(double xJ1,
                                                       double yJ2,
                                                       double zJ3,
                                                       double aJ4,
                                                       double bJ5,
                                                       double cJ6);

        public abstract IRelativeMotion RelativeMotion(double[] position);

        public abstract IRelativeMotion RelativeMotion(double xJ1,
                                                       double yJ2,
                                                       double zJ3,
                                                       double aJ4,
                                                       double bJ5,
                                                       double cJ6);
    }
}