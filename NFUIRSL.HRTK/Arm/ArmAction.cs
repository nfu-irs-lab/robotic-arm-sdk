using System;
using System.Text.RegularExpressions;
using SDKHrobot;

namespace NFUIRSL.HRTK
{
    public interface IArmAction
    {
        string Message { get; }
        bool NeedWait { get; set; }
        int ArmId { get; set; }

        bool Do();
    }

    public abstract class ArmMotion : IArmAction
    {
        public ArmMotion(double xj1,
                         double yj2,
                         double zj3,
                         double aj4,
                         double bj5,
                         double cj6)
        {
            Position[0] = xj1;
            Position[1] = yj2;
            Position[2] = zj3;
            Position[3] = aj4;
            Position[4] = bj5;
            Position[5] = cj6;
        }

        public virtual string Message
        {
            get => "Arm Motion";
        }

        public int ArmId { get; set; }
        protected int SmoothTypeCode;
        public bool NeedWait { get; set; } = true;
        public CoordinateType CoordinateType { get; set; } = CoordinateType.Descartes;
        public PositionType PositionType { get; set; } = PositionType.Absolute;
        public SmoothType SmoothType { get; set; } = SmoothType.TwoLinesSpeedSmooth;
        public double[] Position { get; } = new double[6];

        public abstract bool Do();

        protected virtual string GetTextPosition(double[] position)
        {
            return $"{position[0]}," +
                   $"{position[1]}," +
                   $"{position[2]}," +
                   $"{position[3]}," +
                   $"{position[4]}," +
                   $"{position[5]}";
        }

        public virtual bool IsSuccess(int code, int ignoreCode = 0, int successCode = 0)
            => (code == ignoreCode) || (code == successCode);
    }

    public class PointToPointMotion : ArmMotion
    {
        public string Message
        {
            get => $"PointToPoint:" +
                   $"\"{GetTextPosition(Position)}\";" +
                   $"CT:{CoordinateType.ToString()};" +
                   $"PT:{PositionType.ToString()};" +
                   $"ST:{SmoothTypeCode};" +
                   $"Wait:{NeedWait}";
        }

        public SmoothType SmoothType
        {
            get => SmoothTypeCode == 1 ? SmoothType.TwoLinesSpeedSmooth : SmoothType.Disable;
            set => SmoothTypeCode = value == SmoothType.TwoLinesSpeedSmooth ? 1 : 0;
        }

        public override bool Do()
        {
            Func<int, int, double[], int> action;
            switch (CoordinateType)
            {
                case CoordinateType.Descartes when PositionType == PositionType.Absolute:
                    action = HRobot.ptp_pos;
                    break;

                case CoordinateType.Descartes when PositionType == PositionType.Relative:
                    action = HRobot.ptp_rel_pos;
                    break;

                case CoordinateType.Joint when PositionType == PositionType.Absolute:
                    action = HRobot.ptp_axis;
                    break;

                case CoordinateType.Joint when PositionType == PositionType.Relative:
                    action = HRobot.ptp_rel_axis;
                    break;

                default:
                    return false;
            }
            var returnCode = action(ArmId, SmoothTypeCode, Position);
            return IsSuccess(returnCode);
        }

        public PointToPointMotion(double x,
                                  double y,
                                  double z,
                                  double a,
                                  double b,
                                  double c)
            : base(x, y, z, a, b, c)
        {
            SmoothType = SmoothType.TwoLinesSpeedSmooth;
        }
    }
}