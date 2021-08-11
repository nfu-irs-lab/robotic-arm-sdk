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

        public abstract IConnection Connection();

        public abstract IMotion Motion();
    }
}