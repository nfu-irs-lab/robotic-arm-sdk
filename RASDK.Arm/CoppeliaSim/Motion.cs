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
        private CoordinateType CoordinateType = CoordinateType.Descartes;

        public Motion(string objectName, int id, IMessage message) : base(id, message)
        {
            _objectName = objectName;
            UpdateJointHandles();
        }

        private AdditionalMotionParameters _additionalMotionParameters
        {
            set
            {
                if (value != null)
                {
                    CoordinateType = value.CoordinateType;
                }
            }
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public void Absolute(double[] position, AdditionalMotionParameters addPara = null)
        {
            _additionalMotionParameters = addPara;
            switch (CoordinateType)
            {
                case CoordinateType.Joint:
                    CoppeliaSimRemoteApi.MoveJoint(_id, ConvertToFloatArray(position), _jointHandles, true);
                    break;

                case CoordinateType.Descartes:
                    throw new NotImplementedException();
            }
        }

        public void Absolute(double xJ1, double yJ2, double zJ3, double aJ4, double bJ5, double cJ6, AdditionalMotionParameters addPara = null)
        {
            Absolute(new[] { xJ1, yJ2, zJ3, aJ4, bJ5, cJ6 }, addPara);
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

        private float[] ConvertToFloatArray(double[] value)
        {
            float[] fArray = new float[6];
            for (var i = 0; i < 6; i++)
            {
                fArray[i] = (float)value[i];
            }
            return fArray;
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