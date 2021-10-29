using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RASDK.Basic;
using RASDK.Basic.Message;
using RASDK.Arm.Type;

namespace RASDK.Arm.CoppeliaSim
{
    public class RoboticArm : ArmActionFactory, IDevice
    {
        private readonly string _ip;
        private readonly string _objectName;
        private readonly int _port;
        private int _id = -99;

        public RoboticArm(string ip, int port, IMessage message, string objectName = "UR5")
            : base(message)
        {
            _ip = ip;
            _port = port;
            _objectName = objectName;
        }

        public override double Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IConnection Connection => new Connection(_ip, _port, _message, ref _id);
        public int Id => _id;
        public string Ip => _ip;
        public override IMotion Motion => new Motion(_objectName, _id, _message);
        public string ObjectName => _objectName;
        public int Port => _port;
        public override double Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override double[] GetNowPosition(CoordinateType coordinateType = CoordinateType.Descartes)
        {
            return new GetNowPosition(_objectName, _id, _message).Value(coordinateType);
        }

        #region IDevice

        // IDevice 在這層實作是爲了遵守介面隔離原則(ISP)。

        //public bool Connected => Connection.IsOpen;
        public bool Connected => throw new NotImplementedException();

        public bool Connect()
        {
            throw new NotImplementedException();
            //Connection.Open();
            //return Connected;
        }

        public bool Disconnect()
        {
            throw new NotImplementedException();
            //Connection.Close();
            //return !Connected;
        }

        #endregion IDevice
    }
}