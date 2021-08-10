using System;
using RASDK.Basic.Message;
using RASDK.Arm.Hiwin;
using RASDK.Arm.Type;

namespace RASDK.Arm
{
    public abstract class ArmActionFactory
    {
        protected readonly IMessage _message;

        public ArmActionFactory(IMessage message)
        {
            _message = message;
        }

        public abstract IGetConnectionState GetConnectionState(out bool connected);

        public abstract IConnection Connection();

        public abstract IAbsoluteMotion AbsoluteMotion(double[] position,
                                                       AdditionalMotionParameters additionalMotionParameters = null);

        public abstract IAbsoluteMotion AbsoluteMotion(double xJ1,
                                                       double yJ2,
                                                       double zJ3,
                                                       double aJ4,
                                                       double bJ5,
                                                       double cJ6,
                                                       AdditionalMotionParameters additionalMotionParameters = null);

        public abstract IRelativeMotion RelativeMotion(double[] position,
                                                       AdditionalMotionParameters additionalMotionParameters = null);

        public abstract IRelativeMotion RelativeMotion(double xJ1,
                                                       double yJ2,
                                                       double zJ3,
                                                       double aJ4,
                                                       double bJ5,
                                                       double cJ6,
                                                       AdditionalMotionParameters additionalMotionParameters = null);

        public abstract IHoming Homing(CoordinateType coordinateType = CoordinateType.Descartes, bool needWait = true);

        public virtual IJog Jog(string axis, bool needWait = true)
        {
            throw new NotImplementedException();
        }

        public virtual IAbortMotion AbortMotion()
        {
            throw new NotImplementedException();
        }
    }
}