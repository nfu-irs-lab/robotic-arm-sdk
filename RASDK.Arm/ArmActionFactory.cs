using System;
using Basic.Message;
using RASDK.Arm.Hiwin;
using RASDK.Arm.Type;

namespace RASDK.Arm
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

        public abstract IConnect Connect();

        public abstract IDisconnect Disconnect();

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