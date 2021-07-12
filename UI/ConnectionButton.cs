using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace UI
{
    public partial class ConnectionButton : UserControl
    {
        private List<IDevice> _devices;
        private IMessage _message;

        public ConnectionButton()
        {
            InitializeComponent();
        }

        public void DependencyInjection(List<IDevice> devices, IMessage message)
        {
            _message = message;
            _devices = devices;
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