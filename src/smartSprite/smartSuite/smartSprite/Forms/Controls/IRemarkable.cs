using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSpriteFX.Forms.Controls
{
    /// <summary>
    /// Defines a control which can be marked as bold
    /// </summary>
    public interface IRemarkable
    {
        /// <summary>
        /// Marks the control
        /// </summary>
        /// <param name="bold"></param>
        void Mark(bool bold);
    }
}
