using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arm.Hiwin;
using Basic;
using Basic.Message;

namespace Arm.TestForms
{
    public partial class Form1 : Form
    {
        private ArmActionFactory _arm;

        private IMessage _message =
            //new GeneralMessage(new EmptyLog());
            new EmptyMessage();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (_arm == null)
            {
                _arm = new HiwinArmActionFactory(textBoxIp.Text, _message);
                textBoxIp.Enabled = false;
            }

            _arm.Connect();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            _arm.Disconnect();
        }

        private void buttonHoming_Click(object sender, EventArgs e)
        {
            _arm.Homing();
        }

        private void buttonJogYP_MouseDown(object sender, MouseEventArgs e)
        { }

        private void buttonJogYP_MouseUp(object sender, MouseEventArgs e)
        { }

        private void buttonMove1_Click(object sender, EventArgs e)
        {
            _arm.RelativeMotion(100, 0, 0, 0, 0, 0, new AdditionalMotionParameters() { NeedWait = false });
            MessageBox.Show("1");
            _arm.RelativeMotion(-100, 0, 0, 0, 0, 0, new AdditionalMotionParameters() { NeedWait = true });
            MessageBox.Show("2");
        }

        private void buttonMove2_Click(object sender, EventArgs e)
        { }
    }
}