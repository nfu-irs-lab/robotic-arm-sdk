using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RASDK.Arm.Hiwin;
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

        private void buttonMove1_Click(object sender, EventArgs e)
        {
            _arm.RelativeMotion(100, 0, 0, 0, 0, 0, new AdditionalMotionParameters() { NeedWait = false });
            MessageBox.Show("1");
            _arm.RelativeMotion(-100, 0, 0, 0, 0, 0, new AdditionalMotionParameters() { NeedWait = true });
            MessageBox.Show("2");
        }

        private void buttonMove2_Click(object sender, EventArgs e)
        { }

        #region Jog

        private void JogStart(int indexOfAxis, double value)
        {
            var dir = value >= 0 ? '+' : '-';
            _arm.Jog($"{dir}{indexOfAxis}");
        }

        private void JogStop()
        {
            _arm.AbortMotion();
        }

        #region X

        private void buttonJogXM_MouseDown(object sender, MouseEventArgs e)
            => JogStart(0, (double)-numericUpDownJogXY.Value);

        private void buttonJogXM_MouseUp(object sender, MouseEventArgs e)
            => JogStop();

        private void buttonJogXP_MouseDown(object sender, MouseEventArgs e)
            => JogStart(0, (double)numericUpDownJogXY.Value);

        private void buttonJogXP_MouseUp(object sender, MouseEventArgs e)
            => JogStop();

        #endregion X

        #region Y

        private void buttonJogYM_MouseDown(object sender, MouseEventArgs e)
            => JogStart(1, (double)-numericUpDownJogXY.Value);

        private void buttonJogYM_MouseUp(object sender, MouseEventArgs e)
            => JogStop();

        private void buttonJogYP_MouseDown(object sender, MouseEventArgs e)
            => JogStart(1, (double)numericUpDownJogXY.Value);

        private void buttonJogYP_MouseUp(object sender, MouseEventArgs e)
            => JogStop();

        #endregion Y

        #region Z

        private void buttonJogZM_MouseDown(object sender, MouseEventArgs e)
            => JogStart(2, (double)-numericUpDownJogZ.Value);

        private void buttonJogZM_MouseUp(object sender, MouseEventArgs e)
            => JogStop();

        private void buttonJogZP_MouseDown(object sender, MouseEventArgs e)
            => JogStart(2, (double)numericUpDownJogZ.Value);

        private void buttonJogZP_MouseUp(object sender, MouseEventArgs e)
            => JogStop();

        #endregion Z

        #endregion Jog
    }
}