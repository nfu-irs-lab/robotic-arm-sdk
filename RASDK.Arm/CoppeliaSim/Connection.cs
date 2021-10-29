using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.CoppeliaSim
{
    internal class Connection : BasicAction, IConnection
    {
        private static unsafe int* _idPointer;
        private readonly string _ip;
        private readonly int _port;

        public Connection(string ip, int port, IMessage message, ref int id)
            : base(id, message)
        {
            _ip = ip;
            _port = port;

            unsafe
            {
                fixed (int* i = &id)
                {
                    _idPointer = i;
                }
            }
        }

        public bool IsOpen => CoppeliaSimRemoteApi.IsConnected(_id);

        public void Close()
        {
            var returnCode = CoppeliaSimRemoteApi.Disconnect(_id);
            _message.Show($"斷線.\r\n回傳：{returnCode}");
        }

        public void Open()
        {
            _id = CoppeliaSimRemoteApi.Connect(_ip, _port);

            // Check connection.
            if (_id != -1)
            {
                ShowSuccessfulConnectMessage();
            }
            else
            {
                ShowUnsuccessfulConnectMessage();
            }

            unsafe
            {
                *_idPointer = _id;
            }
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
    }
}