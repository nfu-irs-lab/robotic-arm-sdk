using SDKHrobot;
using System.Threading;
using System.Windows.Forms;
using Basic.Message;

namespace Arm
{
    public class HiwinDisconnect : Disconnect
    {
        public HiwinDisconnect(int id, IMessage message, out bool connected)
        {
            int alarmState;
            int motorState;

            // 將所有錯誤代碼清除。
            alarmState = HRobot.clear_alarm(id);

            // 錯誤代碼300代表沒有警報，無法清除警報。
            alarmState = alarmState == 300 ? 0 : alarmState;

            // 設定控制器: 1為啟動,0為關閉。
            HRobot.set_motor_state(id, 0);
            Thread.Sleep(500);

            // 取得控制器狀態。
            motorState = HRobot.get_motor_state(id);

            // 關閉手臂連線。
            HRobot.disconnect(id);

            var text = "斷線成功!\r\n" +
                       $"控制器狀態: {(motorState == 0 ? "關閉" : "開啟")}\r\n" +
                       $"錯誤代碼: {alarmState}";
            message.Show(text, "斷線", MessageBoxButtons.OK, MessageBoxIcon.None);

            connected = false;
        }
    }
}