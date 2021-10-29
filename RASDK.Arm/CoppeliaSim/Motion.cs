using RASDK.Arm.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.CoppeliaSim
{
    internal class Motion : IMotion
    {
        public void Abort()
        {
            throw new NotImplementedException();
        }

        public void Absolute(double[] position, AdditionalMotionParameters addPara = null)
        {
            throw new NotImplementedException();
        }

        public void Absolute(double xJ1, double yJ2, double zJ3, double aJ4, double bJ5, double cJ6, AdditionalMotionParameters addPara = null)
        {
            throw new NotImplementedException();
        }

        public void Homing(bool slowly = true, CoordinateType coordinateType = CoordinateType.Descartes, bool needWait = true)
        {
            throw new NotImplementedException();
        }

        public void Jog(string axis)
        {
            throw new NotImplementedException();
        }

        public void Relative(double[] position, AdditionalMotionParameters addPara = null)
        {
            throw new NotImplementedException();
        }

        public void Relative(double xJ1, double yJ2, double zJ3, double aJ4, double bJ5, double cJ6, AdditionalMotionParameters addPara = null)
        {
            throw new NotImplementedException();
        }
    }
}