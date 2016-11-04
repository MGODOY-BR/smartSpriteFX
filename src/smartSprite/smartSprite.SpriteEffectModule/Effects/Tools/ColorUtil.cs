using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSpriteFX.SpriteEffectModule.Effects.Tools
{
    /// <summary>
    /// Offers utilities for color operations
    /// </summary>
    public static class ColorUtil
    {
        /// <summary>
        /// Inverts the color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color Invert(Color color)
        {
            var r = 255 - color.R;
            var g = 255 - color.G;
            var b = 255 - color.B;

            return Color.FromArgb(color.A, r, g, b);
        }
    }
}
