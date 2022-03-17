using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RASDK.Arm.TMRobot
{
    public class SocketClientObject : IDisposable
    {
        private bool _connected = false;
        private bool _IsConnected = false;
        private object _IsConnected_Lock = new object();
        private bool _isDefineTimeout = false;
        private object _TcpSock_Lock = new object();
        private int _timeout = 0x1770;
        private bool disposed = false;
        private object lock_RecvSyncData = new object();
        private object lock_WriteSyncData = new object();
        private Socket tcpSynCl = null;

        public SocketClientObject(string ip, int port)
        {
            this.IP = ip;
            this.PORT = port;
            this.SocketClientInit();
        }

        ~SocketClientObject()
        {
            this.Dispose(false);
        }

        public delegate void TCPConnectStatusResponse(SocketClientObject sender, string resp);

        public delegate void TCPReceiveData(SocketClientObject sender, string data);

        public event TCPConnectStatusResponse ConnectStatusResponse;

        public event TCPReceiveData ReceiveData;

        public enum ConnectionStatusType
        {
            Connect,
            Idle,
            FailToConnect,
            ConnectLost,
            NoTCPClientSocket,
            FailToSendData,
            FailToRecvData
        }

        public string IP { get; set; }

        public bool IsConnected
        {
            get
            {
                lock (this._IsConnected_Lock)
                {
                    if (!this._connected)
                    {
                        return false;
                    }
                    return this._IsConnected;
                }
            }

            set
            {
                lock (this._IsConnected_Lock)
                {
                    this._IsConnected = value;
                }
            }
        }

        public int PORT { get; set; }

        public static string DataToPacket(string header = null, string data = null)
        {
            if (string.IsNullOrEmpty(header))
            {
                header = "$TMSCT";
            }
            if (string.IsNullOrEmpty(data))
            {
                data = string.Empty;
            }
            List<byte> list = new List<byte>();
            if (((header != null) && (header.Length >= 1)) && (header[0] != '$'))
            {
                list.Add(0x24);
            }
            list.AddRange(PacketGetBytes(header));
            list.Add(0x2c);
            byte[] collection = PacketGetBytes(data);
            int length = collection.Length;
            list.AddRange(PacketGetBytes(length.ToString()));
            list.Add(0x2c);
            list.AddRange(collection);
            list.Add(0x2c);
            string s = PacketChecksumXOR(list.GetRange(1, list.Count - 1).ToArray()).ToString("X2");
            list.Add(0x2a);
            list.AddRange(PacketGetBytes(s));
            list.Add(13);
            list.Add(10);
            return PacketGetString(list, list.Count);
        }

        public void Close()
        {
            lock (this._TcpSock_Lock)
            {
                this.SocketDisconnect(this.tcpSynCl);
                this.tcpSynCl = null;
                this._connected = false;
                this.CheckSocketConnected();
            }
        }

        public bool Connect(int fail_retry = 0)
        {
            ThreadStart start = null;
            ThreadStart start2 = null;
            Func<bool> condition = null;
            lock (this._TcpSock_Lock)
            {
                do
                {
                    if (this.Connect(this.IP, this.PORT))
                    {
                        if (start == null)
                        {
                            start = delegate { this.RecvDataThread(); };
                        }
                        new Thread(start) { IsBackground = true }.Start();
                        if (start2 == null)
                        {
                            start2 = delegate { this.ConnectStatusResponseThread(); };
                        }
                        new Thread(start2) { IsBackground = true }.Start();
                        return true;
                    }
                    if (condition == null)
                    {
                        condition = () => this.disposed || (fail_retry <= 0);
                    }
                    SpinWait.SpinUntil(condition, 500);
                    if (this.disposed)
                    {
                        return false;
                    }
                    fail_retry--;
                }
                while (fail_retry >= 0);
                if (this.ConnectStatusResponse != null)
                {
                    this.ConnectStatusResponse(this, this.showConnectionStatus(2));
                }
                return false;
            }
        }

        public bool Disconnect()
        {
            if (!this.IsConnected)
            {
                return false;
            }
            this.Close();
            return true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool WriteSyncData(byte[] write_data)
        {
            if (this.tcpSynCl != null)
            {
                try
                {
                    if (!this.IsConnected)
                    {
                        return false;
                    }
                    lock (this.lock_WriteSyncData)
                    {
                        this.tcpSynCl.Send(write_data, 0, write_data.Length, SocketFlags.None);
                    }
                    SLogger.log("send_data=" + Encoding.UTF8.GetString(write_data).ToString(), "TCP_Client");
                    return true;
                }
                catch (Exception exception)
                {
                    string str = exception.ToString();
                    string str2 = "connect lost";
                    if (exception is SocketException)
                    {
                        SocketException exception2 = exception as SocketException;
                        switch (exception2.ErrorCode)
                        {
                            case 0x2714:
                            case 0x274c:
                                str2 = "time out";
                                break;
                        }
                        str = string.Format("{0} {1}", exception2.ErrorCode.ToString(), str);
                    }
                    this.CallException(exception.ToString());
                    if (this.ConnectStatusResponse != null)
                    {
                        this.ConnectStatusResponse(this, this.showConnectionStatus(5));
                    }
                    SLogger.log("WriteSyncData(); ConnectionStatusType.FailToSendData", "TCP_Client");
                    SLogger.log("WriteSyncData(); Exception:" + str + "ErrorCode=" + str2, "TCP_Client");
                    return false;
                }
            }
            if (this.ConnectStatusResponse != null)
            {
                this.ConnectStatusResponse(this, this.showConnectionStatus(4));
            }
            SLogger.log("WriteSyncData(); ConnectionStatusType.NoTCPClientSocket", "TCP_Client");
            return false;
        }

        internal void CallException(string ex)
        {
            try
            {
                if (this.tcpSynCl == null)
                {
                    this._connected = false;
                }
                this.Close();
                SLogger.log("Exception:" + ex, "TCP_Client");
            }
            catch
            { }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Close();
                }
                this.disposed = true;
            }
        }

        private static byte PacketChecksumXOR(byte[] data)
        {
            byte num = 0;
            if ((data != null) && (data.Length > 0))
            {
                for (int i = 0; i < data.Length; i++)
                {
                    num = (byte)(num ^ data[i]);
                }
            }
            return num;
        }

        private static byte[] PacketGetBytes(string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s))
                {
                    return new byte[0];
                }
                return Encoding.UTF8.GetBytes(s);
            }
            catch
            { }
            return new byte[0];
        }

        private static string PacketGetString(List<byte> data, int count)
        {
            try
            {
                if (data == null)
                {
                    return string.Empty;
                }
                if (count < 0)
                {
                    return string.Empty;
                }
                if (count > data.Count)
                {
                    count = data.Count;
                }
                return Encoding.UTF8.GetString(data.ToArray(), 0, count);
            }
            catch
            { }
            return string.Empty;
        }

        private bool CheckSocketConnected()
        {
            bool flag = false;
            if (this._connected)
            {
                try
                {
                    if (this.isSocketConnected(this.tcpSynCl))
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                catch
                {
                    flag = false;
                }
            }
            this.IsConnected = flag;
            return flag;
        }

        private void CheckSocketThread()
        {
            Func<bool> condition = null;
            while (!this.disposed)
            {
                try
                {
                    if (condition == null)
                    {
                        condition = () => this.disposed;
                    }
                    SpinWait.SpinUntil(condition, 0x3e8);
                    if (this.disposed)
                    {
                        break;
                    }
                    this.CheckSocketConnected();
                }
                catch
                { }
            }
        }

        private bool Connect(string ip, int port)
        {
            lock (this._TcpSock_Lock)
            {
                try
                {
                    this.Close();
                    if (this.Connect(ref this.tcpSynCl, ip, port))
                    {
                        this._connected = true;
                        this.CheckSocketConnected();
                        return true;
                    }
                }
                catch (Exception exception)
                {
                    this.CallException(exception.ToString());
                }
                this.Close();
                return false;
            }
        }

        private bool Connect(ref Socket sock, string ip, int port)
        {
            lock (this._TcpSock_Lock)
            {
                try
                {
                    IPAddress address;
                    if (!IPAddress.TryParse(ip, out address))
                    {
                        ip = Dns.GetHostEntry(ip).AddressList[0].ToString();
                    }
                    sock = new Socket(IPAddress.Parse(ip).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    if (this._isDefineTimeout)
                    {
                        sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, this._timeout);
                        sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, this._timeout);
                    }
                    sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Debug, 1);
                    IPEndPoint host = new IPEndPoint(IPAddress.Parse(ip), port);
                    if (!this.LoopConnectII(sock, host, 0x1770))
                    {
                        return false;
                    }
                    this.SetKeepAlive(sock, 0x1388L, 0x3e8L);
                    return true;
                }
                catch (Exception exception)
                {
                    this.CallException(exception.ToString());
                }
                return false;
            }
        }

        private void ConnectStatusResponseThread()
        {
            Func<bool> condition = null;
            SLogger.log("in ConnectStatusResponseThread()", "TCP_Client");
            bool _old_connected = false;
            bool _old_IsConnected = false;
            while (true)
            {
                try
                {
                    if (condition == null)
                    {
                        condition = () => (this.IsConnected != _old_IsConnected) || (this._connected != _old_connected);
                    }
                    SpinWait.SpinUntil(condition);
                    if (this._connected && this.IsConnected)
                    {
                        if (this.ConnectStatusResponse != null)
                        {
                            this.ConnectStatusResponse(this, this.showConnectionStatus(0));
                        }
                    }
                    else
                    {
                        if ((this._connected && !this.IsConnected) && _old_IsConnected)
                        {
                            if (this.ConnectStatusResponse != null)
                            {
                                this.ConnectStatusResponse(this, this.showConnectionStatus(3));
                            }
                            break;
                        }
                        if (!this._connected && !this.IsConnected)
                        {
                            if (this.ConnectStatusResponse != null)
                            {
                                this.ConnectStatusResponse(this, this.showConnectionStatus(1));
                            }
                            break;
                        }
                    }
                    _old_connected = this.IsConnected;
                    _old_IsConnected = this._connected;
                }
                catch
                { }
            }
            SLogger.log("out ConnectStatusResponseThread()", "TCP_Client");
        }

        private bool isSocketConnected(Socket sock)
        {
            try
            {
                int num = 0;
                do
                {
                    if (sock == null)
                    {
                        return false;
                    }
                    if (!sock.Connected)
                    {
                        return false;
                    }
                    bool flag = sock.Poll(100, SelectMode.SelectRead);
                    bool flag2 = sock.Available == 0;
                    if (!((!flag || !flag2) && sock.Connected))
                    {
                        num++;
                    }
                    else
                    {
                        return true;
                    }
                }
                while (((num < 10) && (sock != null)) && sock.Connected);
            }
            catch (Exception exception)
            {
                this.CallException(exception.ToString());
            }
            return false;
        }

        private bool LoopConnectII(Socket sock, IPEndPoint host, int timeout = 0x1770)
        {
            lock (this._TcpSock_Lock)
            {
                try
                {
                    if ((sock == null) || (host == null))
                    {
                        return false;
                    }
                    IAsyncResult asyncResult = sock.BeginConnect(host, null, null);
                    bool flag = asyncResult.AsyncWaitHandle.WaitOne(timeout, true);
                    if (asyncResult.IsCompleted)
                    {
                        if (sock != null)
                        {
                            sock.EndConnect(asyncResult);
                        }
                        if (this.isSocketConnected(sock))
                        {
                            return true;
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.CallException(exception.ToString());
                }
                this.SocketDisconnect(sock);
                return false;
            }
        }

        private int Reconnect(ref Socket socket)
        {
            if (!this.disposed)
            {
                if (!this.isSocketConnected(socket))
                {
                    if (this.Connect(ref socket, this.IP, this.PORT))
                    {
                        return 1;
                    }
                    return -1;
                }
                return 0;
            }
            return -1;
        }

        private bool RecvDataFromServer()
        {
            if (this.tcpSynCl != null)
            {
                try
                {
                    int num = 0;
                    byte[] buffer = new byte[0x800];
                    lock (this.lock_RecvSyncData)
                    {
                        num = this.tcpSynCl.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                    }
                    if (num > 0)
                    {
                        byte[] destinationArray = new byte[num];
                        string data = string.Empty;
                        Array.Copy(buffer, 0, destinationArray, 0, destinationArray.Length);
                        data = Encoding.UTF8.GetString(destinationArray).ToString();
                        if (this.ReceiveData != null)
                        {
                            this.ReceiveData(this, data);
                        }
                        SLogger.log("recv_data=" + data, "TCP_Client");
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    this.CallException(exception.ToString());
                    if (this.ConnectStatusResponse != null)
                    {
                        this.ConnectStatusResponse(this, this.showConnectionStatus(6));
                    }
                    SLogger.log("RecvDataFromServer(); ConnectionStatusType.FailToRecvData", "TCP_Client");
                    SLogger.log("RecvDataFromServer(); Exception:" + exception.ToString(), "TCP_Client");
                    return false;
                }
            }
            if (this.ConnectStatusResponse != null)
            {
                this.ConnectStatusResponse(this, this.showConnectionStatus(4));
            }
            SLogger.log("RecvDataFromServer(); ConnectionStatusType.NoTCPClientSocket", "TCP_Client");
            return false;
        }

        private void RecvDataThread()
        {
            Func<bool> condition = null;
            SLogger.log("in RecvDataThread()", "TCP_Client");
            while (this.IsConnected)
            {
                try
                {
                    if (condition == null)
                    {
                        condition = () =>
                            ((!this.IsConnected || (this.tcpSynCl == null)) || ((this.tcpSynCl != null) && !this.tcpSynCl.Connected)) ||
                            ((this.tcpSynCl != null) && this.tcpSynCl.Poll(0, SelectMode.SelectRead));
                    }
                    SpinWait.SpinUntil(condition, -1);
                    if (((!this.IsConnected || (this.tcpSynCl == null)) || (!this.tcpSynCl.Connected || (this.tcpSynCl.Available == 0))) ||
                        !this.RecvDataFromServer())
                    {
                        break;
                    }
                }
                catch
                { }
            }
            SLogger.log("out RecvDataThread()", "TCP_Client");
        }

        private bool SetKeepAlive(Socket sock, ulong time, ulong interval)
        {
            if (sock == null)
            {
                return false;
            }
            try
            {
                byte[] optionInValue = new byte[12];
                ulong[] numArray = new ulong[3];
                if ((time == 0L) || (interval == 0L))
                {
                    numArray[0] = 0L;
                }
                else
                {
                    numArray[0] = 1L;
                }
                numArray[1] = time;
                numArray[2] = interval;
                for (int i = 0; i < numArray.Length; i++)
                {
                    optionInValue[(i * 4) + 3] = (byte)((numArray[i] >> 0x18) & ((ulong)0xffL));
                    optionInValue[(i * 4) + 2] = (byte)((numArray[i] >> 0x10) & ((ulong)0xffL));
                    optionInValue[(i * 4) + 1] = (byte)((numArray[i] >> 8) & ((ulong)0xffL));
                    optionInValue[i * 4] = (byte)(numArray[i] & ((ulong)0xffL));
                }
                byte[] bytes = BitConverter.GetBytes(0);
                sock.IOControl(IOControlCode.KeepAliveValues, optionInValue, bytes);
            }
            catch (Exception exception)
            {
                this.CallException(exception.ToString());
                return false;
            }
            return true;
        }

        private string showConnectionStatus(int errcode)
        {
            switch (errcode)
            {
                case 0:
                    return "Connected.";

                case 1:
                    return "Disconnected.";

                case 2:
                    return "Fail to connect.";

                case 3:
                    return "Connection lost.";

                case 4:
                    return "No TCP client socket.";

                case 5:
                    return "Fail to send data.";

                case 6:
                    return "Fail to recv data.";
            }
            return "Exception error.";
        }

        private void SocketClientInit()
        {
            new Thread(() => this.CheckSocketThread()) { IsBackground = true }.Start();
        }

        private bool SocketDisconnect(Socket sock)
        {
            if (sock != null)
            {
                try
                {
                    sock.Shutdown(SocketShutdown.Both);
                }
                catch
                { }
                try
                {
                    sock.Disconnect(false);
                }
                catch
                { }
                try
                {
                    sock.Close();
                }
                catch
                { }
                try
                {
                    sock.Dispose();
                }
                catch
                { }
                return true;
            }
            return false;
        }
    }
}