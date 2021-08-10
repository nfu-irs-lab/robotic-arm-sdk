using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using RASDK.Basic;
using RASDK.Basic.Message;
using System.Windows.Forms;

namespace RASDK.Arm.TMRobot
{
    public class TMRobotArm : ArmActionFactory, IDevice
    {
        private readonly string _ip;
        private readonly int _port;

        private static IPAddress _ipAddress = IPAddress.Parse("127.0.0.1");
        private static int _portNumber = 10001;

        string[] data = new string[10];
        double[] dataint = new double[] { 0, 0, 0, 0, 0, 0 };

        private TcpListener ServerListener = new TcpListener(_ipAddress, _portNumber);
        private TcpClient ClientSockt = default;

        public TMRobotArm(string ip, int port, IMessage message) : base(message)
        {
            _ip = ip;
            _port = port;

            Thread threadingServer = new Thread(StartServer);
            threadingServer.Start();
        }

        public override IConnection Connection()
        {
            return new Connection(_ip, _port, _message);
        }

        public override IMotion Motion()
        {
            throw new System.NotImplementedException();
        }

        #region IDevice

        // IDevice 在這層實作是爲了遵守介面隔離原則。

        public bool Connected => Connection().IsOpen;

        public bool Connect()
        {
            Connection().Open();
            return Connected;
        }

        public bool Disconnect()
        {
            Connection().Close();
            return !Connected;
        }

        #endregion IDevice

        #region 底層

        #region --通訊--

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

        private void StartServer()
        {
            // System.Action<int> DelegateTeste_ModifyText = THREAD_MOD;
            // System.Action<string> DelegateTeste_ModifyText2 = THREAD_MOD2;

            System.Action<double, double, double, double, double, double> Tolist = CoordinateConversion2;

            System.Action<string> tp1 = Thread_Point1;

            ServerListener.Start();
            // Invoke(DelegateTeste_ModifyText2, "Server waiting connections!");
            ClientSockt = ServerListener.AcceptTcpClient();
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
                    ServerListener.Stop();
                    ServerListener.Start();
                    ClientSockt = ServerListener.AcceptTcpClient();
                }

            }
        }

        #endregion

        int CoordinateConversionCount = 0;

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

        #endregion 底層
    }
}