using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RASDK.Basic;
using RASDK.Arm.Type;
using System.Text.RegularExpressions;
using RASDK.Basic.Message;

namespace RASDK.Arm
{
    public abstract class RoboticArm : IDevice
    {
        protected readonly IMessage _message;

        public RoboticArm(IMessage message)
        {
            _message = message;
        }

        ~RoboticArm()
        {
            try
            {
                if (Connected)
                {
                    Disconnect();
                }
            }
            catch (Exception)
            { /* None.*/ }
        }

        public abstract double[] GetNowPosition(CoordinateType coordinate = CoordinateType.Descartes);

        #region Speed/Acceleration

        public abstract double Acceleration { get; set; }
        public abstract double Speed { get; set; }

        #endregion Speed/Acceleration

        #region Motion

        public abstract void Abort();

        public abstract void Homing(bool slowly = true,
                                    CoordinateType coordinate = CoordinateType.Descartes,
                                    bool needWait = true);

        #region Motion.Absolute

        public abstract void MoveAbsolute(double j1X,
                                          double j2Y,
                                          double j3Z,
                                          double j4A,
                                          double j5B,
                                          double j6C,
                                          AdditionalMotionParameters addParams = null);

        public virtual void MoveAbsolute(double[] position,
                                         AdditionalMotionParameters addParams = null)
        {
            if (position.Length == 6)
            {
                MoveAbsolute(position[0],
                             position[1],
                             position[2],
                             position[3],
                             position[4],
                             position[5],
                             addParams);
            }
            else
            {
                throw new ArgumentException("The length of position must be 6.");
            }
        }

        #endregion Motion.Absolute

        #region Motion.Relative

        public abstract void MoveRelative(double j1X,
                                          double j2Y,
                                          double j3Z,
                                          double j4A = 0,
                                          double j5B = 0,
                                          double j6C = 0,
                                          AdditionalMotionParameters addParams = null);

        public void MoveRelative(double j1X,
                                 double j2Y,
                                 double j3Z,
                                 AdditionalMotionParameters addParams = null)
        {
            MoveRelative(j1X, j2Y, j3Z, 0, 0, 0, addParams);
        }

        public virtual void MoveRelative(double[] position,
                                         AdditionalMotionParameters addParams = null)
        {
            if (position.Length == 6)
            {
                MoveRelative(position[0],
                             position[1],
                             position[2],
                             position[3],
                             position[4],
                             position[5],
                             addParams);
            }
            else if (position.Length == 3)
            {
                MoveRelative(position[0],
                             position[1],
                             position[2],
                             addParams);
            }
            else
            {
                throw new ArgumentException("The length of position must be 3 or 6.");
            }
        }

        #endregion Motion.Relative

        #region Motion.Jog

        protected readonly string _inputRegexPattern = "[+-][a-cx-zA-CX-Z0-5]";

        public abstract void Jog(string axis);

        protected bool CheckJogArg(string text)
        {
            return Regex.IsMatch(text, _inputRegexPattern);
        }

        protected int ParseDirection(string text)
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

        protected int PatseAxis(string text)
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

        #endregion Motion.Jog

        #endregion Motion

        #region Connect/Disconnect

        // IDevice

        public abstract bool Connected { get; }

        public abstract bool Connect();

        public abstract bool Disconnect();

        #endregion Connect/Disconnect
    }
}