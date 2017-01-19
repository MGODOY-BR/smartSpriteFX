using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Controls.Animations.Frames
{
    /// <summary>
    /// It´s argument from FrameSelection event handler to handle error
    /// </summary>
    public class FrameSelectionErrorEventArgs: FrameSelectionEventArgs
    {
        /// <summary>
        /// It´s a text
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// It´s an exception
        /// </summary>
        public Exception Exception { get; set; }
    }
}
