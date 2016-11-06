using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern
{
    /// <summary>
    /// Represents a color comparer based on similiarity between color, used for pour acuracy.
    /// </summary>
    public class LookLikeColorComparer : IEqualityComparer<Color>
    {
        /// <summary>
        /// It´s a inner comparer
        /// </summary>
        private ColorEqualityComparer innerComparer = new ColorEqualityComparer();

        public bool Equals(Color oldColor, Color newColor)
        {
            return innerComparer.LooksLike(oldColor, newColor);
        }

        public int GetHashCode(Color obj)
        {
            return innerComparer.GetHashCode(obj);
        }
    }
}
