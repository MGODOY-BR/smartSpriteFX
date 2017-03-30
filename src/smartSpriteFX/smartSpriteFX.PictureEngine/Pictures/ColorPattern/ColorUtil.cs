using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern
{
    /// <summary>
    /// Offers utilities to mantains colors
    /// </summary>
    public static class ColorUtil
    {
        /// <summary>
        /// Gets the color component slighty different to trick the transparent mechanism
        /// </summary>
        /// <param name="colorComponent"></param>
        /// <returns></returns>
        public static int GetSlightlyDifferentColorComponent(int colorComponent)
        {
            int factor = 5;

            if (colorComponent + factor > 255)
            {
                return colorComponent - factor;
            }
            else
            {
                return colorComponent + factor;
            }
        }

        /// <summary>
        /// Gets the color slighty different to trick the transparent mechanism 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetSlightlyDifferentColor(Color color)
        {
            return
                Color.FromArgb(
                    color.A,
                    color.R,
                    ColorUtil.GetSlightlyDifferentColorComponent(color.G),
                    color.B);
        }
    }
}
