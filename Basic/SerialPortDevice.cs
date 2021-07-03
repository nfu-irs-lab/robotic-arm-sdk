using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using Basic.Message;
using Basic.File;

namespace Basic
{
    /// <summary>
    /// 基本 Serial Port 裝置介面。
    /// </summary>
    public interface ISerialPortDevice : IDevice
    {
        SerialPort SerialPort { get; set; }
    }

    /// <summary>
    /// 基本 Serial Port 裝置實作。
    /// </summary>
    public class SerialPortDevice : ISerialPortDevice
    {
        public SerialPortDevice(SerialPort serialPort, IMessage message)
        {
            SerialPort = serialPort;
            Message = message;
        }

        public SerialPortDevice(string comPort, IMessage message)
        {
            SerialPort = new SerialPort(comPort);
            Message = message;
        }

        public bool Connected { get; private set; } = false;
        public SerialPort SerialPort { get; set; }
        private IMessage Message { get; set; }

        public virtual bool Connect()
        {
            if (!SerialPort.IsOpen)
            {
                try
                {
                    SerialPort.Open();
                    Thread.Sleep(50);
                    if (SerialPort.IsOpen)
                    {
                        Connected = true;
                        return true;
                    }
                    else
                    {
                        Connected = false;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Message.Show("無法進行連線。\r\n請檢查COM Port等設定。", ex, LoggingLevel.Error);
                    return false;
                }
            }
            else
            {
                Connected = true;
                return true;
            }
        }

        public virtual bool Disconnect()
        {
            if (SerialPort.IsOpen)
            {
                try
                {
                    SerialPort.Close();
                    Thread.Sleep(50);
                    if (SerialPort.IsOpen)
                    {
                        Connected = true;
                        return false;
                    }
                    else
                    {
                        Connected = false;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Message.Show("無法進行斷線。\r\n請檢查COM Port等設定。", ex, LoggingLevel.Error);
                    return false;
                }
            }
            else
            {
                Connected = false;
                return true;
            }
        }
    }
}