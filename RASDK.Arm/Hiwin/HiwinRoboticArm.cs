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

namespace RASDK.Arm
{
    public class HiwinRoboticArm : RoboticArm
    {
        private static unsafe bool* _waiting;
        private readonly string _ip;
        private int _id;

        public HiwinRoboticArm(string ip, IMessage message) : base(message)
        {
            _ip = ip;
        }

        public int Id => _id;
        public string Ip => _ip;

        public override double[] GetNowPosition(CoordinateType coordinate = CoordinateType.Descartes)
        {
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
            ReturnCodeCheck.IsSuccessful(returnCode, _message);
            return position;
        }

        #region Motion

        public override void Abort()
        {
            HRobot.motion_abort(_id);
        }

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

        public override void Jog(string axis)
        {
            // Remove all whitespace char.
            axis = Regex.Replace(axis, @"\s", "");

            if (CheckJogArg(axis))
            {
                HRobot.jog(_id, 0, PatseAxis(axis), ParseDirection(axis));
            }
            else
            {
                throw new ArgumentException($"Input regex: {_inputRegexPattern}");
            }
        }

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
                unsafe
                {
                    *_waiting = true;
                    while (*_waiting)
                    { /* None. */ }
                }
            }
        }

        #endregion Motion

        #region Connect/Disconnect

        private static readonly HRobot.CallBackFun _callBackFun = EventFun;

        public override bool Connected
        {
            get
            {
                bool connected = false;
                try
                {
                    connected = HRobot.network_get_state(_id) == 1;
                }
                catch (Exception)
                { /* None. */ }
                return connected;
            }
        }

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

            return Connected;
        }

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

            return !Connected;
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

                    unsafe
                    {
                        // Motion state=1: Idle.
                        *_waiting = (infos[8] != "1");
                    }
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