
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures.ColorPattern{
	/// <summary>
	/// It´s an object which studies the pixels to detect background pattern
	/// </summary>
	public class BackgroundPattern {

		/// <summary>
		/// It´s an object which studies the pixels to detect background pattern
		/// </summary>
		public BackgroundPattern() {
		}

		/// <summary>
		/// It´s the cache of pixel information
		/// </summary>
		private HashSet<PixelInfo> _learntCache;

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
		/// Returns a key ready for be included on learntCache
		/// </summary>
		/// <param name="x">It´s the x coordinate</param>
		/// <param name="y">It´s the y coordinate</param>
		/// <returns></returns>
		private string formatKey(int x, int y) {
			// TODO implement here
			return "";
		}

		/// <summary>
		/// Does the transparent border
		/// </summary>
		/// <param name="piece">It´s a piece to deal.</param>
		public void DoTransparentBorder(Piece piece) {
			// TODO implement here
		}

	}
}