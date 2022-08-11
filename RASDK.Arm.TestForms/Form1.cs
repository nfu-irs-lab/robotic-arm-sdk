//#define DISABLE_SHOW_MESSAGE
//#define DISABLE_LOG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RASDK.Arm;
using RASDK.Basic;
using RASDK.Basic.Message;

namespace RASDK.Arm.TestForms
{
    public partial class Form1 : Form
    {
        private readonly LogHandler _logHandler;

        private readonly MessageHandler _message;

        private RoboticArm _arm;

        public Form1()
        {
            InitializeComponent();

            _logHandler =
#if DISABLE_LOg
            new EmptyLogHandler();
#else
            new GeneralLogHandler("");
#endif

            _message =
#if DISABLE_SHOW_MESSAGE
            new EmptyMessage();
#else
            new GeneralMessageHandler(_logHandler);
#endif

            comboBoxArmType.SelectedIndex = 0;
        }

        private void buttonCheckConnect_Click(object sender, EventArgs e)
        {
            _arm = _arm ?? RoboticArmFactory();

            var connected = _arm.Connected;
            _message.Show($"Connected: {connected}.");
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            _arm = _arm ?? RoboticArmFactory();

            if (_arm.Connect())
            {
                textBoxIp.Enabled = false;
                textBoxPort.Enabled = false;
                comboBoxArmType.Enabled = false;
            }
            else
            {
                _arm = null;
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            _arm?.Disconnect();
            _arm = null;

            textBoxIp.Enabled = true;
            textBoxPort.Enabled = true;
            comboBoxArmType.Enabled = true;
        }

        private void buttonHoming_Click(object sender, EventArgs e)
        {
            _arm.Homing();
            _message.Show("Homing done!");
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            double[] position;
            switch (comboBoxArmType.SelectedItem.ToString())
            {
                case "HIWIN":
                    position = Hiwin.Default.DescartesHomePosition;
                    break;

                case "TM Robot":
                    position = TMRobot.Default.DescartesHomePosition;
                    break;

                default:
                    throw new Exception("未知的手臂類型。");
            }
            position[0] += 50;
            position[1] += 50;
            position[2] -= 100;

            _arm.MoveAbsolute(position, new AdditionalMotionParameters { NeedWait = true });
        }

        private void buttonMove1_Click(object sender, EventArgs e)
        {
            _arm.MoveRelative(-100,
                          0,
                          0,
                          0,
                          0,
                          0,
                          new AdditionalMotionParameters { NeedWait = false });
            MessageBox.Show("1");
            _arm.MoveRelative(100,
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
            _arm.MoveRelative(100,
                          0,
                          0,
                          0,
                          0,
                          0,
                          new AdditionalMotionParameters { NeedWait = true });
            MessageBox.Show("1");
            _arm.MoveRelative(-100,
                          0,
                          0,
                          0,
                          0,
                          0,
                          new AdditionalMotionParameters { NeedWait = true });
            MessageBox.Show("2");
        }

        private RoboticArm RoboticArmFactory()
        {
            RoboticArm arm;
            switch (comboBoxArmType.SelectedItem.ToString())
            {
                case "HIWIN":
                    arm = new Hiwin.RoboticArm(_message, textBoxIp.Text);
                    break;

                case "TM Robot":
                    arm = new TMRobot.RoboticArm(_message,
                                                 textBoxIp.Text,
                                                 Int16.Parse(textBoxPort.Text));
                    break;

                case "CoppeliaSim":
                    arm = new CoppeliaSim.RoboticArm(_message,
                                                     textBoxIp.Text,
                                                     Int16.Parse(textBoxPort.Text));
                    break;

                default:
                    throw new Exception("未知的手臂類型。");
            }
            return arm;
        }

        #region Jog

        private void JogStart(int indexOfAxis, double value)
        {
            var dir = value >= 0 ? '+' : '-';
            _arm.Jog($"{dir}{indexOfAxis}");
        }

        private void JogStop()
        {
            _arm.Abort();
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