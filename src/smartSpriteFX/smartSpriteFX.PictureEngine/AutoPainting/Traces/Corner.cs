using smartSuite.smartSprite.AutoPainting.Traces;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces
{
    /// <summary>
    /// Represents the corner between two lines
    /// </summary>
    public class Corner
    {
        /// <summary>
        /// It's the vertice of angle
        /// </summary>
        public Point Vertice { get; set; }

        /// <summary>
        /// It's the angle of corner
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Draws a caption in a picture used for debug purposes
        /// </summary>
        /// <param name="debugPicture"></param>
        /// <param name="caption"></param>
        public void DrawDebug(string caption, Picture debugPicture)
        {
            debugPicture.DrawString(caption, this.Vertice.X, this.Vertice.Y);
        }
    }
}
