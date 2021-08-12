using System;
using RASDK.Basic.Message;
using RASDK.Arm.Type;

namespace RASDK.Arm.Hiwin
{
    public abstract class BasicMotion : BasicAction
    {
        public CoordinateType CoordinateType = CoordinateType.Descartes;
        public MotionType MotionType = MotionType.PointToPoint;
        public bool NeedWait = true;
        public int SmoothValue = 50;
        protected int _smoothTypeCode;
        protected unsafe bool* _waitingState;

        protected AdditionalMotionParameters _additionalMotionParameters
        {
            set
            {
                if (value != null)
                {
                    MotionType = value.MotionType;
                    CoordinateType = value.CoordinateType;
                    SmoothType = value.SmoothType;
                    SmoothValue = value.SmoothValue;
                    NeedWait = value.NeedWait;
                }
            }
        }

        public BasicMotion(int id,
                           IMessage message,
                           ref bool waitingState)
            : base(id, message)
        {
            SmoothType = SmoothType.TwoLinesSpeedSmooth;

            unsafe
            {
                fixed (bool* w = &waitingState)
                {
                    _waitingState = w;
                }
            }
        }

        public SmoothType SmoothType
        {
            get
            {
                SmoothType type = SmoothType.Disable;
                switch (MotionType)
                {
                    case MotionType.Circle:
                    case MotionType.PointToPoint:
                        type = (_smoothTypeCode == 1) ? SmoothType.TwoLinesSpeedSmooth : SmoothType.Disable;
                        break;

                    case MotionType.Linear:
                        type = SmoothType;
                        break;
                }
                return type;
            }

            set
            {
                switch (MotionType)
                {
                    case MotionType.Circle:
                    case MotionType.PointToPoint:
                        _smoothTypeCode = (value == SmoothType.TwoLinesSpeedSmooth) ? 1 : 0;
                        break;

                    case MotionType.Linear:
                        _smoothTypeCode = (int)value;
                        break;

                    default:
                        _smoothTypeCode = 0;
                        break;
                }
            }
        }

        protected void WaitForMotionComplete(int returnCode)
        {
            if (NeedWait && ReturnCodeCheck.IsSuccessful(returnCode))
            {
                unsafe
                {
                    *_waitingState = true;
                    while (*_waitingState)
                    {
                        /* Null. */
                    }
                }
            }
        }
    }
}