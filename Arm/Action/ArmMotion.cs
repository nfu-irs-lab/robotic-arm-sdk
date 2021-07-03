using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arm.Type;

namespace Arm.Action
{
    /// <summary>
    /// The motion of arm.
    /// </summary>
    public abstract class ArmMotion : IArmAction
    {
        protected int SmoothTypeCode;

        protected ArmMotion(double xJ1,
                            double yJ2,
                            double zJ3,
                            double aJ4,
                            double bJ5,
                            double cJ6)
        {
            Position[0] = xJ1;
            Position[1] = yJ2;
            Position[2] = zJ3;
            Position[3] = aJ4;
            Position[4] = bJ5;
            Position[5] = cJ6;
            SmoothType = SmoothType.Disable;
        }

        protected ArmMotion(double[] position)
        {
            Position = position;
            SmoothType = SmoothType.Disable;
        }

        public int ArmId { get; set; }
        public CoordinateType CoordinateType { get; set; } = CoordinateType.Descartes;

        public virtual string Message
        {
            get => "Arm:" +
                   $"\"{GetTextPosition(Position)}\"," +
                   $"PT:{PositionType}," +
                   $"CT:{CoordinateType}," +
                   $"MT:{MotionType}," +
                   $"ST:{SmoothTypeCode}," +
                   $"SV:{SmoothValue}," +
                   $"Wait:{NeedWait}";
        }

        public MotionType MotionType { get; set; } = MotionType.PointToPoint;
        public bool NeedWait { get; set; } = true;

        /// <summary>
        /// Target position.
        /// </summary>
        public double[] Position { get; } = new double[6];

        public SmoothType SmoothType
        {
            get
            {
                SmoothType type = SmoothType.Disable;
                switch (MotionType)
                {
                    case MotionType.Circle:
                    case MotionType.PointToPoint:
                        type = (SmoothTypeCode == 1) ? SmoothType.TwoLinesSpeedSmooth : SmoothType.Disable;
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
                        SmoothTypeCode = (value == SmoothType.TwoLinesSpeedSmooth) ? 1 : 0;
                        break;

                    case MotionType.Linear:
                        SmoothTypeCode = (int)value;
                        break;

                    default:
                        SmoothTypeCode = 0;
                        break;
                }
            }
        }

        public int SmoothValue { get; set; } = 50;
        protected abstract PositionType PositionType { get; }

        public abstract bool Do();

        protected virtual string GetTextPosition(double[] position)
        {
            return $"{position[0]}," +
                   $"{position[1]}," +
                   $"{position[2]}," +
                   $"{position[3]}," +
                   $"{position[4]}," +
                   $"{position[5]}";
        }

        protected virtual bool IsSuccessful(int code, int ignoreCode = 0, int successCode = 0)
            => (code == ignoreCode) || (code == successCode);
    }
}