using RASDK.Arm.Type;

namespace RASDK.Arm
{
    public class AdditionalMotionParameters
    {
        public CoordinateType CoordinateType = CoordinateType.Descartes;
        public MotionType MotionType = MotionType.PointToPoint;
        public bool NeedWait = true;
        public SmoothType SmoothType = SmoothType.TwoLinesSpeedSmooth;
        public int SmoothValue = 50;
    }
}