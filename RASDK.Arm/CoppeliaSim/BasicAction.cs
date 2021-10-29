using RASDK.Basic.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.CoppeliaSim
{
    internal abstract class BasicAction
    {
        protected readonly IMessage _message;
        protected int _id;

        public BasicAction(int id, IMessage message)
        {
            _id = id;
            _message = message;
        }
    }
}