
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern;

namespace smartSuite.smartSpriteFX.Effects.Infra
{
    /// <summary>
    /// Controls a amount of colors, used to limt the quality of image
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
        /// It´s a "tricky" comparer for colors
        /// </summary>
        private ColorEqualityComparer _trickyComparer = new ColorEqualityComparer(35f);

        /// <summary>
        /// It´s a sensibility of colors.
        /// </summary>
        private int _contrast;

        /// <summary>
        /// It´s a length of range of colors
        /// </summary>
        private int _rangeLength;

        /// <summary>
        /// Gets the color slighty different to trick the transparent mechanism 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetSlightlyDifferentColor(Color color)
        {
            return
                ColorUtil.GetSlightlyDifferentColor(color);
        }

        /// <summary>
        /// It´s a list of avoided colors
        /// </summary>
        public List<Color> AvoidedColorList
        {
            get;
            set;
        }

        /// <summary>
        /// Creates an instance of object
        /// </summary>
        /// <param name="length">It´s the maximum amount of allowed colors.</param>
        /// <param name="contrast">It´s how different the colors are each others</param>
        public ColorBuffer(int length, int contrast)
        {
            #region Entries validation

            if (length < 1 && length != 0)
            {
                throw new ArgumentOutOfRangeException("Invalid color length.");
            }

            #endregion

            this._maxLength = length;
            this._contrast = contrast;
            this._rangeLength = this.GetRangeLength(this._maxLength);
            this._comparer = new ColorEqualityComparer(0);

            #region Getting the transparent background filter

            foreach (var filterItem in EffectEngine.GetSelectedFilterList())
            {
                TransparentBackgroundFilter registeredTransparentBackgroundFilter = filterItem as TransparentBackgroundFilter;
                if (registeredTransparentBackgroundFilter != null)
                {
                    if (this.AvoidedColorList == null)
                    {
                        this.AvoidedColorList = new List<Color>();
                    }
                    this.AvoidedColorList.Add(registeredTransparentBackgroundFilter.TransparentColor);
                    break;
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
            returnColor = this.TrickAvoidedColor(returnColor);

            return returnColor;
        }

        /// <summary>
        /// Gets the color component slighty different to trick the transparent mechanism
        /// </summary>
        /// <param name="colorComponent"></param>
        /// <returns></returns>
        public static int GetSlightlyDifferentColorComponent(int colorComponent)
        {
            return ColorUtil.GetSlightlyDifferentColorComponent(colorComponent);
        }

        /// <summary>
        /// Gets the color component opposite different to trick the transparent mechanism
        /// </summary>
        /// <param name="colorComponent"></param>
        /// <returns></returns>
        private int GetOppositeColorComponent(int colorComponent)
        {
            int factor = 50;

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
        /// Tricks the avoided color
        /// </summary>
        /// <param name="returnColor"></param>
        /// <returns></returns>
        private Color TrickAvoidedColor(Color returnColor)
        {
            #region Entries validation

            if (this.AvoidedColorList == null)
            {
                return returnColor;
            }

            #endregion

            foreach (var avoidedColor in this.AvoidedColorList)
            {
                if (this._trickyComparer.LooksLikeBySensibility3(avoidedColor, returnColor))
                {
                    Color newColor =
                        Color.FromArgb(
                            returnColor.A,
                            this.GetOppositeColorComponent(returnColor.R),
                            this.GetOppositeColorComponent(returnColor.G),
                            this.GetOppositeColorComponent(returnColor.B));

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
            int returnValue = 0;

            float partColor = 0;

            if (maxColor > 3)
            {
                partColor = maxColor / 3;
            }
            else
            {
                return maxColor;
            }

            float componentColor = 256 / partColor;

            returnValue = (int)componentColor * 5;

            if (returnValue == 0)
            {
                returnValue = 1;
            }
            if (returnValue > 250)
            {
                returnValue = 128;
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
            else if (fitColor > 255)
            {
                fitColor = 255;
            }

            if (fitColor + this._contrast > 0)
            {
                fitColor -= Math.Abs((int)this._contrast);
                if (fitColor < 0)
                {
                    fitColor = 0;
                }
            }

            return fitColor;
        }

    }
}