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
    /// <summary>
    /// 機械手臂。
    /// </summary>
    public abstract class RoboticArm : IDevice
    {
        /// <summary>
        /// 訊息處理器。
        /// </summary>
        protected readonly IMessage _message;

        /// <summary>
        /// 機械手臂。
        /// </summary>
        /// <param name="message">訊息處理器。</param>
        public RoboticArm(IMessage message)
        {
            _message = message;
        }

        /// <summary>
        /// 機械手臂解構子。
        /// </summary>
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

        /// <summary>
        /// 取得目前的位置座標。
        /// </summary>
        /// <param name="coordinate">座標系類型。預設爲 <c>Descartes</c>。</param>
        /// <returns>目前的位置座標陣列。</returns>
        public abstract double[] GetNowPosition(CoordinateType coordinate = CoordinateType.Descartes);

        #region Speed/Acceleration

        /// <summary>
        /// 加速度。
        /// </summary>
        public abstract double Acceleration { get; set; }

        /// <summary>
        /// 速度。
        /// </summary>
        public abstract double Speed { get; set; }

        #endregion Speed/Acceleration

        #region Motion

        /// <summary>
        /// 中止動作。
        /// </summary>
        public abstract void Abort();

        /// <summary>
        /// 復歸，回原點。
        /// </summary>
        /// <param name="slowly">是否要慢速移動。</param>
        /// <param name="coordinate">座標系類型。</param>
        /// <param name="needWait">是否需要等待動作完成 (阻塞)。</param>
        public abstract void Homing(bool slowly = true,
                                    CoordinateType coordinate = CoordinateType.Descartes,
                                    bool needWait = true);

        #region Motion.Absolute

        /// <summary>
        /// 絕對運動。
        /// </summary>
        /// <param name="j1X">目標的 J1 或 X 數值。</param>
        /// <param name="j2Y">目標的 J2 或 Y 數值。</param>
        /// <param name="j3Z">目標的 J3 或 Z 數值。</param>
        /// <param name="j4A">目標的 J4 或 A 數值。</param>
        /// <param name="j5B">目標的 J5 或 B 數值。</param>
        /// <param name="j6C">目標的 J6 或 C 數值。</param>
        /// <param name="addParams">額外的運動參數。</param>
        public abstract void MoveAbsolute(double j1X,
                                          double j2Y,
                                          double j3Z,
                                          double j4A,
                                          double j5B,
                                          double j6C,
                                          AdditionalMotionParameters addParams = null);

        /// <summary>
        /// 絕對運動。
        /// </summary>
        /// <param name="position">目標位置座標陣列。陣列長度(Length)需爲 6。</param>
        /// <param name="addParams">額外的運動參數。</param>
        /// <exception cref="ArgumentException">請確定參數 <c>position</c> 的長度(Length)需爲 6。</exception>
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

        /// <summary>
        /// 相對運動。
        /// </summary>
        /// <param name="j1X">目標的 J1 或 X 數值。</param>
        /// <param name="j2Y">目標的 J2 或 Y 數值。</param>
        /// <param name="j3Z">目標的 J3 或 Z 數值。</param>
        /// <param name="j4A">目標的 J4 或 A 數值。</param>
        /// <param name="j5B">目標的 J5 或 B 數值。</param>
        /// <param name="j6C">目標的 J6 或 C 數值。</param>
        /// <param name="addParams">額外的運動參數。</param>
        public abstract void MoveRelative(double j1X,
                                          double j2Y,
                                          double j3Z,
                                          double j4A = 0,
                                          double j5B = 0,
                                          double j6C = 0,
                                          AdditionalMotionParameters addParams = null);

        /// <summary>
        /// 相對運動。
        /// </summary>
        /// <param name="j1X">目標的 J1 或 X 數值。</param>
        /// <param name="j2Y">目標的 J2 或 Y 數值。</param>
        /// <param name="j3Z">目標的 J3 或 Z 數值。</param>
        /// <param name="addParams">額外的運動參數。</param>
        public void MoveRelative(double j1X,
                                 double j2Y,
                                 double j3Z,
                                 AdditionalMotionParameters addParams = null)
        {
            MoveRelative(j1X, j2Y, j3Z, 0, 0, 0, addParams);
        }

        /// <summary>
        /// 相對運動。
        /// </summary>
        /// <param name="position">目標位置座標陣列。陣列長度(Length)需爲 6 或 3。</param>
        /// <param name="addParams">額外的運動參數。</param>
        /// <exception cref="ArgumentException">請確定參數 <c>position</c> 的長度(Length)需爲 6 或 3。</exception>
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

        /// <summary>
        /// 吋動的參數輸入正則表達時判讀模板。
        /// </summary>
        protected readonly string _inputRegexPattern = "[+-][a-cx-zA-CX-Z0-5]";

        /// <summary>
        /// 吋動。
        /// </summary>
        /// <param name="axis">目標軸及方向。例如 <c>"+x"</c> 爲 X 軸增加。</param>
        public abstract void Jog(string axis);

        /// <summary>
        /// 檢查吋動的參數是否合法。
        /// </summary>
        /// <param name="text">吋動參數。</param>
        /// <returns>此輸入參數是否合法。</returns>
        protected bool CheckJogArg(string text)
        {
            return Regex.IsMatch(text, _inputRegexPattern);
        }

        /// <summary>
        /// 解析方向。
        /// </summary>
        /// <param name="text">吋動參數</param>
        /// <returns>方向。</returns>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// 解析軸。
        /// </summary>
        /// <param name="text">吋動參數。</param>
        /// <returns>軸。</returns>
        /// <exception cref="ArgumentException"></exception>
        protected int ParseAxis(string text)
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

        /// <summary>
        /// 是否已連線。
        /// </summary>
        public abstract bool Connected { get; }

        /// <summary>
        /// 進行連線。
        /// </summary>
        /// <returns>是否連線成功。</returns>
        public abstract bool Connect();

        /// <summary>
        /// 進行斷線。
        /// </summary>
        /// <returns>是否斷線成功。</returns>
        public abstract bool Disconnect();

        #endregion Connect/Disconnect
    }
}