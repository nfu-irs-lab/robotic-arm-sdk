using System;
using RASDK.Basic.Message;
using RASDK.Arm.Hiwin;
using RASDK.Arm.Type;
using RASDK.Basic;

namespace RASDK.Arm
{
    public abstract class ArmActionFactory
    {
        protected readonly IMessage _message;

        public ArmActionFactory(IMessage message)
        {
            _message = message;
        }

        public abstract double Speed { get; set; }
        public abstract double Acceleration { get; set; }

        public abstract double[] NowPosition(CoordinateType coordinateType = CoordinateType.Descartes);

        public abstract IConnection Connection { get; }

        public abstract IMotion Motion { get; }
    }
}