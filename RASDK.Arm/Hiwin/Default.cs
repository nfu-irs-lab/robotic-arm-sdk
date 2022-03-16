namespace RASDK.Arm.Hiwin
{
    /// <summary>
    /// 預設的數值。
    /// </summary>
    public static class Default
    {
        /// <summary>
        /// 手臂連線 IP。
        /// </summary>
        public const string Ip = "127.0.0.1";

        /// <summary>
        /// 啓動時的加速度。
        /// </summary>
        public static readonly int AccelerationOfPowerOn = 20;

        /// <summary>
        /// 笛卡爾座標原點位置陣列。
        /// </summary>
        public static readonly double[] DescartesHomePosition = { 0, 368, 294, 180, 0, 90 };

        /// <summary>
        /// 關節座標原點位置陣列。
        /// </summary>
        public static readonly double[] JointHomePosition = { 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// 慢速回原點的速度。
        /// </summary>
        public static readonly int SpeedOfHomingSlowly = 5;

        /// <summary>
        /// 啓動時的速度。
        /// </summary>
        public static readonly int SpeedOfPowerOn = 10;
    }
}