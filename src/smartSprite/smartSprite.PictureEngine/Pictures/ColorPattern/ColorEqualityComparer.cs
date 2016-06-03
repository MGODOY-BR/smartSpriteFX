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
        /// <summary>
        /// It´s a sensibility, used to check similiar colors
        /// </summary>
        private const int SENSIBILITY = 30;

        /// <summary>
        /// Gets a indicator informing if whe colors are similar
        /// </summary>
        /// <param name="comparing"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        public bool LooksLike(Color comparing, Color compareTo)
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
            if (comparing.Equals(compareTo))
            {
                return true;
            }
            if (this.Equals(compareTo, Color.Transparent))
            {
                return false;
            }
            if (this.Equals(comparing, Color.Transparent))
            {
                return false;
            }

            #endregion

            var comparingR = comparing.R;
            var comparingG = comparing.G;
            var comparingB = comparing.B;

            var compareToR = compareTo.R;
            var compareToG = compareTo.G;
            var compareToB = compareTo.B;

            int[] factorArray = new int[3];
            factorArray[0] = comparingR - compareToR;
            factorArray[1] = comparingG - compareToG;
            factorArray[2] = comparingB - compareToB;

            int refFactor = Math.Abs(factorArray[0]);

            bool similarity = true;
            for (int i = 0; i < factorArray.Length; i++)
            {
                similarity &=
                    (
                        CheckSimiliarity(refFactor, factorArray[i]) || factorArray[i] == 0
                    )
                    && refFactor < 20;
            }

            return similarity;
        }

        /// <summary>
        /// Checks if there's some similiarity between the comparer factor
        /// </summary>
        private static bool CheckSimiliarity(int refFactor, int factor)
        {
            int compareFactor = Math.Abs(factor - refFactor);
            return compareFactor == 0 || compareFactor < SENSIBILITY;
        }

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
