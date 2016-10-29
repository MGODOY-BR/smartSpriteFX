
using smartSprite.Pictures.ColorPattern;
using smartSuite.smartSprite.Effects.Core;
using smartSuite.smartSprite.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Infra
{
    /// <summary>
    /// A buffer of colors, used to limt the quality of image
    /// </summary>
    public class ColorBuffer
    {

        /// <summary>
        /// It´s the maximum of allowed colors.
        /// </summary>
        private int _maxLength;

        /// <summary>
        /// It´s a comparer for colors
        /// </summary>
        private ColorEqualityComparer _comparer;

        /// <summary>
        /// It´s an eventual TransparentBackgroundFilter registered on the EffectEngine.
        /// </summary>
        private TransparentBackgroundFilter _registeredTransparentBackgroundFilter;

        /// <summary>
        /// It´s a sensibility of colors.
        /// </summary>
        private int _contrast;

        /// <summary>
        /// It´s a length of range of colors
        /// </summary>
        private int _rangeLength;

        /// <summary>
        /// It´s a list of avoided colors
        /// </summary>
        public List<Color> AvoidedColorList { get; set; }

        /// <summary>
        /// Creates an instance of object
        /// </summary>
        /// <param name="length">It´s the maximum amount of allowed colors.</param>
        /// <param name="contrast">It´s how different the colors are each others</param>
        public ColorBuffer(int length, int contrast)
        {
            #region Entries validation

            if (length < 4 && length != 0)
            {
                throw new ArgumentOutOfRangeException("Invalid color length. Inform minimun 4, or 0 to unlimited colors.");
            }

            #endregion

            this._maxLength = length;
            this._contrast = contrast;
            this._rangeLength = this.GetRangeLength(this._maxLength);

            //if (this._maxLength == 0)
            //{
                this._comparer = new ColorEqualityComparer(0);
            //}
            //else
            //{
            //    this._comparer = new ColorEqualityComparer(this._contrast);
            //}

            #region Getting the transparent background filter

            if (this._registeredTransparentBackgroundFilter == null)
            {
                foreach (var filterItem in EffectEngine.GetSelectedFilterList())
                {
                    this._registeredTransparentBackgroundFilter = filterItem as TransparentBackgroundFilter;
                    if (this._registeredTransparentBackgroundFilter != null)
                    {
                        break;
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// Gets a color similar to assigned color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public Color GetSimilarColor(Color color)
        {
            var returnColor =
                Color.FromArgb(
                    color.A,
                    this.FitColorComponent(color.R, this._rangeLength),
                    this.FitColorComponent(color.G, this._rangeLength),
                    this.FitColorComponent(color.B, this._rangeLength));

            // Skipping transparent color
            returnColor = this.TrickTransparentColor(returnColor);

            return returnColor;
        }

        /// <summary>
        /// Clears the color buffer
        /// </summary>
        public void Clear()
        {
        }

        /// <summary>
        /// Counts the amount of buffer
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the color component slighty different to trick the transparent mechanism
        /// </summary>
        /// <param name="colorComponent"></param>
        /// <returns></returns>
        private int GetSlightlyColorComponent(int colorComponent)
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
        /// Tricks the transparent color
        /// </summary>
        /// <param name="returnColor"></param>
        /// <returns></returns>
        private Color TrickTransparentColor(Color returnColor)
        {
            if (this._registeredTransparentBackgroundFilter != null)
            {
                if (this._comparer.Equals(this._registeredTransparentBackgroundFilter.TransparentColor, returnColor))
                {
                    Color newColor =
                        Color.FromArgb(
                            this._registeredTransparentBackgroundFilter.TransparentColor.A,
                            this._registeredTransparentBackgroundFilter.TransparentColor.R,
                            this._registeredTransparentBackgroundFilter.TransparentColor.G,
                            this.GetSlightlyColorComponent(this._registeredTransparentBackgroundFilter.TransparentColor.B));

                    returnColor = newColor;
                }
            }

            return returnColor;
        }

        /// <summary>
        /// Gets the range length
        /// </summary>
        /// <param name="maxColor"></param>
        /// <returns></returns>
        private int GetRangeLength(int maxColor)
        {
            #region Entries validation

            if (maxColor < 1)
            {
                throw new ArgumentOutOfRangeException("maxColor", maxColor, "The maxColor must be greater then 0");
            }

            #endregion

            // This is just a bare estimate. Calculate using combination is too hard because of long numbers formed and it takes too much
            int returnValue = 256 / 1 / (maxColor / 12);

            if (returnValue <= 0)
            {
                returnValue = 1;
            }

            return returnValue;
        }

        /// <summary>
        /// Fit the color component in correspondent range color
        /// </summary>
        /// <param name="colorComponent">It´s the original color component</param>
        /// <param name="rangeLength">It´s the range length</param>
        /// <returns></returns>
        private int FitColorComponent(int colorComponent, int rangeLength)
        {
            #region Entries validation

            if (rangeLength < 1 || rangeLength > 256)
            {
                throw new ArgumentOutOfRangeException("rangeLength", rangeLength, "Invalid rangeLength");
            }

            #endregion

            // Put in scale
            int result;
            int fitColor;
            int remainder = Math.DivRem(colorComponent, rangeLength, out result);
            if (remainder > 0)
            {
                int increment = rangeLength - result;
                fitColor = colorComponent + increment;
            }
            else
            {
                fitColor = colorComponent;
            }

            if (fitColor < 0)
            {
                fitColor = 0;
            }
            else if (fitColor > 254)
            {
                fitColor = 254; // <-- Putting 255 causes an impredictable transparent color
            }

            if (fitColor + this._contrast > 0)
            {
                fitColor -= Math.Abs((int)this._contrast);
            }

            return fitColor;
        }

    }
}