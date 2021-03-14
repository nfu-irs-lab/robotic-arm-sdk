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

        private void buttonChooseCamera_Click(object sender, EventArgs e)
        {
            Camera.ChooseCamera();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Camera.Exit();
        }

        private void buttonOpenFreeRun_Click(object sender, EventArgs e)
        {
            Camera.Open(CaptureMode.FreeRun, true);
        }
    }
}