using System;
using AELTA_test;
using RASDK.Arm.Type;

namespace RASDK.Arm.TMRobot
{
    public class Motion : IMotion
    {
        private CommandSender _commandSender;
        private CoordinateType _coordinateType = CoordinateType.Descartes;
        private MotionType _motionType = MotionType.PointToPoint;

        private AdditionalMotionParameters _additionalMotionParameters
        {
            set
            {
                if (value != null)
                {
                    _motionType = value.MotionType;
                    _coordinateType = value.CoordinateType;
                }
            }
        }

        public Motion(SocketClientObject socketClientObject)
        {
            _commandSender = new CommandSender(socketClientObject);
        }

        public void Abort()
        {
            _commandSender.Send(@"1,StopAndClearBuffer()");
        }

        public void Absolute(double[] position, AdditionalMotionParameters addPara = null)
        {
            if (position.Length != 6)
            {
                throw new ArgumentException("Length of position must be 6");
            }

            _additionalMotionParameters = addPara;

            string positionString = "";
            foreach (var p in position)
            {
                positionString += p.ToString();
                positionString += ',';
            }

            var coordianteTypeChar = _coordinateType == CoordinateType.Descartes ? 'C' : 'J';
            string motionTypeString;
            switch (_motionType)
            {
                case MotionType.PointToPoint:
                    motionTypeString = "PTP";
                    break;

                case MotionType.Linear:
                    motionTypeString = "Line";
                    break;

                case MotionType.Circle:
                    motionTypeString = "Circle";
                    break;

                default:
                    throw new Exception();
            }

            string command = $"1,{motionTypeString}(\"{coordianteTypeChar}PP\",{positionString}100,200,0,false)";
            _commandSender.Send(command);
        }

        public void Absolute(double xJ1,
                             double yJ2,
                             double zJ3,
                             double aJ4,
                             double bJ5,
                             double cJ6,
                             AdditionalMotionParameters addPara = null)
        {
            Absolute(new[] { xJ1, yJ2, zJ3, aJ4, bJ5, cJ6 }, addPara);
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
            int speed = 100;
            double sp_pc = 1.0;
            string command = @"1,PTP(""CPP"",519,-122,458,185,0,90," + string.Format("{0:000}", speed * sp_pc) + ",200,0,false)";
            _commandSender.Send(command);
        }

        public void Jog(string axis)
        {
            throw new System.NotImplementedException();
        }
    }
}