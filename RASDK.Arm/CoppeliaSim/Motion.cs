using RASDK.Arm.Type;
using RASDK.Basic.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.CoppeliaSim
{
    internal class Motion : BasicAction, IMotion
    {
        private readonly string _objectName;
        private int[] _jointHandles = new int[] { -1, -1, -1, -1, -1, -1 };

        public Motion(string objectName, int id, IMessage message) : base(id, message)
        {
            _objectName = objectName;
            UpdateJointHandles();
        }

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

        private void UpdateJointHandles()
        {
            for (var i = 0; i < _jointHandles.Length; i++)
            {
                var jointName = $"{_objectName}_joint{i + 1}";
                _jointHandles[i] = CoppeliaSimRemoteApi.GetObjectHandle(_id, jointName);
            }
        }
    }
}