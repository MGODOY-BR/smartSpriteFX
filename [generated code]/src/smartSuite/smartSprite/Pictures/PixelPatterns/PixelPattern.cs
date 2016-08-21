
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		/// 
		/// </summary>
		private HashSet<PixelInfo> _learntCache;

		/// <summary>
		/// 
		/// </summary>
		private ICoveringPattern _coveringPattern;

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
		/// <returns></returns>
		public Color GetPattern(int x, int y) {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Returns a key ready for be included on learntCache
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		/// <returns></returns>
		private string formatKey(int x, int y) {
			// TODO implement here
			return "";
		}

	}
}