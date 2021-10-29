using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.CoppeliaSim
{
    internal class Connection : IConnection
    {
        public bool IsOpen => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}