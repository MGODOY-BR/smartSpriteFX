
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents an image
	/// </summary>
	public class Picture {

		/// <summary>
		/// Represents an image
		/// </summary>
		public Picture() {
		}

		/// <summary>
		/// It´s the fullname of picture
		/// </summary>
		private String _fullPath;

		/// <summary>
		/// It's the last clicked point
		/// </summary>
		public Point LastPoint;

		/// <summary>
		/// Gets a pixel from coordinate
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Color GetPixel(int x, int y) {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Replaces a pixel in the buffer
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="newColor">		public void ReplacePixel(int x, int y, Color newColor) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Overwrites the picture
		/// </summary>
		/// <param name="transparentColor">		public void Overwrite(Color transparentColor) {</param>
			// TODO implement here
		}

	}
}