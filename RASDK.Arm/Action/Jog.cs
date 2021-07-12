using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SDKHrobot;

namespace RASDK.Arm.Action
{
    /// <summary>
    /// RASDK.Arm jog.
    /// </summary>
    public class Jog : IArmAction
    {
        private int _axisIndex;

        private int _direction;

        /// <summary>
        /// RASDK.Arm jog. Input example: +x<br/>
        /// Input regex: <c>[+-][a-cx-zA-CX-Z0-5]</c>
        /// </summary>
        /// <param name="axis"></param>
        /// <exception cref="ArgumentException">
        /// Input regex: <c>[+-][a-cx-zA-CX-Z0-5]</c>
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

            Message = $"RASDK.Arm jog. {axis}.";
        }

        public int ArmId { get; set; }
        public string Message { get; private set; }

        public bool NeedWait
        {
            get => false;
            set { }
        }

        public bool Do()
        {
            // type = 0: Base coor.
            var returnCode = HRobot.jog(ArmId, 0, _axisIndex, _direction);

            // Return 0: successful.
            return returnCode == 0;
        }

        private bool CheckArgs(string axis)
        {
            if (Regex.IsMatch(axis, "[+-][a-cx-zA-CX-Z0-5]"))
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
            switch (axis.Substring(1, 1))
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
    }
}