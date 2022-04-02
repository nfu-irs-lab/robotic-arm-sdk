using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RASDK.Arm.Type;

namespace RASDK.Arm
{
    /// <summary>
    /// 機械手臂動作。
    /// </summary>
    public class RoboticArmMotion
    {
        #region Default Value

        /// <summary>
        /// 預設的座標類型。
        /// </summary>
        protected const CoordinateType _defaultCoordinateType = CoordinateType.Descartes;

        /// <summary>
        /// 預設的運動類型。
        /// </summary>
        protected const MotionType _defaultMotionType = MotionType.PointToPoint;

        /// <summary>
        /// 預設的位置類型。
        /// </summary>
        protected const PositionType _defaultPositionType = PositionType.Absolute;

        /// <summary>
        /// 預設的手臂平滑模式。
        /// </summary>
        protected const SmoothType _defaultSmoothType = SmoothType.TwoLinesSpeedSmooth;

        #endregion Default Value

        #region Position Value

        /// <summary>
        /// 座標 J1/X。
        /// </summary>
        public double J1X = 0;

        /// <summary>
        /// 座標 J2/Y。
        /// </summary>
        public double J2Y = 0;

        /// <summary>
        /// 座標 J3/Z。
        /// </summary>
        public double J3Z = 0;

        /// <summary>
        /// 座標 J4/A。
        /// </summary>
        public double J4A = 0;

        /// <summary>
        /// 座標 J5/B。
        /// </summary>
        public double J5B = 0;

        /// <summary>
        /// 座標 J6/C。
        /// </summary>
        public double J6C = 0;

        #endregion Position Value

        #region Type

        /// <summary>
        /// 座標類型。
        /// </summary>
        public CoordinateType CoordinateType;

        /// <summary>
        /// 運動類型。
        /// </summary>
        public MotionType MotionType;

        /// <summary>
        /// 位置類型。
        /// </summary>
        public PositionType PositionType;

        /// <summary>
        /// 手臂平滑模式。
        /// </summary>
        public SmoothType SmoothType = _defaultSmoothType;

        /// <summary>
        /// 手臂平滑模式數值。
        /// </summary>
        public int SmoothValue = 50;

        #endregion Type

        /// <summary>
        /// 是否需要等待動作完成（阻塞）。
        /// </summary>
        public bool NeedWait = true;

        #region Constructor

        /// <summary>
        /// 機械手臂動作。
        /// </summary>
        /// <param name="j1X">座標 J1/X。</param>
        /// <param name="j2Y">座標 J2/Y。</param>
        /// <param name="j3Z">座標 J3/Z。</param>
        /// <param name="j4A">座標 J4/A。</param>
        /// <param name="j5B">座標 J5/B。</param>
        /// <param name="j6C">座標 J6/C。</param>
        /// <param name="positionType">位置類型。</param>
        /// <param name="coordinateType">座標類型。</param>
        /// <param name="motionType">運動類型。</param>
        /// <param name="needWait">是否需要等待動作完成（阻塞）。</param>
        public RoboticArmMotion(double j1X, double j2Y, double j3Z, double j4A, double j5B, double j6C,
                                PositionType positionType = _defaultPositionType,
                                CoordinateType coordinateType = _defaultCoordinateType,
                                MotionType motionType = _defaultMotionType,
                                bool needWait = true)
        {
            J1X = j1X;
            J2Y = j2Y;
            J3Z = j3Z;
            J4A = j4A;
            J5B = j5B;
            J6C = j6C;
            PositionType = positionType;
            CoordinateType = coordinateType;
            MotionType = motionType;
            NeedWait = needWait;
        }

        /// <summary>
        /// 機械手臂動作。
        /// </summary>
        /// <param name="position">座標位置。</param>
        /// <param name="positionType">位置類型。</param>
        /// <param name="coordinateType">座標類型。</param>
        /// <param name="motionType">運動類型。</param>
        /// <param name="needWait">是否需要等待動作完成（阻塞）。</param>
        /// <exception cref="ArgumentException"></exception>
        public RoboticArmMotion(double[] position,
                                PositionType positionType = _defaultPositionType,
                                CoordinateType coordinateType = _defaultCoordinateType,
                                MotionType motionType = _defaultMotionType,
                                bool needWait = true)
        {
            if (position.Length != 6)
            {
                throw new ArgumentException("The length of 'position' must be 6.");
            }
            J1X = position[0];
            J2Y = position[1];
            J3Z = position[2];
            J4A = position[3];
            J5B = position[4];
            J6C = position[5];
            PositionType = positionType;
            CoordinateType = coordinateType;
            MotionType = motionType;
            NeedWait = needWait;
        }

        /// <summary>
        /// 機械手臂動作。
        /// </summary>
        /// <param name="roboticArmMotion">機械手臂動作。</param>
        public RoboticArmMotion(RoboticArmMotion roboticArmMotion)
        {
            if (roboticArmMotion == null)
            {
                throw new ArgumentNullException();
            }

            // Clone.
            J1X = roboticArmMotion.J1X;
            J2Y = roboticArmMotion.J2Y;
            J3Z = roboticArmMotion.J3Z;
            J4A = roboticArmMotion.J4A;
            J5B = roboticArmMotion.J5B;
            J6C = roboticArmMotion.J6C;
            PositionType = roboticArmMotion.PositionType;
            CoordinateType = roboticArmMotion.CoordinateType;
            MotionType = roboticArmMotion.MotionType;
            SmoothType = roboticArmMotion.SmoothType;
            SmoothValue = roboticArmMotion.SmoothValue;
            NeedWait = roboticArmMotion.NeedWait;
        }

        #endregion Constructor
    }
}
