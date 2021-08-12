using RASDK.Arm.Type;

namespace RASDK.Arm
{
    public class AdditionalMotionParameters
    {
        public MotionType MotionType = MotionType.PointToPoint;
        public CoordinateType CoordinateType = CoordinateType.Descartes;
        public SmoothType SmoothType = SmoothType.TwoLinesSpeedSmooth;
        public int SmoothValue = 50;
        public bool NeedWait = true;
    }
}