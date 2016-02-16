
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
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
		private Dictionary<String, Color> learntCache = new Dictionary<String, Color>();

		/// <summary>
		/// Includes the coordinates to study the pattern
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		/// <param name="color">It´s the color of pixel</param>
		public void Learn(int x, int y, Color color) {
			// TODO implement here
		}

		/// <summary>
		/// Gets the learnt pixel pattern for the coordinate
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		/// <param name="color">It´s the color of pixel</param>
		public void GetPattern(int x, int y, Color color) {
			// TODO implement here
		}

		/// <summary>
		/// Returns a key ready for be included on learntCache
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		/// <param name="color">It´s the color of pixel</param>
		private void formatKey(int x, int y, Color color) {
			// TODO implement here
		}

	}
}