using RASDK.Arm.Type;
using RASDK.Basic.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.CoppeliaSim
{
    internal class GetNowPosition : BasicAction
    {
        private readonly string _objectName;

        public GetNowPosition(string objectName, int id, IMessage message) : base(id, message)
        {
            _objectName = objectName;
        }

        public double[] Value(CoordinateType coordinateType = CoordinateType.Descartes)
        {
            var position = new float[6];
            var jh = JointHandle.Get(_id, _objectName, 6);

            switch (coordinateType)
            {
                case CoordinateType.Descartes:
                    throw new NotImplementedException();
                    break;

                case CoordinateType.Joint:
                    CoppeliaSimRemoteApi.GetJointPosition(_id, jh, position);
                    break;

                default:
                    throw new ArgumentException("Unknown coordinator type.");
            }

            var dPos = new double[6];
            for (var i = 0; i < 6; i++)
            {
                dPos[i] = (double)position[i];
            }
            return dPos;
        }
    }
}