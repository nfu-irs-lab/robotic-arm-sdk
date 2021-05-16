using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFUIRSL.HRTK.Vision.TestForm
{
    internal partial class VisionTestForm : Form
    {
        private readonly IDSCamera Camera;

        public VisionTestForm()
        {
            InitializeComponent();
            Camera = new IDSCamera(pictureBoxDisplay, new EmptyMessage());
        }

        private void buttonCapModeSnapshot_Click(object sender, EventArgs e)
        {
            Camera.ChangeCaptureMode(CaptureMode.Snapshot);
        }

        private void buttonChooseCamera_Click(object sender, EventArgs e)
        {
            Camera.ChooseCamera();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Camera.Disconnect();
        }

        private void buttonOpenFreeRun_Click(object sender, EventArgs e)
        {
            Camera.Connect();
        }

        private void buttonShowSettingForm_Click(object sender, EventArgs e)
        {
            Camera.ShowSettingForm();
        }

        private void radioButtonCapModeFreeRun_CheckedChanged(object sender, EventArgs e)
        {
            var mode = radioButtonCapModeFreeRun.Checked ? CaptureMode.FreeRun : CaptureMode.Stop;
            Camera.ChangeCaptureMode(mode);
        }
    }
}