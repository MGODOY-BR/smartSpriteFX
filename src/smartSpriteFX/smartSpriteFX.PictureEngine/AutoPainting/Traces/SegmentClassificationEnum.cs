using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces
{
    /// <summary>
    /// Relates the classification of a piece of line
    /// </summary>
    public enum SegmentClassificationEnum
    {
        UNRELATED,
        UNDETERMINED,
        STRAIGHT_HORIZONTALLY,
        STRAIGHT_VERTICALLY,
        INCLINATION,
    }
}
