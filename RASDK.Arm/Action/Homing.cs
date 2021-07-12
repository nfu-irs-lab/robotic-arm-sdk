using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RASDK.Arm.Type;

namespace RASDK.Arm.Action
{
    /// <summary>
    /// RASDK.Arm Homing.
    /// </summary>
    public class Homing : AbsoluteMotion
    {
        /// <summary>
        /// RASDK.Arm Homing.
        /// </summary>
        /// <param name="coordinateType"></param>
        public Homing(CoordinateType coordinateType = CoordinateType.Descartes)
            : base(coordinateType == CoordinateType.Descartes ?
                       HiwinArm.DescartesHomePosition :
                       coordinateType == CoordinateType.Joint ?
                           HiwinArm.JointHomePosition :
                           throw new AggregateException())
        {
            CoordinateType = coordinateType;
            MotionType = MotionType.PointToPoint;
            NeedWait = true;
        }

        public override string Message => "RASDK.Arm Homing. " +
                                          $"CT{CoordinateType}," +
                                          $"MT{MotionType}," +
                                          $"Wait{NeedWait}.";
    }
}