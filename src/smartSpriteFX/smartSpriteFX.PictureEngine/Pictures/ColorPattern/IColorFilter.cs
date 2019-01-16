using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern
{
    /// <summary>
    /// Defines how a color filter must be
    /// </summary>
    public interface IColorFilter
    {
        /// <summary>
        /// Gets if the color is valid
        /// </summary>
        /// <returns></returns>
        bool IsValid(int x, int y, Color color);
    }
}
