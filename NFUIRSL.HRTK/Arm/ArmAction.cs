using System;
using System.Text.RegularExpressions;
using SDKHrobot;

namespace NFUIRSL.HRTK
{
    /// <summary>
    /// The action of arm.
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
        /// <returns>Return true for successful.</returns>
        bool Do();
    }

    public class AbortMotion : IArmAction
    {
        public string Message => "Arm Abort.";

        public bool NeedWait
        {
            get => false;
            set { }
        }

        public int ArmId { get; set; }

        public bool Do()
        {
            var returnCode = HRobot.motion_abort(ArmId);
            return returnCode == 0;
        }
    }

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

        protected abstract PositionType PositionType { get; }

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
            get => "Arm:" +
                   $"\"{GetTextPosition(Position)}\"," +
                   $"PT:{PositionType}," +
                   $"CT:{CoordinateType}," +
                   $"MT:{MotionType}," +
                   $"ST:{SmoothTypeCode}," +
                   $"SV:{SmoothValue}," +
                   $"Wait:{NeedWait}";
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

        protected virtual bool IsSuccessful(int code, int ignoreCode = 0, int successCode = 0)
            => (code == ignoreCode) || (code == successCode);
    }

    /// <summary>
    /// The relative motion of arm.
    /// </summary>
    public class RelativeMotion : ArmMotion
    {
        /// <summary>
        /// The relative motion of arm.
        /// </summary>
        /// <param name="xJ1"></param>
        /// <param name="yJ2"></param>
        /// <param name="zJ3"></param>
        /// <param name="aJ4"></param>
        /// <param name="bJ5"></param>
        /// <param name="cJ6"></param>
        public RelativeMotion(double xJ1,
                              double yJ2,
                              double zJ3,
                              double aJ4,
                              double bJ5,
                              double cJ6)
            : base(xJ1, yJ2, zJ3, aJ4, bJ5, cJ6)
        { }

        /// <summary>
        /// The relative motion of arm.
        /// </summary>
        /// <param name="position"></param>
        public RelativeMotion(double[] position) : base(position)
        { }

        protected override PositionType PositionType => PositionType.Relative;

        public override bool Do()
        {
            int returnCode;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_pos(ArmId,
                                                    SmoothTypeCode,
                                                    Position);
                    break;

                case CoordinateType.Descartes when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_pos(ArmId,
                                                    SmoothTypeCode,
                                                    SmoothValue,
                                                    Position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_axis(ArmId,
                                                     SmoothTypeCode,
                                                     Position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_axis(ArmId,
                                                     SmoothTypeCode,
                                                     SmoothValue,
                                                     Position);
                    break;

                default:
                    return false;
            }
            return IsSuccessful(returnCode);
        }
    }

    /// <summary>
    /// The absolute motion of arm.
    /// </summary>
    public class AbsoluteMotion : ArmMotion
    {
        /// <summary>
        /// The absolute motion of arm.
        /// </summary>
        /// <param name="xJ1"></param>
        /// <param name="yJ2"></param>
        /// <param name="zJ3"></param>
        /// <param name="aJ4"></param>
        /// <param name="bJ5"></param>
        /// <param name="cJ6"></param>
        public AbsoluteMotion(double xJ1,
                              double yJ2,
                              double zJ3,
                              double aJ4,
                              double bJ5,
                              double cJ6)
            : base(xJ1, yJ2, zJ3, aJ4, bJ5, cJ6)
        { }

        /// <summary>
        /// The absolute motion of arm.
        /// </summary>
        /// <param name="position"></param>
        public AbsoluteMotion(double[] position) : base(position)
        { }

        protected override PositionType PositionType => PositionType.Absolute;

        public override bool Do()
        {
            int returnCode;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_pos(ArmId,
                                                SmoothTypeCode,
                                                Position);
                    break;

                case CoordinateType.Descartes when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_pos(ArmId,
                                                SmoothTypeCode,
                                                SmoothValue,
                                                Position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_axis(ArmId,
                                                 SmoothTypeCode,
                                                 Position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_axis(ArmId,
                                                 SmoothTypeCode,
                                                 SmoothValue,
                                                 Position);
                    break;

                default:
                    return false;
            }
            return IsSuccessful(returnCode);
        }

        /// <summary>
        /// Arm jog.
        /// </summary>
        public class Jog : IArmAction
        {
            /// <summary>
            /// Arm jog. Input example: +x<br/>
            /// Input regex: <c>[+-][a-cx-zA-CX-Z]</c>
            /// </summary>
            /// <param name="axis"></param>
            /// <exception cref="ArgumentException">
            /// Input regex: <c>[+-][a-cx-zA-CX-Z]</c>
            /// </exception>
            public Jog(string axis)
            {
                // Remove all whitespace char.
                axis = Regex.Replace(axis, @"\s", "");
                if (CheckArgs(axis))
                {
                    ParseDir(axis);
                    ParseAxis(axis);
                }
                else
                {
                    throw new ArgumentException();
                }

                Message = $"Arm jog. {axis}.";
            }

            public string Message { get; private set; }

            public bool NeedWait
            {
                get => false;
                set { }
            }

            private int _direction;
            private int _axisIndex;

            private bool CheckArgs(string axis)
            {
                if (Regex.IsMatch(axis, "[+-][a-cx-zA-CX-Z]"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private void ParseAxis(string axis)
            {
                int val;
                switch (axis.Substring(1, 1).ToLower())
                {
                    case "x":
                        val = 0;
                        break;

                    case "y":
                        val = 1;
                        break;

                    case "z":
                        val = 2;
                        break;

                    case "a":
                        val = 3;
                        break;

                    case "b":
                        val = 4;
                        break;

                    case "c":
                        val = 5;
                        break;

                    default:
                        throw new ArgumentException();
                }
                _axisIndex = val;
            }

            private void ParseDir(string axis)
            {
                if (axis.Substring(0, 1) == "+")
                {
                    _direction = 1;
                }
                else if (axis.Substring(0, 1) == "-")
                {
                    _direction = -1;
                }
            }

            public int ArmId { get; set; }

            public bool Do()
            {
                // type = 0: Base coor.
                var returnCode = HRobot.jog(ArmId, 0, _axisIndex, _direction);

                // Return 0: successful.
                return returnCode == 0;
            }
        }
    }
}