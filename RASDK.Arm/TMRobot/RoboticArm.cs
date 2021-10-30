using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using RASDK.Basic;
using RASDK.Basic.Message;
using System.Windows.Forms;
using RASDK.Arm.Type;
using System.Text.RegularExpressions;

namespace RASDK.Arm.TMRobot
{
    public class RoboticArm : RASDK.Arm.RoboticArm
    {
        private static IPAddress _pcIp = IPAddress.Parse("127.0.0.1");
        private static int _pcPort = 10001;
        private readonly string _armIp;
        private readonly int _armPort;
        private double[] _lastPosition;
        private TcpListener _serverListener;
        private SocketClientObject _socketClientObject;
        private TcpClient ClientSockt = default;
        private string[] data = new string[10];
        private double[] dataint = new double[] { 0, 0, 0, 0, 0, 0 };

        public RoboticArm(IMessage message,
                          string ip = Default.IpOfArmConnection,
                          int port = Default.PortOfArmConnection,
                          TcpListener pcTcpListener = null)
            : base(message)
        {
            _armIp = ip;
            _armPort = port;

            _speed = 50;
            _acceleration = 200;

            _serverListener = pcTcpListener ?? new TcpListener(_pcIp, _pcPort);

            Thread threadingServer = new Thread(StartServer);
            threadingServer.Start();
        }

        public override double[] GetNowPosition(CoordinateType coordinateType = CoordinateType.Descartes)
        {
            var times = 25;
            var delay = 50;

            _lastPosition = null;
            _socketClientObject.ReceiveData += ReceiveDataHandler;
            SendCommand("1,ListenSend(90,GetString(Robot[0].CoordRobot))");

            for (int i = 0; i < times; i++)
            {
                Thread.Sleep(delay);
                if (_lastPosition != null)
                {
                    _socketClientObject.ReceiveData -= ReceiveDataHandler;
                    break;
                }
            }
            return _lastPosition;
        }

        #region Motion

        public override void Abort()
        {
            SendCommand(@"1,StopAndClearBuffer()");
        }

        public override void Homing(bool slowly = true, CoordinateType coordinate = CoordinateType.Descartes, bool needWait = true)
        {
            var homePos = coordinate == CoordinateType.Descartes ? Default.DescartesHomePosition : Default.JointHomePosition;
            var homePosString = "";
            foreach (var p in homePos)
            {
                homePosString += p.ToString();
                homePosString += ",";
            }

            var speed = slowly ? Default.SpeedOfHomingSlowly : _speed;
            var command = $"1,PTP(\"CPP\",{homePosString}{speed},{_acceleration},0,false)";
            SendCommand(command);
        }

        public override void Jog(string axis)
        {
            // Remove all whitespace char.
            axis = Regex.Replace(axis, @"\s", "");

            if (CheckJogArg(axis))
            {
                var pos = new double[] { 0, 0, 0, 0, 0, 0 };
                pos[PatseAxis(axis)] = ParseDirection(axis) * 10;
                MoveRelative(pos);
            }
            else
            {
                throw new ArgumentException($"Input regex: {_inputRegexPattern}");
            }
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
            var positionString = "";
            var coordianteTypeChar = addParams.CoordinateType == CoordinateType.Descartes ? 'C' : 'J';
            string motionTypeString;

            foreach (var p in position)
            {
                positionString += p.ToString();
                positionString += ',';
            }

            switch (addParams.MotionType)
            {
                case MotionType.PointToPoint:
                    motionTypeString = "PTP";
                    break;

                case MotionType.Linear:
                    motionTypeString = "Line";
                    break;

                case MotionType.Circle:
                    throw new NotImplementedException("還未實作 Circle 的控制方法。");

                default:
                    throw new ArgumentException("未知的絕對運動方式。");
            }

            var command = $"1,{motionTypeString}(\"{coordianteTypeChar}PP\",{positionString}{_speed},{_acceleration},0,false)";
            SendCommand(command);
        }

        public override void MoveRelative(double j1X, double j2Y, double j3Z, double j4A = 0, double j5B = 0, double j6C = 0, AdditionalMotionParameters addParams = null)
        {
            addParams = addParams ?? new AdditionalMotionParameters();
            var position = new double[] { j1X, j2Y, j3Z, j4A, j5B, j6C };
            var positionString = "";
            var coordianteTypeChar = addParams.CoordinateType == CoordinateType.Descartes ? 'C' : 'J';
            string motionTypeString;

            foreach (var p in position)
            {
                positionString += p.ToString();
                positionString += ',';
            }

            switch (addParams.MotionType)
            {
                case MotionType.PointToPoint:
                    motionTypeString = "Move_PTP";
                    break;

                case MotionType.Linear:
                    motionTypeString = "Move_Line";
                    break;

                case MotionType.Circle:
                    throw new ArgumentException("沒有 Circle 的相對運動方式。");

                default:
                    throw new ArgumentException("未知的相對運動方式。");
            }

            string command = $"1,{motionTypeString}(\"{coordianteTypeChar}PP\",{positionString}{_speed},{_acceleration},0,false)";
            SendCommand(command);
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
                    connected = _socketClientObject.IsConnected;
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
            _socketClientObject = _socketClientObject ?? new SocketClientObject(_armIp, _armPort);

            ThreadStart threadStart = null;
            try
            {
                int result = 0;
                if (CheckIpAddressValid(_armIp))
                {
                    if (_socketClientObject != null)
                    {
                        _socketClientObject.ConnectStatusResponse +=
                            new SocketClientObject.TCPConnectStatusResponse(this.ShowConnectStatus);
                        if (threadStart == null)
                        {
                            threadStart = delegate
                            {
                                if (_socketClientObject.Connect(0))
                                {
                                    _socketClientObject.ReceiveData += new SocketClientObject.TCPReceiveData(this.ShowReceiveData);
                                }
                            };
                        }
                        new Thread(threadStart) { IsBackground = true }.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                _message.Show(ex, LoggingLevel.Error);
            }

            return Connected;
        }

        public override bool Disconnect()
        {
            if ((_socketClientObject != null) && _socketClientObject.Disconnect())
            {
                _socketClientObject.ReceiveData -= new SocketClientObject.TCPReceiveData(this.ShowReceiveData);
                _socketClientObject = null;
            }

            return !Connected;
        }

        private bool CheckIpAddressValid(string ip)
        {
            IPAddress address;

            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            return IPAddress.TryParse(ip, out address);
        }

        private void ShowConnectStatus(object sender, string resp)
        {
            // AddConnectStatus(resp, LB_ConnectionStatus);

            _message.Show(resp);
        }

        private void ShowReceiveData(object sender, string recv_data)
        {
            string str = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss:fff"), recv_data);
            // this.showRecvDataLog.AppendLine(str);
            // AddReceiveData(showRecvDataLog.ToString(), TB_RecvData);
        }

        #endregion Connect/Disconnect

        #region Speed/Acceleration

        private double _acceleration;

        private double _speed;

        public override double Acceleration
        {
            get => _acceleration;

            set { _acceleration = value; }
        }

        public override double Speed
        {
            get => _speed;

            set
            {
                if (value > 100 || value < 1)
                {
                    _message.Show($"手臂速度應爲1% ~ 100%之間。輸入值爲： {value}",
                                  LoggingLevel.Warn);
                }
                else
                {
                    _speed = value;
                }
            }
        }

        #endregion Speed/Acceleration

        #region 底層

        private int CoordinateConversionCount = 0;

        private void CoordinateConversion2(double d1,
                                           double d2,
                                           double d3,
                                           double d4,
                                           double d5,
                                           double d6)
        {
            int xmax = 600, xmin = 70, ymax = 600, ymin = -600, zmax = 400, zmin = -45;

            double RotateBuffer_x = 180, RotateBuffer_y = 0, RotateBuffer_z = 90;

            int x, y, z;

            x = Convert.ToInt32(d1) + 450;
            y = Convert.ToInt32(d3) + (-122);
            z = Convert.ToInt32(d2) + 300;

            d1 = x;
            d2 = y;
            d3 = z;

            RotateBuffer_y = d4 * -1;
            RotateBuffer_z = 90 - d5;

            if (d6 >= 0)
                RotateBuffer_x = -180 + d6;
            else if (d6 < 0)
                RotateBuffer_x = d6 + 180;

            d4 = RotateBuffer_x;
            d5 = RotateBuffer_y;
            d6 = RotateBuffer_z;

            if (d1 > xmax)
                d1 = xmax;
            if (d1 < xmin)
                d1 = xmin;

            if (d2 > ymax)
                d2 = ymax;
            if (d2 < ymin)
                d2 = ymin;

            if (d3 > zmax)
                d3 = zmax;
            if (d3 < zmin)
                d3 = zmin;

            // System.Action<double, double, double, double, double, double> Tolist2 = ToDataPointList;

            // Invoke(Tolist2, d1, d2, d3, d4, d5, d6);

            // System.Action<int, double, double, double, double, double, double> EDG = WriteDataGrid;

            // Invoke(EDG, CoordinateConversionCount, d1, d2, d3, d4, d5, d6);
            CoordinateConversionCount++;
        }

        private void ReceiveDataHandler(object sender, string recvData)
        {
            recvData = recvData.Trim();
            if (recvData.IndexOf("$TMSTA") == 0 && recvData.Split(',')[2] == "90")
            {
                var head = recvData.IndexOf('{');
                var end = recvData.IndexOf('}');
                var pos = recvData.Substring(head + 1, (end - head) - 1).Split(',');

                if (pos.Length == 6)
                {
                    _lastPosition = new[] { 0.0, 0, 0, 0, 0, 0 };
                    for (int i = 0; i < 6; i++)
                    {
                        try
                        {
                            _lastPosition[i] = Convert.ToDouble(pos[i]);
                        }
                        catch (Exception)
                        {
                            _lastPosition = null;
                            return;
                        }
                    }
                }
                else
                {
                    _lastPosition = null;
                }
            }
        }

        private void SendCommand(string command, string type = "$TMSCT")
        {
            var data = SocketClientObject.DataToPacket(type, command);
            var bytes = Encoding.UTF8.GetBytes(data);

            // Send.
            _socketClientObject?.WriteSyncData(bytes);
        }

        #region 底層.通訊

        private void StartServer()
        {
            // System.Action<int> DelegateTeste_ModifyText = THREAD_MOD;
            // System.Action<string> DelegateTeste_ModifyText2 = THREAD_MOD2;

            System.Action<double, double, double, double, double, double> Tolist = CoordinateConversion2;

            System.Action<string> tp1 = Thread_Point1;

            _serverListener.Start();
            // Invoke(DelegateTeste_ModifyText2, "Server waiting connections!");
            ClientSockt = _serverListener.AcceptTcpClient();
            // Invoke(DelegateTeste_ModifyText2, "Server ready!");

            while (true)
            {
                try
                {
                    NetworkStream networkStream = ClientSockt.GetStream();
                    byte[] bytesFrom = new byte[128];
                    int byteRead = networkStream.Read(bytesFrom, 0, 128);

                    string msg = Encoding.Unicode.GetString(bytesFrom, 0, byteRead);
                    //Invoke(DelegateTeste_ModifyText2, msg);

                    string[] cutmasg = msg.Split('A');

                    int i = 0;
                    foreach (var item in cutmasg)
                    {
                        i++;
                        data[i] = item;
                        //Invoke(DelegateTeste_ModifyText2, i.ToString() + ":" + item);
                    }

                    // Invoke(DelegateTeste_ModifyText2, "cut " + data[1]);
                    // Invoke(DelegateTeste_ModifyText2, "cut " + data[2]);
                    // Invoke(DelegateTeste_ModifyText2, "cut " + data[3]);
                    // Invoke(DelegateTeste_ModifyText2, "cut " + data[4]);
                    // Invoke(DelegateTeste_ModifyText2, "cut " + data[5]);
                    // Invoke(DelegateTeste_ModifyText2, "cut " + data[6]);

                    try
                    {
                        dataint[0] = Convert.ToDouble(data[1]);
                        dataint[1] = Convert.ToDouble(data[2]);
                        dataint[2] = Convert.ToDouble(data[3]);
                        dataint[3] = Convert.ToDouble(data[4]);
                        dataint[4] = Convert.ToDouble(data[5]);
                        dataint[5] = Convert.ToDouble(data[6]);

                        // Invoke(DelegateTeste_ModifyText2, "dataint " + dataint[0]);
                        // Invoke(DelegateTeste_ModifyText2, "dataint " + dataint[1]);
                        // Invoke(DelegateTeste_ModifyText2, "dataint " + dataint[2]);
                        // Invoke(DelegateTeste_ModifyText2, "dataint " + dataint[3]);
                        // Invoke(DelegateTeste_ModifyText2, "dataint " + dataint[4]);
                        // Invoke(DelegateTeste_ModifyText2, "dataint " + dataint[5]);

                        // Invoke(Tolist, dataint[0], dataint[1], dataint[2], dataint[3], dataint[4], dataint[5]);
                    }
                    catch
                    {
                        // Invoke(DelegateTeste_ModifyText2, "error 1 ");
                    }
                }
                catch
                {
                    // Invoke(DelegateTeste_ModifyText2, "error 2");
                    _serverListener.Stop();
                    _serverListener.Start();
                    ClientSockt = _serverListener.AcceptTcpClient();
                }
            }
        }

        private void THREAD_MOD(int teste)
        {
            // socketmsg.Text += Environment.NewLine + teste;
            //
            // socketmsg.SelectionStart = socketmsg.TextLength;
            // socketmsg.ScrollToCaret();
        }

        private void THREAD_MOD2(string teste)
        {
            // socketmsg.Text += Environment.NewLine + teste;
            //
            // socketmsg.SelectionStart = socketmsg.TextLength;
            // socketmsg.ScrollToCaret();
        }

        private void Thread_Point1(string teste)
        {
            // socketmsg.Text += Environment.NewLine + teste;
            //
            // socketmsg.SelectionStart = socketmsg.TextLength;
            // socketmsg.ScrollToCaret();
        }

        #endregion 底層.通訊

        private void ToDataPointList(double d1,
                                     double d2,
                                     double d3,
                                     double d4,
                                     double d5,
                                     double d6)
        {
            // this.PointDataList.BeginUpdate();
            //
            // ListViewItem pdl = new ListViewItem();
            //
            // pdl.SubItems.Add("" + d1);
            // pdl.SubItems.Add("" + d2);
            // pdl.SubItems.Add("" + d3);
            // pdl.SubItems.Add("" + d4);
            // pdl.SubItems.Add("" + d5);
            // pdl.SubItems.Add("" + d6);
            //
            // this.PointDataList.Items.Add(pdl);
            //
            // this.PointDataList.EndUpdate();
            //
            // Action<double, double, double, double, double, double> Toarm = armMove2;
            //
            // Invoke(Toarm, d1, d2, d3, d4, d5, d6);
        }

        private void WriteDataGrid(int count,
                                   double d1,
                                   double d2,
                                   double d3,
                                   double d4,
                                   double d5,
                                   double d6)
        {
            // this.PointDataGrid.Rows.Add(count, d1, d2, d3, d4, d5, d6);
        }

        #endregion 底層
    }
}