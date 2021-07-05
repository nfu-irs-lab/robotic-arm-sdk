using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basic;
using Basic.Message;

namespace UI
{
    public partial class ConnectionButton : UserControl
    {
        public readonly List<IDevice> _devices;
        public readonly IMessage _message;

        public ConnectionButton()
        {
            InitializeComponent();

            //            _devices = devices;
            //           _message = message;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            _message.Log("Connect button click.", LoggingLevel.Trace);

            for (var i = 0; i < _devices.Count; i++)
            {
                _devices[i].Connect();
            }

            buttonConnect.Enabled = !buttonConnect.Enabled;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            _message.Log("Disconnect button click.", LoggingLevel.Trace);

            for (var i = 0; i < _devices.Count; i++)
            {
                _devices[i].Disconnect();
            }

            buttonConnect.Enabled = true;
        }
    }
}