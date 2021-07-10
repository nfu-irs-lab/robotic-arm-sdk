using System;
using Basic;
using Arm.Type;

namespace Arm
{
    public interface IArmAdapter
    {
        bool Connect();

        bool Disconnect();

        double GetNowPosition(CoordinateType coordinateType);

        double Speed { get; set; }
        double Acceleration { get; set; }

        double[] DescartesHomePosition { get; }
        double[] JointHomePosition { get; }

        bool Jog(Sign direction, Axis axis);

        bool AbsoluteMotion(double[] targetPosition, CoordinateType coordinateType, MotionType motionType, SmoothType smoothType);

        bool RelativeMotion(double[] position, CoordinateType coordinateType, MotionType motionType, SmoothType smoothType);

        bool Homing(CoordinateType coordinateType);

        bool AbortMotion();
    }
}