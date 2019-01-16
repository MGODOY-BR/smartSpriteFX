using smartSuite.smartSprite.AutoPainting;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.AutoPainting
{
    /// <summary>
    /// Represents a version of point and relative prepared to be grouped in polygons
    /// </summary>
    [Obsolete()]
    public class LinkedPointAndPolygon : LinkedPoint
    {
        /// <summary>
        /// A polygon belonged
        /// </summary>
        public virtual Polygon Polygon { get; set; }

        /// <summary>
        /// Indicates if the point has been checked in distribute proccess
        /// </summary>
        public virtual bool HasDistributed { get; set; }
    }
}
