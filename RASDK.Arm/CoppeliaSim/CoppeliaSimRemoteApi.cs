using System;
using System.Runtime.InteropServices;

namespace RASDK.Arm.CoppeliaSim
{
    internal class CoppeliaSimRemoteApi
    {
        [DllImport("CoppeliaSimRemoteAPI.dll")]
        public static extern int Connect(string ip, int port);

        [DllImport("CoppeliaSimRemoteAPI.dll")]
        public static extern int Disconnect(int id);

        [DllImport("CoppeliaSimRemoteAPI.dll")]
        public static extern int GetJointPosition(int id,
            int[] jointHandles,
            float[] position);

        [DllImport("CoppeliaSimRemoteAPI.dll")]
        public static extern int GetObjectHandle(int id, string name);

        [DllImport("CoppeliaSimRemoteAPI.dll")]
        public static extern int MoveJoint(int id,
            float[] position,
            int[] jointHandles,
            bool inTorqueForceMode);
    }
}