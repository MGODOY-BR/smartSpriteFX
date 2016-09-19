using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSprite.PictureEngine.Pictures
{
    /// <summary>
    /// Represents a composition of point and aditional information
    /// </summary>
    public class PointInfo : smartSuite.smartSprite.Pictures.Point
    {
        public PointInfo(smartSuite.smartSprite.Pictures.Point point, Color newColor) : base(point.X, point.Y)
        {
            this.Color = newColor;
        }

        public PointInfo(float x, float y, Color color) : base(x, y)
        {
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets color
        /// </summary>
        public Color Color
        {
            get;
            private set;
        }
    }
}
