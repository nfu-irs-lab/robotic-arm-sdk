namespace RASDK.Arm.Hiwin
{
    public static class Default
    {
        public const string Ip = "127.0.0.1";
        public static readonly int AccelerationOfPowerOn = 20;
        public static readonly double[] DescartesHomePosition = { 0, 368, 294, 180, 0, 90 };
        public static readonly double[] JointHomePosition = { 0, 0, 0, 0, 0, 0 };
        public static readonly int SpeedOfHomingSlowly = 5;
        public static readonly int SpeedOfPowerOn = 10;
    }
}