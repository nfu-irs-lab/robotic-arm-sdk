using System.Windows.Forms;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.Hiwin
{
    public class Acceleration : BasicAction
    {
        public Acceleration(int id, IMessage message) : base(id, message)
        { }

        public int Value
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
                    var returnCode = HRobot.set_acc_dec_ratio(_id, value);

                    // 執行HRobot.set_acc_dec_ratio時會固定回傳錯誤代碼4000。
                    ReturnCodeCheck.IsSuccessful(returnCode, _message, 4000);
                }
            }
        }
    }
}