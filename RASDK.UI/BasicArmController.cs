using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RASDK.Basic;
using RASDK.Basic.Message;
using RASDK.Arm;
using RASDK.Arm.Type;

namespace RASDK.UI
{
    public partial class BasicArmController : UserControl
    {
        private IMessage _message;
        private ArmActionFactory _arm;

        public BasicArmController()
        {
            InitializeComponent();

            _armNowPositionControllers = new List<TextBox>
            {
                textBoxArmNowPositionXJ1,
                textBoxArmNowPositionYJ2,
                textBoxArmNowPositionZJ3,
                textBoxArmNowPositionAJ4,
                textBoxArmNowPositionBJ5,
                textBoxArmNowPositionCJ6
            };

            _armTargetPositionControllers = new List<NumericUpDown>
            {
                numericUpDownArmTargetPositionXJ1,
                numericUpDownArmTargetPositionYJ2,
                numericUpDownArmTargetPositionZJ3,
                numericUpDownArmTargetPositionAJ4,
                numericUpDownArmTargetPositionBJ5,
                numericUpDownArmTargetPositionZJ6
            };
        }

        public void DependencyInjection(ArmActionFactory armController, IMessage message)
        {
            _message = message;
            _arm = armController;
        }

        private void UpdateNowPosition()
        {
            // _nowPosition = _arm.GetPosition(_nowCoordinateType);
        }

        #region Type

        private CoordinateType _nowCoordinateType
        {
            get
            {
                if (radioButtonCoordinateTypeDescartes.Checked)
                    return CoordinateType.Descartes;
                else if (radioButtonCoordinateTypeJoint.Checked)
                    return CoordinateType.Joint;
                else
                    return CoordinateType.Unknown;
            }

            set
            {
                switch (value)
                {
                    case CoordinateType.Descartes:
                        radioButtonCoordinateTypeDescartes.Checked = true;
                        break;

                    case CoordinateType.Joint:
                        radioButtonCoordinateTypeJoint.Checked = true;
                        break;
                }
            }
        }

        private MotionType _nowMotionType
        {
            get
            {
                if (radioButtonMotionTypePointToPoint.Checked)
                    return MotionType.PointToPoint;
                else if (radioButtonMotionTypeLinear.Checked)
                    return MotionType.Linear;
                else
                    return MotionType.Unknown;
            }

            set
            {
                switch (value)
                {
                    case MotionType.PointToPoint:
                        radioButtonMotionTypePointToPoint.Checked = true;
                        break;

                    case MotionType.Linear:
                        radioButtonMotionTypeLinear.Checked = true;
                        break;
                }
            }
        }

        private PositionType _nowPositionType
        {
            get
            {
                if (radioButtonPositionTypeAbsolute.Checked)
                    return PositionType.Absolute;
                else if (radioButtonPositionTypeRelative.Checked)
                    return PositionType.Relative;
                else
                    return PositionType.Unknown;
            }

            set
            {
                switch (value)
                {
                    case PositionType.Absolute:
                        radioButtonPositionTypeAbsolute.Checked = true;
                        break;

                    case PositionType.Relative:
                        radioButtonPositionTypeRelative.Checked = true;
                        break;
                }
            }
        }

        #endregion Type

        #region Position

        private readonly List<TextBox> _armNowPositionControllers;
        private readonly List<NumericUpDown> _armTargetPositionControllers;

        private double[] _nowPosition
        {
            get
            {
                double[] pos = new double[6];
                for (var i = 0; i < 6; i++)
                {
                    try
                    {
                        pos[i] = Convert.ToDouble(_armNowPositionControllers[i].Text);
                    }
                    catch (Exception ex)
                    {
                        _message.Show(ex, LoggingLevel.Warn);
                        pos = null;
                    }
                }
                return pos;
            }

            set
            {
                if (value.Length == 6 || value.Length == 3)
                {
                    for (var i = 0; i < value.Length; i++)
                    {
                        _armNowPositionControllers[i].Text = value[i].ToString();
                    }
                }
            }
        }

        private double[] _targetPosition
        {
            get
            {
                double[] pos = new double[6];
                for (var i = 0; i < 6; i++)
                {
                    try
                    {
                        pos[i] = Convert.ToDouble(_armTargetPositionControllers[i].Value);
                    }
                    catch (Exception ex)
                    {
                        _message.Show(ex, LoggingLevel.Warn);
                        pos = null;
                    }
                }
                return pos;
            }

            set
            {
                if (value.Length == 6 || value.Length == 3)
                {
                    for (var i = 0; i < value.Length; i++)
                    {
                        _armTargetPositionControllers[i].Value = Convert.ToDecimal(value[i]);
                    }
                }
            }
        }

        #endregion Position

        #region Speed and Acceleration

        private int _nowAcceleration
        {
            get { return Convert.ToInt32(numericUpDownArmAcceleration); }

            set
            {
                //_message.Log($"Set arm acceleration:{value}.", LoggingLevel.Info);
                if (value > 0 && value <= 100)
                    numericUpDownArmAcceleration.Value = value;
            }
        }

        private int _nowSpeed
        {
            get { return Convert.ToInt32(numericUpDownArmSpeed); }

            set
            {
                //_message.Log($"Set arm speed:{value}.", LoggingLevel.Info);
                if (value > 0 && value <= 100)
                    numericUpDownArmSpeed.Value = value;
            }
        }

        #endregion Speed and Acceleration

        #region Event

        private void buttonArmCopyPositionFromNowToTarget_Click(object sender, EventArgs e)
        {
            _targetPosition = _nowPosition;
        }

        private void buttonArmHoming_Click(object sender, EventArgs e)
        {
            if (checkBoxArmSlowlyHoming.Checked)
            {
                // _arm.Speed = 5;
                // _arm.Acceleration = 10;

                Thread.Sleep(300);

                _arm.Motion().Homing(_nowCoordinateType);

                // _arm.Speed = _nowSpeed;
                // _arm.Acceleration = _nowAcceleration;
            }
            else
            {
                _arm.Motion().Homing(_nowCoordinateType);
            }

            UpdateNowPosition();
        }

        private void buttonArmMotionStart_Click(object sender, EventArgs e)
        {
            // IArmAction act;
            switch (_nowPositionType)
            {
                case PositionType.Absolute:
                    _arm.Motion().
                         Absolute(_targetPosition,
                                  new AdditionalMotionParameters
                                  {
                                      MotionType = _nowMotionType,
                                      CoordinateType = _nowCoordinateType
                                  });
                    break;

                case PositionType.Relative:
                    _arm.Motion().
                         Relative(_targetPosition,
                                  new AdditionalMotionParameters
                                  {
                                      MotionType = _nowMotionType,
                                      CoordinateType = _nowCoordinateType
                                  });
                    break;

                default:
                    _message.Show("未知的位置類型。", LoggingLevel.Warn);
                    break;
            }

            UpdateNowPosition();
        }

        private void buttonArmUpdateNowPosition_Click(object sender, EventArgs e)
        {
            UpdateNowPosition();
        }

        private void buttonSetSpeedAndAcceleration_Click(object sender, EventArgs e)
        {
            // _arm.Speed = _nowSpeed;
            // _arm.Acceleration = _nowAcceleration;

            Thread.Sleep(300);

            // _nowSpeed = _arm.Speed;
            // _nowAcceleration = _arm.Acceleration;
        }

        private void radioButtonCoordinateTypeDescartes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCoordinateTypeJoint.Checked)
            { }
            else
            { }
        }

        private void radioButtonPositionTypeAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPositionTypeRelative.Checked)
            {
                _targetPosition = new double[] { 0, 0, 0, 0, 0, 0 };
            }
            else
            { }
        }

        #endregion Event
    }
}