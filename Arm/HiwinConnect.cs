//#define DISABLE_SHOW_MESSAGE

// 選擇一種等待手臂動作完成的做法，都不選就是使用預設方法。
#define USE_CALLBACK_MOTION_STATE_WAIT
// #define USE_MOTION_STATE_WAIT

#if (DISABLE_SHOW_MESSAGE)
#warning Message is disabled.
#endif


using System;
using System.Threading;
using System.Windows.Forms;
using SDKHrobot;
using Basic;
using Basic.Message;

namespace Arm
{
    public class HiwinConnect : Connect
    {
        private static readonly HRobot.CallBackFun _callBackFun = EventFun;

        private readonly IMessage _message;

        public static bool Waiting { get; private set; } = false;

        public int Id { get; }

        public HiwinConnect(string ip,
                            IMessage message,
                            out int id,
                            out bool connected,
                            ref bool waiting)
        {
            _message = message;
            Id = HRobot.open_connection(ip, 1, _callBackFun);

            // Check connection.
            if (Id >= 0 && Id <= 65535)
            {
                ShowSuccessfulConnectMessage();
                connected = true;
            }
            else
            {
                ShowUnsuccessfulConnectMessage();
                connected = false;
            }

            id = Id;
            waiting = Waiting;
        }

        private void ShowSuccessfulConnectMessage()
        {
            int alarmState;
            int motorState;
            int connectionLevel;

            // 將所有錯誤代碼清除。
            alarmState = HRobot.clear_alarm(Id);

            // 錯誤代碼300代表沒有警報，無法清除警報。
            alarmState = alarmState == 300 ? 0 : alarmState;

            // 設定控制器: 1為啟動,0為關閉。
            HRobot.set_motor_state(Id, 1);
            Thread.Sleep(500);

            // 取得控制器狀態。
            motorState = HRobot.get_motor_state(Id);

            // 取得連線等級。
            connectionLevel = HRobot.get_connection_level(Id);

            var text = "連線成功!\r\n" +
                       $"手臂ID: {Id}\r\n" +
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
            switch (Id)
            {
                case -1:
                    errorMessage = "-1：連線失敗。";
                    break;

                case -2:
                    errorMessage = "-2：回傳機制創建失敗。";
                    break;

                case -3:
                    errorMessage = "-3：無法連線至Robot。";
                    break;

                case -4:
                    errorMessage = "-4：版本不相符。";
                    break;

                default:
                    errorMessage = $"未知的錯誤代碼： {Id}";
                    break;
            }
            _message.Show($"無法連線!\r\n{errorMessage}", LoggingLevel.Error);
        }

        private static void EventFun(UInt16 cmd, UInt16 rlt, ref UInt16 Msg, int len)
        {
            // 該 Method 的內容請參考 HRSDK-SampleCode： 11.CallbackNotify。
            // 此處不受 IMessage 影響。

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

#if (USE_CALLBACK_MOTION_STATE_WAIT)
                    // Motion state=1: Idle.
                    Waiting = infos[8] != "1";
#endif
                    break;

                case 4011 when rlt != 0:
#if (!DISABLE_SHOW_MESSAGE)
                    MessageBox.Show("Update fail. " + rlt,
                                    "HRSS update callback",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
#endif
                    break;
            }
        }
    }
}