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
using Arm;

namespace UI
{
    public partial class BasicArmController : UserControl
    {
        private IArmController _armController;
        private IMessage _message;

        public BasicArmController()
        {
            InitializeComponent();
        }

        public void DependencyInjection(IArmController armController, IMessage message)
        {
            _message = message;
            _armController = armController;
        }
    }
}