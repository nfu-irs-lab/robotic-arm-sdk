using System;
using System.Text.RegularExpressions;
using RASDK.Arm.Hiwin;
using RASDK.Arm.Type;

namespace RASDK.Arm.TMRobot
{
    internal class Motion : IMotion
    {
        private CommandSender _commandSender;
        private CoordinateType _coordinateType = CoordinateType.Descartes;
        private MotionType _motionType = MotionType.PointToPoint;
        private int _speed;
        private int _acceleration;

        private AdditionalMotionParameters _additionalMotionParameters
        {
            set
            {
                if (value != null)
                {
                    _motionType = value.MotionType;
                    _coordinateType = value.CoordinateType;
                }
            }
        }

        public Motion(double speed, double acceleration, SocketClientObject socketClientObject)
        {
            _speed = (int)Math.Round(speed);
            _acceleration = (int)Math.Round(acceleration);

            _commandSender = new CommandSender(socketClientObject);
        }

        public void Abort()
        {
            _commandSender.Send(@"1,StopAndClearBuffer()");
        }

        public void Absolute(double[] position, AdditionalMotionParameters addPara = null)
        {
            if (position.Length != 6)
            {
                throw new ArgumentException("Length of position must be 6");
            }

            _additionalMotionParameters = addPara;

            string positionString = "";
            foreach (var p in position)
            {
                positionString += p.ToString();
                positionString += ',';
            }

            var coordianteTypeChar = _coordinateType == CoordinateType.Descartes ? 'C' : 'J';
            string motionTypeString;
            switch (_motionType)
            {
                case MotionType.PointToPoint:
                    motionTypeString = "PTP";
                    break;

                case MotionType.Linear:
                    motionTypeString = "Line";
                    break;

                case MotionType.Circle:
                    throw new NotImplementedException("還未實作 Circle 的控制方法。");

                default:
                    throw new Exception();
            }

            string command = $"1,{motionTypeString}(\"{coordianteTypeChar}PP\",{positionString}{_speed},{_acceleration},0,false)";
            _commandSender.Send(command);
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

            string positionString = "";
            foreach (var p in position)
            {
                positionString += p.ToString();
                positionString += ',';
            }

            var coordianteTypeChar = _coordinateType == CoordinateType.Descartes ? 'C' : 'J';
            string motionTypeString;
            switch (_motionType)
            {
                case MotionType.PointToPoint:
                    motionTypeString = "Move_PTP";
                    break;

                case MotionType.Linear:
                    motionTypeString = "Move_Line";
                    break;

                case MotionType.Circle:
                    throw new Exception("沒有 Circle 的相對運動方式。");

                default:
                    throw new Exception();
            }

            string command = $"1,{motionTypeString}(\"{coordianteTypeChar}PP\",{positionString}{_speed},{_acceleration},0,false)";
            _commandSender.Send(command);
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

        public void Homing(bool slowly = true, CoordinateType coordinateType = CoordinateType.Descartes, bool needWait = true)
        {
            var homePos = coordinateType == CoordinateType.Descartes ? Default.DescartesHomePosition : Default.JointHomePosition;
            string homePosString = "";
            foreach (var p in homePos)
            {
                homePosString += p.ToString();
                homePosString += ",";
            }

            var speed = slowly ? Default.SpeedOfHomingSlowly : _speed;
            var command = $"1,PTP(\"CPP\",{homePosString}{speed},{_acceleration},0,false)";
            _commandSender.Send(command);
        }

        #region Jog

        private readonly string InputRegexPattern = "[+-][a-cx-zA-CX-Z0-5]";

        public void Jog(string axis)
        {
            // Remove all whitespace char.
            axis = Regex.Replace(axis, @"\s", "");

            if (CheckArgs(axis))
            {
                double[] pos = { 0.0, 0, 0, 0, 0, 0 };
                pos[PatseAxis(axis)] = ParseDirection(axis);
                Relative(pos);
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
            int value = 10;
            if (text.Substring(0, 1) == "+")
            {
                return value;
            }
            else if (text.Substring(0, 1) == "-")
            {
                return -value;
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