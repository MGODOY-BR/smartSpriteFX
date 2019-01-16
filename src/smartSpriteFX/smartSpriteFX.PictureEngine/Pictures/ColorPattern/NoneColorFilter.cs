using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern
{
    /// <summary>
    /// Represents a filter color with no effect at all
    /// </summary>
    [Serializable]
    public class NoneColorFilter : IColorFilter
    {
        public bool IsValid(int x, int y, Color color)
        {
            return true;
        }
    }
}
