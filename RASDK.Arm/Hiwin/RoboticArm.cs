using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RASDK.Arm.Hiwin;
using RASDK.Arm.Type;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.Hiwin
{
    /// <summary>
    /// 上銀機械手臂。
    /// </summary>
    public class RoboticArm : RASDK.Arm.RoboticArm
    {
        private static volatile bool _waiting;
        private readonly string _ip;
        private int _id;

        /// <summary>
        /// 上銀機械手臂。
        /// </summary>
        /// <param name="message">訊息處理器。</param>
        /// <param name="ip">手臂連線 IP。例如 <c>"192.168.0.3"</c>。</param>
        public RoboticArm(MessageHandler message, string ip = Default.Ip) : base(message)
        {
            _ip = ip;
        }

        /// <summary>
        /// 機械手臂 ID。
        /// </summary>
        public int Id => _id;

        /// <summary>
        /// 取得目前的位置座標。
        /// </summary>
        /// <param name="coordinate">座標系類型。預設爲 <c>Descartes</c>。</param>
        /// <returns>目前的位置座標陣列。</returns>
        /// <exception cref="ArgumentException"></exception>
        public override double[] GetNowPosition(CoordinateType coordinate = CoordinateType.Descartes)
        {
            _message.Log($"執行：{nameof(GetNowPosition)}()，" +
                         $"參數 {nameof(coordinate)}：{coordinate}。",
                         LoggingLevel.Trace);

            var position = new double[6];
            Func<int, double[], int> action;

            switch (coordinate)
            {
                case CoordinateType.Descartes:
                    action = HRobot.get_current_position;
                    break;

                case CoordinateType.Joint:
                    action = HRobot.get_current_joint;
                    break;

                default:
                    throw new ArgumentException("Unknown coordinator type.");
            }

            var returnCode = action(_id, position);
            var noError = ReturnCodeCheck.IsSuccessful(returnCode, _message);

            _message.Log($"完成：{nameof(GetNowPosition)}()，" +
                         $"回傳：{position[0]},{position[1]},{position[2]},{position[3]},{position[4]},{position[5]}，" +
                         $"錯誤代碼：{returnCode}。",
                         noError ? LoggingLevel.Trace : LoggingLevel.Warn);
            return position;
        }

        #region Motion

        /// <summary>
        /// 中止動作。
        /// </summary>
        public override void Abort()
        {
            HRobot.motion_abort(_id);
        }

        /// <summary>
        /// 復歸，回原點。
        /// </summary>
        /// <param name="slowly">是否要慢速移動。</param>
        /// <param name="coordinate">座標系類型。</param>
        /// <param name="needWait">是否需要等待動作完成 (阻塞)。</param>
        /// <exception cref="ArgumentException"></exception>
        public override void Homing(bool slowly = true, CoordinateType coordinate = CoordinateType.Descartes, bool needWait = true)
        {
            var oriSpeedValue = Speed;
            if (slowly)
            {
                Speed = Default.SpeedOfHomingSlowly;
            }

            int retuenCode;
            switch (coordinate)
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

            WaitForMotionComplete(needWait, retuenCode);

            if (slowly)
            {
                Speed = oriSpeedValue;
            }
        }

        /// <summary>
        /// 吋動。
        /// </summary>
        /// <param name="axis">目標軸及方向。例如 <c>"+x"</c> 爲 X 軸增加。</param>
        /// <exception cref="ArgumentException"></exception>
        public override void Jog(string axis)
        {
            // Remove all whitespace char.
            axis = Regex.Replace(axis, @"\s", "");

            if (CheckJogArg(axis))
            {
                HRobot.jog(_id, 0, ParseAxis(axis), ParseDirection(axis));
            }
            else
            {
                throw new ArgumentException($"Input regex: {_inputRegexPattern}");
            }
        }

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
        public override void MoveAbsolute(double j1X,
                                          double j2Y,
                                          double j3Z,
                                          double j4A,
                                          double j5B,
                                          double j6C,
                                          AdditionalMotionParameters addParams = null)
        {
            addParams = addParams ?? new AdditionalMotionParameters();
            var position = new double[] { j1X, j2Y, j3Z, j4A, j5B, j6C };
            var smoothTypeCode = GetSmoothTypeCode(addParams.SmoothType, addParams.MotionType);
            int returnCode = 0;

            switch (addParams.CoordinateType)
            {
                case CoordinateType.Descartes when addParams.MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_pos(_id,
                                                smoothTypeCode,
                                                position);
                    break;

                case CoordinateType.Descartes when addParams.MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_pos(_id,
                                                smoothTypeCode,
                                                addParams.SmoothValue,
                                                position);
                    break;

                case CoordinateType.Joint when addParams.MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_axis(_id,
                                                 smoothTypeCode,
                                                 position);
                    break;

                case CoordinateType.Joint when addParams.MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_axis(_id,
                                                 smoothTypeCode,
                                                 addParams.SmoothValue,
                                                 position);
                    break;
            }

            WaitForMotionComplete(addParams.NeedWait, returnCode);
        }

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
        public override void MoveRelative(double j1X,
                                          double j2Y,
                                          double j3Z,
                                          double j4A = 0,
                                          double j5B = 0,
                                          double j6C = 0,
                                          AdditionalMotionParameters addParams = null)
        {
            addParams = addParams ?? new AdditionalMotionParameters();
            var position = new double[] { j1X, j2Y, j3Z, j4A, j5B, j6C };
            var smoothTypeCode = GetSmoothTypeCode(addParams.SmoothType, addParams.MotionType);
            int returnCode = 0;

            switch (addParams.CoordinateType)
            {
                case CoordinateType.Descartes when addParams.MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_pos(_id,
                                                    smoothTypeCode,
                                                    position);
                    break;

                case CoordinateType.Descartes when addParams.MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_pos(_id,
                                                    smoothTypeCode,
                                                    addParams.SmoothValue,
                                                    position);
                    break;

                case CoordinateType.Joint when addParams.MotionType == MotionType.PointToPoint:
                    returnCode = HRobot.ptp_rel_axis(_id,
                                                     smoothTypeCode,
                                                     position);
                    break;

                case CoordinateType.Joint when addParams.MotionType == MotionType.Linear:
                    returnCode = HRobot.lin_rel_axis(_id,
                                                     smoothTypeCode,
                                                     addParams.SmoothValue,
                                                     position);
                    break;
            }

            WaitForMotionComplete(addParams.NeedWait, returnCode);
        }

        private int GetSmoothTypeCode(SmoothType smoothType, MotionType motionType)
        {
            int code = 0;
            switch (motionType)
            {
                case MotionType.Circle:
                case MotionType.PointToPoint:
                    code = (smoothType == SmoothType.TwoLinesSpeedSmooth) ? 1 : 0;
                    break;

                case MotionType.Linear:
                    code = (int)smoothType;
                    break;
            }
            return code;
        }

        private void WaitForMotionComplete(bool needWait, int returnCode)
        {
            if (needWait && ReturnCodeCheck.IsSuccessful(returnCode))
            {
                _waiting = true;
                while (_waiting)
                { /* Do nothing. */ }
            }
        }

        #endregion Motion

        #region Connect/Disconnect

        private static readonly HRobot.CallBackFun _callBackFun = EventFun;

        // FIXME: HRobot.network_get_state(_id) always return 0, so Connected always false.

        /// <summary>
        /// 是否已連線。
        /// </summary>
        public override bool Connected
        {
            get
            {
                bool connected;
                try
                {
                    connected = HRobot.network_get_state(_id) == 1;
                }
                catch (Exception)
                {
                    connected = false;
                }
                return connected;
            }
        }

        /// <summary>
        /// 進行連線。
        /// </summary>
        /// <returns>是否連線成功。</returns>
        public override bool Connect()
        {
            _id = HRobot.open_connection(_ip, 1, _callBackFun);

            // Check connection.
            if (_id >= 0 && _id <= 65535)
            {
                Speed = Default.SpeedOfPowerOn;
                Acceleration = Default.AccelerationOfPowerOn;

                ShowSuccessfulConnectMessage();
            }
            else
            {
                ShowUnsuccessfulConnectMessage();
            }

            // FIXME: HRobot.network_get_state(_id) always return 0, so the Connected always false.
            //return Connected;
            return true;
        }

        /// <summary>
        /// 進行斷線。
        /// </summary>
        /// <returns>是否斷線成功。</returns>
        public override bool Disconnect()
        {
            int alarmState;
            int motorState;

            // 將所有錯誤代碼清除。
            alarmState = HRobot.clear_alarm(_id);

            // 錯誤代碼300代表沒有警報，無法清除警報。
            alarmState = alarmState == 300 ? 0 : alarmState;

            // 設定控制器: 1為啟動,0為關閉。
            HRobot.set_motor_state(_id, 0);
            Thread.Sleep(500);

            // 取得控制器狀態。
            motorState = HRobot.get_motor_state(_id);

            // 關閉手臂連線。
            HRobot.disconnect(_id);

            var text = "斷線成功!\r\n" +
                       $"控制器狀態: {(motorState == 0 ? "關閉" : "開啟")}\r\n" +
                       $"錯誤代碼: {alarmState}";
            _message.Show(text, "斷線", MessageBoxButtons.OK, MessageBoxIcon.None);

            // FIXME: HRobot.network_get_state(_id) always return 0, so the Connected always false.
            //return !Connected;
            return true;
        }

        private static void EventFun(UInt16 cmd, UInt16 rlt, ref UInt16 Msg, int len)
        {
            // 該 Method 的內容請參考 HRSDK-SampleCode： 11.CallbackNotify。

            // Get info.
            String info = "";
            unsafe
            {
                fixed (UInt16* p = &Msg)
                {
                    for (int i = 0; i < len; i++)
                    {
                        info += (char)p[i];
                    }
                }
            }
            var infos = info.Split(',');

            // 此處不受 IMessage 影響。
            // Show in Console.
            Console.WriteLine($"Command:{cmd}, Result:{rlt}");
            switch (cmd)
            {
                case 0 when rlt == 4702:
                    Console.WriteLine($"HRSS Mode:{infos[0]}\r\n" +
                                      $"Operation Mode:{infos[1]}\r\n" +
                                      $"Override Ratio:{infos[2]}\r\n" +
                                      $"Motor State:{infos[3]}\r\n" +
                                      $"Exe File Name:{infos[4]}\r\n" +
                                      $"Function Output:{infos[5]}\r\n" +
                                      $"Alarm Count:{infos[6]}\r\n" +
                                      $"Keep Alive:{infos[7]}\r\n" +
                                      $"Motion Status:{infos[8]}\r\n" +
                                      $"Payload:{infos[9]}\r\n" +
                                      $"Speed:{infos[10]}\r\n" +
                                      $"Position:{infos[11]}\r\n" +
                                      $"Coor:{infos[14]},{infos[15]},{infos[16]},{infos[17]},{infos[18]},{infos[19]}\r\n" +
                                      $"Joint:{infos[20]},{infos[21]},{infos[22]},{infos[23]},{infos[24]},{infos[25]}\r\n");

                    // Motion state=1: Idle.
                    _waiting = (infos[8] != "1");
                    break;

                case 4011 when rlt != 0:
                    MessageBox.Show("Update fail. " + rlt,
                                    "HRSS update callback",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
                    break;
            }
        }

        private void ShowSuccessfulConnectMessage()
        {
            int alarmState;
            int motorState;
            int connectionLevel;

            // 將所有錯誤代碼清除。
            alarmState = HRobot.clear_alarm(_id);

            // 錯誤代碼300代表沒有警報，無法清除警報。
            alarmState = alarmState == 300 ? 0 : alarmState;

            // 設定控制器: 1為啟動,0為關閉。
            HRobot.set_motor_state(_id, 1);
            Thread.Sleep(500);

            // 取得控制器狀態。
            motorState = HRobot.get_motor_state(_id);

            // 取得連線等級。
            connectionLevel = HRobot.get_connection_level(_id);

            var text = "連線成功!\r\n" +
                       $"手臂ID: {_id}\r\n" +
                       $"連線等級: {(connectionLevel == 0 ? "觀測者" : "操作者")}\r\n" +
                       $"控制器狀態: {(motorState == 0 ? "關閉" : "開啟")}\r\n" +
                       $"錯誤代碼: {alarmState}";
            _message.Show(text,
                          "連線",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.None);
        }

        private void ShowUnsuccessfulConnectMessage()
        {
            string errorMessage;
            switch (_id)
            {
                case -1:
                    errorMessage = $"{_id}：連線失敗。";
                    break;

                case -2:
                    errorMessage = $"{_id}：回傳機制創建失敗。";
                    break;

                case -3:
                    errorMessage = $"{_id}：無法連線至Robot。";
                    break;

                case -4:
                    errorMessage = $"{_id}：版本不相符。";
                    break;

                default:
                    errorMessage = $"未知的錯誤代碼： {_id}";
                    break;
            }
            _message.Show($"無法連線!\r\n{errorMessage}", LoggingLevel.Error);
        }

        #endregion Connect/Disconnect

        #region Speed/Acceleration

        /// <summary>
        /// 加速度。單位爲 %。<br/>
        /// 容許的數值範圍爲 1 ~ 100。<br/>
        /// 回傳 -1 代表取得數值時錯誤。
        /// </summary>
        public override double Acceleration
        {
            get
            {
                int acc;
                acc = HRobot.get_acc_dec_ratio(_id);
                if (acc == -1)
                {
                    _message.Show("取得手臂加速度時出錯。", LoggingLevel.Error);
                }
                return acc;
            }

            set
            {
                if (value < 1 || value > 100)
                {
                    _message.Show($"手臂加速度應為1% ~ 100%之間。輸入值為：{value}",
                                  LoggingLevel.Warn);
                }
                else
                {
                    var returnCode = HRobot.set_acc_dec_ratio(_id, (int)value);

                    // 執行HRobot.set_acc_dec_ratio時會固定回傳錯誤代碼4000。
                    ReturnCodeCheck.IsSuccessful(returnCode, _message, 4000);
                }
            }
        }

        /// <summary>
        /// 速度。單位爲 %。<br/>
        /// 容許的數值範圍爲 1 ~ 100。<br/>
        /// 回傳 -1 代表取得數值時錯誤。
        /// </summary>
        public override double Speed
        {
            get
            {
                var speed = HRobot.get_override_ratio(_id);
                if (speed == -1)
                {
                    _message.Show("取得手臂速度時出錯。", LoggingLevel.Error);
                }
                return speed;
            }

            set
            {
                if (value < 1 || value > 100)
                {
                    _message.Show($"手臂速度應為1% ~ 100%之間。輸入值為：{value}",
                                  LoggingLevel.Warn);
                }
                else
                {
                    var returnCode = HRobot.set_override_ratio(_id, (int)value);
                    ReturnCodeCheck.IsSuccessful(returnCode, _message);
                }
            }
        }

        #endregion Speed/Acceleration
    }
}