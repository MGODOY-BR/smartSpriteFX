
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
		private List<PixelInfo> _learntCache = new List<PixelInfo>();

        /// <summary>
        /// It´s the covering pattern
        /// </summary>
        private ICoveringPattern _coveringPattern = new ConstantCoveringPattern();

		/// <summary>
		/// Includes the coordinates to study the pattern
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		/// <param name="color">It´s the color of pixel</param>
		public void Learn(int x, int y, Color color) {

            this._learntCache.Add(
                new PixelInfo
                {
                    X = x,
                    Y = y,
                    Color = color
                });
		}

		/// <summary>
		/// Gets the learnt pixel pattern for the coordinate
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		public Color GetPattern(int x, int y)
        {
            return this._coveringPattern.GetPixel(this._learntCache, x, y);
        }


	}
}