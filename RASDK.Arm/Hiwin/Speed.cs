using System.Windows.Forms;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.Hiwin
{
    internal class Speed : BasicAction
    {
        public Speed(int id, IMessage message) : base(id, message)
        { }

        public int Value
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
                    var returnCode = HRobot.set_override_ratio(_id, value);
                    ReturnCodeCheck.IsSuccessful(returnCode, _message);
                }
            }
        }
    }
}