using System;
using Arm.Type;
using Basic.Message;

namespace Arm.Hiwin
{
    public abstract class HiwinBasicMotion : HiwinBasicAction
    {
        public CoordinateType CoordinateType = CoordinateType.Descartes;
        public MotionType MotionType = MotionType.PointToPoint;
        public bool NeedWait = true;
        public int SmoothValue = 50;
        protected double[] _position;
        protected int _smoothTypeCode;
        protected unsafe bool* _waitingState;

        public HiwinBasicMotion(double xJ1,
                                double yJ2,
                                double zJ3,
                                double aJ4,
                                double bJ5,
                                double cJ6,
                                int id,
                                IMessage message,
                                ref bool waitingState,
                                AdditionalMotionParameters additionalPara = null)
            : base(id, message)
        {
            if (additionalPara != null)
            {
                MotionType = additionalPara.MotionType;
                CoordinateType = additionalPara.CoordinateType;
                SmoothType = additionalPara.SmoothType;
                SmoothValue = additionalPara.SmoothValue;
                NeedWait = additionalPara.NeedWait;
            }

            _position = new[] { xJ1, yJ2, zJ3, aJ4, bJ5, cJ6 };
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