using RASDK.Basic.Message;
using SDKHrobot;

namespace RASDK.Arm.Hiwin
{
    public class HiwinGetConnectionState : HiwinBasicAction, IGetConnectionState
    {
        public HiwinGetConnectionState(int id, IMessage message, out bool connected) : base(id, message)
        {
            // Return 1: Connected
            // Return 0: Didn't connected.
            connected = HRobot.network_get_state(id) == 1;
        }
    }
}