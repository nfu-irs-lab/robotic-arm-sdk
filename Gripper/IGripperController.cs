using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basic;

namespace Gripper
{
    /// <summary>
    /// 夾爪控制介面。
    /// </summary>
    public interface IGripperController : IDevice
    {
        string Control(int position,
                       int speed = 50,
                       int force = 70,
                       int CJog = 0,
                       int PushVel = 0,
                       int PushPosStk = 0);

        void Reset();
    }
}