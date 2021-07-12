using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RASDK.Arm.Action
{
    /// <summary>
    /// The action of arm.
    /// </summary>
    public interface IArmAction
    {
        /// <summary>
        /// The ID of arm.
        /// </summary>
        int ArmId { get; set; }

        /// <summary>
        /// The message of this action.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Enable wait for arm motion complete.
        /// </summary>
        bool NeedWait { get; set; }

        /// <summary>
        /// Do the action.
        /// </summary>
        /// <returns>Return true for successful.</returns>
        bool Do();
    }
}