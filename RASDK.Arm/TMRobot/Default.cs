namespace RASDK.Arm.TMRobot
{
    public static class Default
    {
        public const string IpOfArmConnection = "169.254.119.180";
        public const int PortOfArmConnection = 5890;
        public static readonly double[] DescartesHomePosition = { 519, -122, 458, 185, 0, 90 };
        public static readonly double[] JointHomePosition = { 0, 0, 0, 0, 0, 0 };
        public static readonly int SpeedOfHomingSlowly = 10;
    }
}