using System;
using System.Text.RegularExpressions;
using Basic.Message;
using SDKHrobot;

namespace RASDK.Arm.Hiwin
{
    public class HiwinJog : HiwinBasicMotion, IJog
    {
        public HiwinJog(string axis,
                        int id,
                        IMessage message,
                        ref bool waitingState,
                        bool needWait = true)
            : base(0,
                   0,
                   0,
                   0,
                   0,
                   0,
                   id,
                   message,
                   ref waitingState,
                   null)
        {
            NeedWait = needWait;

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

        private readonly string InputRegexPattern = "[+-][a-cx-zA-CX-Z0-5]";

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
    }
}