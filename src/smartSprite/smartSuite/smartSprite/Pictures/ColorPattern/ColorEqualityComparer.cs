using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSprite.Pictures.ColorPattern
{
    /// <summary>
    /// Represents a color equality comparer for colors
    /// </summary>
    public class ColorEqualityComparer : IEqualityComparer<Color>
    {
        public bool Equals(Color comparing, Color compareTo)
        {
            #region Entries validation

            if (comparing == null)
            {
                return false;
            }
            if (compareTo == null)
            {
                return false;
            }

            #endregion

            return comparing.ToArgb() == compareTo.ToArgb();
        }

        public int GetHashCode(Color obj)
        {
            #region Entries validation

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            #endregion

            return obj.ToArgb();
        }
    }
}
