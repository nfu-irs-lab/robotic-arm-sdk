using System;
using System.Net;
using System.Text;
using System.Threading;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.TMRobot
{
    public class Connection : IConnection
    {
        private readonly IMessage _message;
        private string _ip;
        private int _port;

        private SocketClientObject _tcpClientObject;
        // private StringBuilder showRecvDataLog = new StringBuilder();

        public Connection(string ip, int port, IMessage message, SocketClientObject socketClientObject)
        {
            _ip = ip;
            _port = port;
            _message = message;

            _tcpClientObject = socketClientObject;
            //socketClientObject = _tcpClientObject;
        }

        public bool IsOpen { get; }

        public void Close()
        {
            if ((this._tcpClientObject != null) && this._tcpClientObject.Disconnect())
            {
                this._tcpClientObject.ReceiveData -= new SocketClientObject.TCPReceiveData(this.ShowReceiveData);
                this._tcpClientObject = null;
            }
        }

        public void Open()
        {
            ThreadStart threadStart = null;
            try
            {
                int result = 0;
                if (CheckIpAddressValid(_ip))
                {
                    // this._tcpClientObject = new SocketClientObject(_ip, _port);
                    if (this._tcpClientObject != null)
                    {
                        this._tcpClientObject.ConnectStatusResponse +=
                            new SocketClientObject.TCPConnectStatusResponse(this.showConnectStatus);
                        if (threadStart == null)
                        {
                            threadStart = delegate
                            {
                                if (this._tcpClientObject.Connect(0))
                                {
                                    _tcpClientObject.ReceiveData += new SocketClientObject.TCPReceiveData(this.ShowReceiveData);
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
        }

        public void ShowReceiveData(object sender, string recv_data)
        {
            string str = string.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss:fff"), recv_data);
            // this.showRecvDataLog.AppendLine(str);
            // AddReceiveData(showRecvDataLog.ToString(), TB_RecvData);
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

        private void showConnectStatus(object sender, string resp)
        {
            // AddConnectStatus(resp, LB_ConnectionStatus);

            _message.Show(resp);
        }

        // private delegate void AddConnectDataDelegate(string _connectstatus, Label _label);
        // private void AddConnectStatus(string _connectstatus, Label _label)
        // {
        //     if (this.InvokeRequired)
        //     {
        //         AddConnectDataDelegate ConnectStatus = new AddConnectDataDelegate(AddConnectStatus);
        //         this.Invoke(ConnectStatus, _connectstatus, _label);
        //     }
        //     else
        //     {
        //         _label.Text = _connectstatus;
        //     }
        // }
        // private void AddReceiveData(string _receivedata, TextBox _textbox)
        // {
        //     if (this.InvokeRequired)
        //     {
        //         AddReceiveDataDelegate ReceiveData = new AddReceiveDataDelegate(AddReceiveData);
        //         this.Invoke(ReceiveData, _receivedata, _textbox);
        //     }
        //     else
        //     {
        //         _textbox.AppendText(_receivedata);
        //     }
        // }
    }
}