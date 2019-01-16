using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces
{
    /// <summary>
    /// Represents an extension of the point
    /// </summary>
    public class PointHandExtention
    {
        /// <summary>
        /// Gets or sets the amount of extention
        /// </summary>
        public int ExtentionCount { get; private set; }

        /// <summary>
        /// Registers an extention
        /// </summary>
        public void Registry()
        {
            this.ExtentionCount++;
        }
    }
}
