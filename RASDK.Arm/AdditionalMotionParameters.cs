using RASDK.Arm.Type;

namespace RASDK.Arm
{
    /// <summary>
    /// 額外的機械手臂運動參數。
    /// </summary>
    public class AdditionalMotionParameters
    {
        /// <summary>
        /// 座標類型。
        /// </summary>
        public CoordinateType CoordinateType = CoordinateType.Descartes;

        /// <summary>
        /// 運動類型。
        /// </summary>
        public MotionType MotionType = MotionType.PointToPoint;

        /// <summary>
        /// 手臂平滑模式。
        /// </summary>
        public SmoothType SmoothType = SmoothType.TwoLinesSpeedSmooth;

        /// <summary>
        /// 是否需要等待動作完成（阻塞）。
        /// </summary>
        public bool NeedWait = true;

        /// <summary>
        /// 手臂平滑模式數值。
        /// </summary>
        public int SmoothValue = 50;

        public override string ToString()
        {
            return $"NeedWait: {NeedWait}," +
                   $"CoordinatyType: {CoordinateType}," +
                   $"MotionType: {MotionType}," +
                   $"SmoothType: {SmoothType}, " +
                   $"SmoothValue: {SmoothValue}.";
        }
    }
}