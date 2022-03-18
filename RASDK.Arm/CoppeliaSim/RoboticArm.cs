using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RASDK.Arm.Type;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.CoppeliaSim
{
    public class RoboticArm : RASDK.Arm.RoboticArm
    {
        private readonly string _ip;
        private readonly string _objectName;
        private readonly int _port;
        private int _id;
        private int[] _jointHandles;

        public RoboticArm(MessageHandler message,
                          string ip = Default.Ip,
                          int port = Default.Port,
                          string objectName = Default.ObjectName)
            : base(message)
        {
            _ip = ip;
            _port = port;
            _objectName = objectName;
        }

        public int Id => _id;

        public override double[] GetNowPosition(CoordinateType coordinate = CoordinateType.Descartes)
        {
            var position = new float[6];

            switch (coordinate)
            {
                case CoordinateType.Descartes:
                    throw new NotImplementedException();
                    break;

                case CoordinateType.Joint:
                    CoppeliaSimRemoteApi.GetJointPosition(_id, _jointHandles, position);
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

        #region Motion

        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override void Homing(bool slowly = true, CoordinateType coordinate = CoordinateType.Descartes, bool needWait = true)
        {
            throw new NotImplementedException();
        }

        public override void Jog(string axis)
        {
            throw new NotImplementedException();
        }

        public override void MoveAbsolute(double j1X,
                                          double j2Y,
                                          double j3Z,
                                          double j4A,
                                          double j5B,
                                          double j6C,
                                          AdditionalMotionParameters addParams = null)
        {
            addParams = addParams ?? new AdditionalMotionParameters();
            var position = new double[] { j1X, j2Y, j3Z, j4A, j5B, j6C };

            switch (addParams.CoordinateType)
            {
                case CoordinateType.Joint:
                    CoppeliaSimRemoteApi.MoveJoint(_id, ConvertToFloatArray(position), _jointHandles, true);
                    break;

                case CoordinateType.Descartes:
                    throw new NotImplementedException();
            }
        }

        public override void MoveRelative(double j1X,
                                          double j2Y,
                                          double j3Z,
                                          double j4A = 0,
                                          double j5B = 0,
                                          double j6C = 0,
                                          AdditionalMotionParameters addParams = null)
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

        #endregion Motion

        #region Connect/Disconnect

        public override bool Connected
        {
            get
            {
                bool connected;
                try
                {
                    connected = CoppeliaSimRemoteApi.IsConnected(_id);
                }
                catch (Exception)
                {
                    connected = false;
                }
                return connected;
            }
        }

        public override bool Connect()
        {
            _id = CoppeliaSimRemoteApi.Connect(_ip, _port);

            // Check connection.
            if (_id != -1)
            {
                UpdateJointHandles();
                ShowSuccessfulConnectMessage();
            }
            else
            {
                ShowUnsuccessfulConnectMessage();
            }

            return Connected;
        }

        public override bool Disconnect()
        {
            var returnCode = CoppeliaSimRemoteApi.Disconnect(_id);
            _message.Show($"斷線.\r\n回傳：{returnCode}");
            return !Connected;
        }

        private void ShowSuccessfulConnectMessage()
        {
            var text = "CoppeliaSim 連線成功!\r\n" +
                       $"ID: {_id}\r\n" +
                       $"IP: {_ip}\r\n" +
                       $"Port: {_port}";

            _message.Show(text,
                          "連線",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.None);
        }

        private void ShowUnsuccessfulConnectMessage()
        {
            var text = "CoppeliaSim 連線失敗!\r\n" +
                       $"ID: {_id}\r\n" +
                       $"IP: {_ip}\r\n" +
                       $"Port: {_port}";

            _message.Show(text,
                          "連線",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error,
                          LoggingLevel.Error);
        }

        #endregion Connect/Disconnect

        #region Speed/Acceleration

        public override double Acceleration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override double Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion Speed/Acceleration

        private void UpdateJointHandles()
        {
            _jointHandles = new int[6];
            for (var i = 0; i < _jointHandles.Length; i++)
            {
                var jointName = $"{_objectName}_joint{i + 1}";
                _jointHandles[i] = CoppeliaSimRemoteApi.GetObjectHandle(_id, jointName);
            }
        }
    }
}