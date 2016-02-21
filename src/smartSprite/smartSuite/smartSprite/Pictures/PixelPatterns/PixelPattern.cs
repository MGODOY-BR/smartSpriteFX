
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace smartSuite.smartSprite.Pictures.PixelPatterns{
	/// <summary>
	/// Represents a pattern analyser to study the pixel patterns of a image.
	/// </summary>
	public class PixelPattern {

		/// <summary>
		/// Represents a pattern analyser to study the pixel patterns of a image.
		/// </summary>
		public PixelPattern() {
		}

		/// <summary>
		/// It´s a cache dictionary where key is formed by coordinate x and y and the value is the color.
		/// </summary>
		private Dictionary<String, Color> _learntCache = new Dictionary<String, Color>();

		/// <summary>
		/// Includes the coordinates to study the pattern
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		/// <param name="color">It´s the color of pixel</param>
		public void Learn(int x, int y, Color color) {

            string key = this.formatKey(x, y);
            this._learntCache.Add(key, color);
		}

		/// <summary>
		/// Gets the learnt pixel pattern for the coordinate
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		public Color GetPattern(int x, int y)
        {
            string key = this.getLastKeyForX(x);
            return this._learntCache[key];
        }

        /// <summary>
        /// Gets the last key for x learnt
        /// </summary>
        private string getLastKeyForX(int x)
        {
            Regex regEx = new Regex(@"\d+_(\d+)", RegexOptions.Compiled);

            IReferentialCoordinateCriteria criteria = new DefaultReferentialCoordinateCriteria();

            var keyListForX = from item in this._learntCache
                            where 
                                item.Key.StartsWith(x.ToString() + "_") &&
                                criteria.IsValid(
                                    this._learntCache, 
                                    x, 
                                    int.Parse(regEx.Match(item.Key).Groups[1].Value))
                            orderby int.Parse(regEx.Match(item.Key).Groups[1].Value)
                            select item.Key;

            return keyListForX.LastOrDefault();
        }

        /// <summary>
        /// Returns a key ready for be included on learntCache
        /// </summary>
        /// <param name="x">It´s the x coordinate</param>
        /// <param name="y">It´s the y coordinate</param>
        private string formatKey(int x, int y)
        {
            return string.Format("{0}_{1}", x, y);
		}

	}
}