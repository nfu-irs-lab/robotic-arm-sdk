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
        private int _id = -99;

        public RoboticArm(string ip, IMessage message) : base(message)
        {
            _ip = ip;
        }

        public override double Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IConnection Connection => throw new NotImplementedException();
        public int Id => _id;

        public override IMotion Motion => throw new NotImplementedException();
        public override double Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override double[] GetNowPosition(CoordinateType coordinateType = CoordinateType.Descartes)
        {
            throw new NotImplementedException();
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