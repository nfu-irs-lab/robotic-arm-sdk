using System;
using System.Text.RegularExpressions;
using SDKHrobot;

namespace NFUIRSL.HRTK
{
    /// <summary>
    /// A action of arm.
    /// </summary>
    public interface IArmAction
    {
        /// <summary>
        /// The message of this action.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Enable wait for arm motion complete.
        /// </summary>
        bool NeedWait { get; set; }

        /// <summary>
        /// The ID of arm.
        /// </summary>
        int ArmId { get; set; }

        /// <summary>
        /// Do the action.
        /// </summary>
        /// <returns>Success</returns>
        bool Do();
    }

    /// <summary>
    /// A motion type of arm.
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
        }

        protected ArmMotion(double[] position)
        {
            Position = position;
        }

        public CoordinateType CoordinateType { get; set; } = CoordinateType.Descartes;

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

        public MotionType MotionType { get; set; } = MotionType.PointToPoint;
        public int SmoothValue { get; set; } = 50;

        /// <summary>
        /// Target position.
        /// </summary>
        public double[] Position { get; } = new double[6];

        public virtual string Message
        {
            get => "Arm Motion";
        }

        public int ArmId { get; set; }
        public bool NeedWait { get; set; } = true;

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

        protected virtual bool IsSuccess(int code, int ignoreCode = 0, int successCode = 0)
            => (code == ignoreCode) || (code == successCode);
    }

    /// <summary>
    /// A Point-To-Point motion of arm.
    /// </summary>
    public class PointToPointMotion : ArmMotion
    {
        /// <summary>
        /// Point-To-Point motion of arm.
        /// </summary>
        /// <param name="xJ1"></param>
        /// <param name="yJ2"></param>
        /// <param name="zJ3"></param>
        /// <param name="aJ4"></param>
        /// <param name="bJ5"></param>
        /// <param name="cJ6"></param>
        public PointToPointMotion(double xJ1,
                                  double yJ2,
                                  double zJ3,
                                  double aJ4,
                                  double bJ5,
                                  double cJ6)
            : base(xJ1, yJ2, zJ3, aJ4, bJ5, cJ6)
        {
            SmoothType = SmoothType.TwoLinesSpeedSmooth;
        }

        /// <summary>
        /// Point-To-Point motion of arm.
        /// </summary>
        /// <param name="position"></param>
        public PointToPointMotion(double[] position) : base(position)
        { }

        public override string Message
        {
            get => $"PointToPoint:" +
                   $"\"{GetTextPosition(Position)}\";" +
                   $"CT:{CoordinateType.ToString()};" +
                   $"PT:{PositionType.ToString()};" +
                   $"ST:{SmoothTypeCode};" +
                   $"Wait:{NeedWait}";
        }

        public SmoothType SmoothType
        {
            get => SmoothTypeCode == 1 ? SmoothType.TwoLinesSpeedSmooth : SmoothType.Disable;
            set => SmoothTypeCode = value == SmoothType.TwoLinesSpeedSmooth ? 1 : 0;
        }

        public override bool Do()
        {
            Func<int, int, double[], int> action;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when PositionType == PositionType.Absolute:
                    action = HRobot.ptp_pos;
                    break;

                case CoordinateType.Descartes when PositionType == PositionType.Relative:
                    action = HRobot.ptp_rel_pos;
                    break;

                case CoordinateType.Joint when PositionType == PositionType.Absolute:
                    action = HRobot.ptp_axis;
                    break;

                case CoordinateType.Joint when PositionType == PositionType.Relative:
                    action = HRobot.ptp_rel_axis;
                    break;

                default:
                    return false;
            }
            var returnCode = action(ArmId, SmoothTypeCode, Position);
            return IsSuccess(returnCode);
        }
    }
}