
using smartSprite.Pictures.ColorPattern;
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

            if (this._maxLength == 0)
            {
                this._comparer = new ColorEqualityComparer(0);
            }
            else
            {
                this._comparer = new ColorEqualityComparer((int)sensibility);
            }
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
                if (this._comparer.LooksLike2(color, colorItem))
                {
                    found = true;
                    break;
                }
            }

            if (!found && (this._colorCacheList.Count < this._maxLength) || this._maxLength == 0)
            {
                if (!this._colorCacheList.Contains(color))
                {
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
                if (this._comparer.LooksLike2(color, colorItem))
                {
                    return colorItem;
                }
            }

            throw new InvalidOperationException("Incompatible color - " + color.ToArgb().ToString());
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

	}
}