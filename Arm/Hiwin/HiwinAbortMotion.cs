using Basic.Message;
using SDKHrobot;

namespace Arm.Hiwin
{
    public class HiwinAbortMotion : HiwinBasicAction, IAbortMotion
    {
        public HiwinAbortMotion(int id, IMessage message) : base(id, message)
        {
            HRobot.motion_abort(id);
        }
    }
}