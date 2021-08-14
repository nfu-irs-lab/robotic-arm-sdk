using System;
using System.Text.RegularExpressions;
using RASDK.Basic.Message;
using RASDK.Arm.Type;

namespace RASDK.Arm.Hiwin
{
    internal class Motion : BasicMotion, IMotion
    {
        public Motion(int id,
                      IMessage message,
                      ref bool waitingState)
            : base(id, message, ref waitingState)
        { }

        public void Abort()
        {
            HRobot.motion_abort(_id);
        }

        public void Absolute(double[] position, AdditionalMotionParameters addPara = null)
        {
            if (position.Length != 6)
            {
                throw new ArgumentException("Length of position must be 6");
            }

            _additionalMotionParameters = addPara;

            int returnCode = 0;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_pos(_id,
                                                _smoothTypeCode,
                                                position);
                    break;

                case CoordinateType.Descartes when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_pos(_id,
                                                _smoothTypeCode,
                                                SmoothValue,
                                                position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_axis(_id,
                                                 _smoothTypeCode,
                                                 position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_axis(_id,
                                                 _smoothTypeCode,
                                                 SmoothValue,
                                                 position);
                    break;
            }

            WaitForMotionComplete(returnCode);
        }

        public void Absolute(double xJ1,
                             double yJ2,
                             double zJ3,
                             double aJ4,
                             double bJ5,
                             double cJ6,
                             AdditionalMotionParameters addPara = null)
        {
            Absolute(new[] { xJ1, yJ2, zJ3, aJ4, bJ5, cJ6 }, addPara);
        }

        public void Relative(double[] position, AdditionalMotionParameters addPara = null)
        {
            if (position.Length != 6)
            {
                throw new ArgumentException("Length of position must be 6");
            }

            _additionalMotionParameters = addPara;

            int returnCode = 0;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_pos(_id,
                                                    _smoothTypeCode,
                                                    position);
                    break;

                case CoordinateType.Descartes when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_pos(_id,
                                                    _smoothTypeCode,
                                                    SmoothValue,
                                                    position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_axis(_id,
                                                     _smoothTypeCode,
                                                     position);
                    break;

                case CoordinateType.Joint when MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_axis(_id,
                                                     _smoothTypeCode,
                                                     SmoothValue,
                                                     position);
                    break;
            }

            WaitForMotionComplete(returnCode);
        }

        public void Relative(double xJ1,
                             double yJ2,
                             double zJ3,
                             double aJ4,
                             double bJ5,
                             double cJ6,
                             AdditionalMotionParameters addPara = null)
        {
            Relative(new[] { xJ1, yJ2, zJ3, aJ4, bJ5, cJ6 }, addPara);
        }


        public void Homing(bool slowly = true,
                           CoordinateType coordinateType = CoordinateType.Descartes,
                           bool needWait = true)
        {
            CoordinateType = coordinateType;
            NeedWait = needWait;

            var speed = new Speed(_id, _message);
            var oriSpeedValue = speed.Value;
            if (slowly)
            {
                speed.Value = Default.SpeedOfHomingSlowly;
            }

            int retuenCode;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes:
                    retuenCode = HRobot.ptp_pos(_id, 0, Default.DescartesHomePosition);
                    break;

                case CoordinateType.Joint:
                    retuenCode = HRobot.ptp_axis(_id, 0, Default.JointHomePosition);
                    break;

                default:
                    throw new ArgumentException("Unknown coordinator type.");
            }

            WaitForMotionComplete(retuenCode);

            if (slowly)
            {
                speed.Value = oriSpeedValue;
            }
        }

        #region Jog

        private readonly string InputRegexPattern = "[+-][a-cx-zA-CX-Z0-5]";

        public void Jog(string axis)
        {
            // Remove all whitespace char.
            axis = Regex.Replace(axis, @"\s", "");

            if (CheckArgs(axis))
            {
                HRobot.jog(_id, 0, PatseAxis(axis), ParseDirection(axis));
            }
            else
            {
                throw new ArgumentException($"Input regex: {InputRegexPattern}");
            }
        }

        private bool CheckArgs(string text)
        {
            return Regex.IsMatch(text, InputRegexPattern);
        }

        private int ParseDirection(string text)
        {
            if (text.Substring(0, 1) == "+")
            {
                return 1;
            }
            else if (text.Substring(0, 1) == "-")
            {
                return -1;
            }
            throw new ArgumentException();
        }

        private int PatseAxis(string text)
        {
            int val;
            switch (text.Substring(1, 1))
            {
                case "x":
                case "X":
                case "0":
                    val = 0;
                    break;

                case "y":
                case "Y":
                case "1":
                    val = 1;
                    break;

                case "z":
                case "Z":
                case "2":
                    val = 2;
                    break;

                case "a":
                case "A":
                case "3":
                    val = 3;
                    break;

                case "b":
                case "B":
                case "4":
                    val = 4;
                    break;

                case "c":
                case "C":
                case "5":
                    val = 5;
                    break;

                default:
                    throw new ArgumentException();
            }
            return val;
        }

        #endregion Jog
    }
}