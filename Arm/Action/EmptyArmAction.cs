using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arm.Action
{
    /// <summary>
    /// Empty arm action.
    /// </summary>
    public class EmptyArmAction : IArmAction
    {
        private readonly bool _returnValue;

        /// <summary>
        /// Empty arm action.
        /// </summary>
        /// <param name="returnValue"></param>
        public EmptyArmAction(bool returnValue = true)
        {
            _returnValue = returnValue;
        }

        public int ArmId { get; set; }
        public string Message => "Empty arm action.";

        public bool NeedWait
        {
            get => false;
            set { }
        }

        public bool Do()
        {
            return _returnValue;
        }
    }
}