using System;
using System.Drawing;

namespace smartSpriteFX.Pictures.ColorPattern
{
    /// <summary>
    /// Represents a difference between two colors expressed by their components
    /// </summary>
    public class ColorFactor
    {
        /// <summary>
        /// Gets or sets the correspondent color component
        /// </summary>
        public float A { get; set; }
        /// <summary>
        /// Gets or sets the correspondent color component
        /// </summary>
        public float R { get; set; }
        /// <summary>
        /// Gets or sets the correspondent color component
        /// </summary>
        public float G { get; set; }
        /// <summary>
        /// Gets or sets the correspondent color component
        /// </summary>
        public float B { get; set; }

        /// <summary>
        /// Gets a color factor by difference
        /// </summary>
        /// <param name="comparing"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        public static ColorFactor GetColorFactorByDifference(Color comparing, Color compareTo)
        {
            #region Entries validation
            
            if (comparing == null)
            {
                throw new ArgumentNullException("comparing");
            }
            if (compareTo == null)
            {
                throw new ArgumentNullException("compareTo");
            }

            #endregion

            var comparingR = comparing.R;
            var comparingG = comparing.G;
            var comparingB = comparing.B;

            var compareToR = compareTo.R;
            var compareToG = compareTo.G;
            var compareToB = compareTo.B;

            return new ColorFactor
            {
                R = Math.Abs((float)comparingR - (float)compareToR),
                G = Math.Abs((float)comparingG - (float)compareToG),
                B = Math.Abs((float)comparingB - (float)compareToB)
            };
        }

        /// <summary>
        /// Gets a color factor by division
        /// </summary>
        /// <param name="comparing"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        public static ColorFactor GetColorFactorByDivision(Color comparing, Color compareTo)
        {
            #region Entries validation

            if (comparing == null)
            {
                throw new ArgumentNullException("comparing");
            }
            if (compareTo == null)
            {
                throw new ArgumentNullException("compareTo");
            }

            #endregion

            var comparingR = comparing.R;
            var comparingG = comparing.G;
            var comparingB = comparing.B;

            var compareToR = compareTo.R;
            var compareToG = compareTo.G;
            var compareToB = compareTo.B;

            return new ColorFactor
            {
                R = (float)comparingR / (float)compareToR,
                G = (float)comparingG / (float)compareToG,
                B = (float)comparingB / (float)compareToB
            };
        }

    }
}