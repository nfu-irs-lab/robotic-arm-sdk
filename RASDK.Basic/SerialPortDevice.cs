using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using RASDK.Basic.Message;

namespace RASDK.Basic
{
    /// <summary>
    /// 基本 Serial Port 裝置介面。
    /// </summary>
    public interface ISerialPortDevice : IDevice
    {
        /// <summary>
        /// 底層 SerialPort。
        /// </summary>
        SerialPort SerialPort { get; set; }
    }

    /// <summary>
    /// 基本 Serial Port 裝置。
    /// </summary>
    public class SerialPortDevice : ISerialPortDevice
    {
        /// <summary>
        /// 基本 Serial Port 裝置。
        /// </summary>
        public SerialPortDevice(SerialPort serialPort, IMessage message)
        {
            SerialPort = serialPort;
            Message = message;
        }

        /// <summary>
        /// 基本 Serial Port 裝置。
        /// </summary>
        public SerialPortDevice(string comPort, IMessage message)
        {
            SerialPort = new SerialPort(comPort);
            Message = message;
        }

        /// <summary>
        /// 已連線。
        /// </summary>
        public bool Connected { get; private set; } = false;

        /// <summary>
        /// 底層 SerialPort。
        /// </summary>
        public SerialPort SerialPort { get; set; }
        private IMessage Message { get; set; }

        /// <summary>
        /// 進行連線。
        /// </summary>
        /// <returns>是否成功連線。</returns>
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

        /// <summary>
        /// 進行斷線。
        /// </summary>
        /// <returns>是否成功斷線。。</returns>
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