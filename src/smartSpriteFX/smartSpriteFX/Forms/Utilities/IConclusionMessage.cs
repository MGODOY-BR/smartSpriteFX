using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities
{
    /// <summary>
    /// Defines a conclusion message that needs be shown
    /// </summary>
    public interface IConclusionMessage
    {
        /// <summary>
        /// Shows the message
        /// </summary>
        /// <param name="message"></param>
        void Show(string message);
    }
}
