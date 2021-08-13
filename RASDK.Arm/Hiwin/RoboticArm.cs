using System;
using RASDK.Basic.Message;
using RASDK.Arm.Type;
using RASDK.Basic;

namespace RASDK.Arm.Hiwin
{
    public class RoboticArm : ArmActionFactory, IDevice
    {
        private readonly string _ip;
        private int _id;
        private static bool _waiting = false;

        public RoboticArm(string ip, IMessage message) : base(message)
        {
            _ip = ip;
        }

        public int Id => _id;

        public override double Speed
        {
            get => new Speed(_id, _message).Value;
            set => new Speed(_id, _message).Value = (int)Math.Round(value);
        }

        public override double Acceleration
        {
            get => new Acceleration(_id, _message).Value;
            set => new Acceleration(_id, _message).Value = (int)Math.Round(value);
        }

        public override double[] GetNowPosition(CoordinateType coordinateType = CoordinateType.Descartes)
        {
            return new GetNowPosition(_id, _message).Value(coordinateType);
        }

        public override IConnection Connection => new Connection(_ip, _message, ref _id, ref _waiting);

        public override IMotion Motion => new Motion(_id, _message, ref _waiting);

        #region IDevice

        // IDevice 在這層實作是爲了遵守介面隔離原則。

        public bool Connected => Connection.IsOpen;

        public bool Connect()
        {
            Connection.Open();
            return Connected;
        }

        public bool Disconnect()
        {
            Connection.Close();
            return !Connected;
        }

        #endregion IDevice
    }
}