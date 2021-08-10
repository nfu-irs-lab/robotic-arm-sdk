using System;
using RASDK.Basic.Message;
using RASDK.Arm.Type;
using RASDK.Basic;

namespace RASDK.Arm.Hiwin
{
    public class HiwinArm : ArmActionFactory, IDevice
    {
        private readonly string _ip;
        private int _id;
        private static bool _waiting = false;

        public HiwinArm(string ip, IMessage message) : base(message)
        {
            _ip = ip;
        }

        public int Id => _id;

        public override IConnection Connection()
        {
            return new HiwinConnection(_ip, _message, ref _id, ref _waiting);
        }

        public override IMotion Motion()
        {
            return new HiwinMotion(_id,
                                   _message,
                                   ref _waiting);
        }

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
    }
}