using System;
using System.Threading;
using System.Windows.Forms;
using SDKHrobot;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.Hiwin
{
    public class HiwinConnection : HiwinBasicAction, IConnection
    {
        private static readonly HRobot.CallBackFun _callBackFun = EventFun;
        private static unsafe bool* _waiting;
        private static unsafe int* _idPointer;
        private const int _defaultID = -99;
        private readonly string _ip;

        public HiwinConnection(string ip,
                               IMessage message,
                               ref int id,
                               ref bool waiting)
            : base(_defaultID, message)
        {
            _ip = ip;

            unsafe
            {
                fixed (int* i = &id)
                {
                    _idPointer = i;
                }
            }

            unsafe
            {
                fixed (bool* w = &waiting)
                {
                    _waiting = w;
                }
            }
        }

        public void Open()
        {
            _id = HRobot.open_connection(_ip, 1, _callBackFun);

            // Check connection.
            if (_id >= 0 && _id <= 65535)
            {
                ShowSuccessfulConnectMessage();
            }
            else
            {
                ShowUnsuccessfulConnectMessage();
            }

            unsafe
            {
                *_idPointer = _id;
            }
        }

        public void Close()
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
        }

        public bool IsOpen => HRobot.network_get_state(_id) == 1;

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

                case _defaultID:
                    errorMessage = $"{_id}：自定義錯誤碼，預設 ID。從未進行連線，請檢測程式。";
                    break;

                default:
                    errorMessage = $"未知的錯誤代碼： {_id}";
                    break;
            }
            _message.Show($"無法連線!\r\n{errorMessage}", LoggingLevel.Error);
        }
    }
}