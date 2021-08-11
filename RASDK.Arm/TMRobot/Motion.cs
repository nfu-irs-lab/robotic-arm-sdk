using RASDK.Arm.Type;

namespace RASDK.Arm.TMRobot
{
    public class Motion : IMotion
    {
        public void Abort()
        {
            throw new System.NotImplementedException();
        }

        public void Absolute(double[] position, AdditionalMotionParameters addPara = null)
        {
            throw new System.NotImplementedException();
        }

        public void Absolute(double xJ1,
                             double yJ2,
                             double zJ3,
                             double aJ4,
                             double bJ5,
                             double cJ6,
                             AdditionalMotionParameters addPara = null)
        {
            throw new System.NotImplementedException();
        }

        public void Relative(double[] position, AdditionalMotionParameters addPara = null)
        {
            throw new System.NotImplementedException();
        }

        public void Relative(double xJ1,
                             double yJ2,
                             double zJ3,
                             double aJ4,
                             double bJ5,
                             double cJ6,
                             AdditionalMotionParameters addPara = null)
        {
            throw new System.NotImplementedException();
        }

        public void Homing(CoordinateType coordinateType = CoordinateType.Descartes, bool needWait = true)
        {
            throw new System.NotImplementedException();
        }

        public void Jog(string axis)
        {
            throw new System.NotImplementedException();
        }
    }
}