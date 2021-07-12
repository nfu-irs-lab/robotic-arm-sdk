using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKHrobot;

namespace RASDK.Arm.Action
{
    public class AbortMotion : IArmAction
    {
        public int ArmId { get; set; }
        public string Message => "RASDK.Arm Abort.";

        public bool NeedWait
        {
            get => false;
            set { }
        }

        public bool Do()
        {
            var returnCode = HRobot.motion_abort(ArmId);
            return returnCode == 0;
        }
    }
}