using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSprite.SpriteEffectModule.Effects.Tools
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
            var r = Math.Abs(color.R - 255);
            var g = Math.Abs(color.G - 255);
            var b = Math.Abs(color.B - 255);

            return Color.FromArgb(color.A, NotGray(r), NotGray(g), NotGray(b));
        }

        /// <summary>
        /// Not allows the color component turns to gray
        /// </summary>
        /// <param name="colorComponent"></param>
        /// <returns></returns>
        private static int NotGray(int colorComponent)
        {
            if (colorComponent >= 120 && colorComponent <= 180)
            {
                return 0;
            }
            else
            {
                return colorComponent;
            }
        }
    }
}
