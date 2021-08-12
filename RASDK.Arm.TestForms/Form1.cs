// #define DISABLE_SHOW_MESSAGE

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

namespace RASDK.Arm.TestForms
{
    public partial class Form1 : Form
    {
        private ArmActionFactory _arm;

        private readonly IMessage _message =
#if DISABLE_SHOW_MESSAGE
            new EmptyMessage();
#else
            new GeneralMessage(new EmptyLog());
#endif

        public Form1()
        {
            InitializeComponent();
            comboBoxArmType.SelectedIndex = 0;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (_arm == null)
            {
                _arm = GetArmType();

                textBoxIp.Enabled = false;
                textBoxPort.Enabled = false;
                comboBoxArmType.Enabled = false;
            }

            _arm.Connection().Open();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            _arm.Connection().Close();

            _arm = null;
            textBoxIp.Enabled = true;
            textBoxPort.Enabled = true;
            comboBoxArmType.Enabled = true;
        }

        private void buttonHoming_Click(object sender, EventArgs e)
        {
            _arm.Motion().Homing();
        }

        private void buttonMove1_Click(object sender, EventArgs e)
        {
            _arm.Motion().
                 Relative(-100,
                          0,
                          0,
                          0,
                          0,
                          0,
                          new AdditionalMotionParameters { NeedWait = false });
            MessageBox.Show("1");
            _arm.Motion().
                 Relative(100,
                          0,
                          0,
                          0,
                          0,
                          0,
                          new AdditionalMotionParameters { NeedWait = false });
            MessageBox.Show("2");
        }

        private void buttonMove2_Click(object sender, EventArgs e)
        {
            _arm.Motion().
                 Relative(100,
                          0,
                          0,
                          0,
                          0,
                          0,
                          new AdditionalMotionParameters { NeedWait = true });
            MessageBox.Show("1");
            _arm.Motion().
                 Relative(-100,
                          0,
                          0,
                          0,
                          0,
                          0,
                          new AdditionalMotionParameters { NeedWait = true });
            MessageBox.Show("2");
        }

        private ArmActionFactory GetArmType()
        {
            ArmActionFactory armActionFactory;
            switch (comboBoxArmType.SelectedItem.ToString())
            {
                case "HIWIN":
                    armActionFactory = new Hiwin.RoboticArm(textBoxIp.Text, _message);
                    break;

                case "TM Robot":
                    armActionFactory = new TMRobot.RoboticArm(textBoxIp.Text,
                                                              Int16.Parse(textBoxPort.Text),
                                                              _message);
                    break;

                default:
                    throw new Exception("未知的手臂類型。");
            }
            return armActionFactory;
        }

        #region Jog

        private void JogStart(int indexOfAxis, double value)
        {
            var dir = value >= 0 ? '+' : '-';
            _arm.Motion().Jog($"{dir}{indexOfAxis}");
        }

        private void JogStop()
        {
            _arm.Motion().Abort();
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