using RASDK.Arm.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.CoppeliaSim
{
    public class RoboticArm : RASDK.Arm.RoboticArm
    {
        public override double Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override bool Connected => throw new NotImplementedException();
        public override double Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override bool Connect()
        {
            throw new NotImplementedException();
        }

        public override bool Disconnect()
        {
            throw new NotImplementedException();
        }

        public override double[] GetNowPosition(CoordinateType coordinate = CoordinateType.Descartes)
        {
            throw new NotImplementedException();
        }

        public override void Homing(bool slowly = true, CoordinateType coordinate = CoordinateType.Descartes, bool needWait = true)
        {
            throw new NotImplementedException();
        }

        public override void Jog(string axis)
        {
            throw new NotImplementedException();
        }

        public override void MoveAbsolute(double j1X, double j2Y, double j3Z, double j4A, double j5B, double j6C, AdditionalMotionParameters addParams = null)
        {
            throw new NotImplementedException();
        }

        public override void MoveRelative(double j1X, double j2Y, double j3Z, double j4A = 0, double j5B = 0, double j6C = 0, AdditionalMotionParameters addParams = null)
        {
            throw new NotImplementedException();
        }
    }
}