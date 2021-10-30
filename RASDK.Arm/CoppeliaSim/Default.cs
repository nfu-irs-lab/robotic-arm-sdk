namespace RASDK.Arm.CoppeliaSim
{
    public static class Default
    {
        public const string Ip = "127.0.0.1";

        public const string ObjectName = "UR5";

        public const int Port = 3000;

        //public static readonly double[] DescartesHomePosition = { 0, 368, 294, 180, 0, 90 };
        public static readonly double[] JointHomePosition = { 0, 0, 0, 0, 0, 0 };
    }
}