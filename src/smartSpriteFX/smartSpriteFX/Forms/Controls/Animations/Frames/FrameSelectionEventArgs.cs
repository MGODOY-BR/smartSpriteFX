using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Controls.Animations.Frames
{
    /// <summary>
    /// It´s argument from FrameSelection event handler
    /// </summary>
    public class FrameSelectionEventArgs : EventArgs
    {
        /// <summary>
        /// It´s the frame index in an animation
        /// </summary>
        public int FrameIndex { get; set; }

        /// <summary>
        /// It´s the path of animation
        /// </summary>
        public string FilePath { get; set; }
    }
}
