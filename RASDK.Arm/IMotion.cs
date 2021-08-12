using RASDK.Arm.Type;

namespace RASDK.Arm
{
    public interface IMotion
    {
        void Abort();

        void Absolute(double[] position, AdditionalMotionParameters addPara = null);

        void Absolute(double xJ1,
                      double yJ2,
                      double zJ3,
                      double aJ4,
                      double bJ5,
                      double cJ6,
                      AdditionalMotionParameters addPara = null);

        void Relative(double[] position, AdditionalMotionParameters addPara = null);

        void Relative(double xJ1,
                      double yJ2,
                      double zJ3,
                      double aJ4,
                      double bJ5,
                      double cJ6,
                      AdditionalMotionParameters addPara = null);

        void Homing(bool slowly = true, CoordinateType coordinateType = CoordinateType.Descartes, bool needWait = true);

        void Jog(string axis);
    }
}