using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.CoppeliaSim
{
    internal class JointHandle
    {
        public static int[] Get(int id, string objectName, int count = 6)
        {
            int[] jh = new int[6];
            for (var i = 0; i < count; i++)
            {
                var jointName = $"{objectName}_joint{i + 1}";
                jh[i] = CoppeliaSimRemoteApi.GetObjectHandle(id, jointName);
            }
            return jh;
        }
    }
}