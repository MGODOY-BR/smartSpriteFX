
using smartSprite.Pictures.ColorPattern;
using smartSuite.smartSprite.Effects.Core;
using smartSuite.smartSprite.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Infra{
	/// <summary>
	/// A buffer of colors, used to limt the quality of image
	/// </summary>
	public class ColorBuffer {

        /// <summary>
        /// It´s the maximum of allowed colors.
        /// </summary>
        private int _maxLength;

        /// <summary>
        /// It´s a comparer for colors
        /// </summary>
        private ColorEqualityComparer _comparer;

        /// <summary>
        /// It´s a cache of colors.
        /// </summary>
        private List<Color> _colorCacheList = new List<Color>();

        /// <summary>
        /// It´s an eventual TransparentBackgroundFilter registered on the EffectEngine.
        /// </summary>
        private TransparentBackgroundFilter _registeredTransparentBackgroundFilter;

        /// <summary>
        /// It´s a sensibility of colors.
        /// </summary>
        private float _sensibility;

		/// <summary>
		/// Creates an instance of object
		/// </summary>
		/// <param name="length">It´s the maximum amount of allowed colors.</param>
        /// <param name="sensibility">It´s a percentage of tolerance to consider colors as equals</param>
		public ColorBuffer(int length, float sensibility)
        {
            #region Entries validation

            if (length < 4 && length != 0)
            {
                throw new ArgumentOutOfRangeException("Invalid color length. Inform minimun 4, or 0 to unlimited colors.");
            }

            #endregion

            this._maxLength = length;
            this._sensibility = sensibility;

            if (this._maxLength == 0)
            {
                this._comparer = new ColorEqualityComparer(0);
            }
            else
            {
                this._comparer = new ColorEqualityComparer(this._sensibility);
            }

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
        /// Registers a color in buffer
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public void Register(Color color)
        {
            bool found = false;
            foreach (var colorItem in this._colorCacheList) 
            {
                if (this._comparer.LooksLikeBySensibility(color, colorItem))
                {
                    found = true;
                    break;
                }
            }

            if (!found && (this._colorCacheList.Count < this._maxLength) || this._maxLength == 0)
            {
                if (!this._colorCacheList.Contains(color))
                {
                    #region Entries validation

                    if (this._registeredTransparentBackgroundFilter != null)
                    {
                        if (this._registeredTransparentBackgroundFilter.TransparentColor.ToArgb() == color.ToArgb())
                        {
                            return;
                        }
                    }

                    #endregion

                    this._colorCacheList.Add(color);
                }
            }
        }

		/// <summary>
		/// Gets a color similar to assigned color
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public Color GetSimilarColor(Color color)
        {
            foreach (var colorItem in this._colorCacheList)
            {
                if (this._comparer.LooksLikeBySensibility(color, colorItem))
                {
                    return colorItem;
                }
            }

            this._sensibility += 0.001f;
            this._comparer = new ColorEqualityComparer(this._sensibility);

            Color returnColor = this.GetSimilarColor(color);

            // Skipping transparent color
            returnColor = this.TrickTransparentColor(returnColor);

            return returnColor;
        }

        /// <summary>
        /// Clears the color buffer
        /// </summary>
        public void Clear()
        {
            this._colorCacheList.Clear();
		}

        /// <summary>
        /// Counts the amount of buffer
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this._colorCacheList.Count;
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
    }
}