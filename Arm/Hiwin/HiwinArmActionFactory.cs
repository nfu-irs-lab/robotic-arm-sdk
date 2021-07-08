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

        public override IAbsoluteMotion AbsoluteMotion(double[] position)
        {
            return new HiwinAbsoluteMotion(position, _id, _message, out var code);
        }

        public override IAbsoluteMotion AbsoluteMotion(double xJ1,
                                                       double yJ2,
                                                       double zJ3,
                                                       double aJ4,
                                                       double bJ5,
                                                       double cJ6)
        {
            return new HiwinAbsoluteMotion(new[] { xJ1, yJ2, zJ3, aJ4, bJ5, cJ6, }, _id, _message, out var code);
        }

        public override IRelativeMotion RelativeMotion(double[] position)
        {
            return new HiwinRelativeMotion(position, _id, _message, out var code);
        }

        public override IRelativeMotion RelativeMotion(double xJ1,
                                                       double yJ2,
                                                       double zJ3,
                                                       double aJ4,
                                                       double bJ5,
                                                       double cJ6)
        {
            return new HiwinRelativeMotion(new[] { xJ1, yJ2, zJ3, aJ4, bJ5, cJ6, }, _id, _message, out var code);
        }
    }
}