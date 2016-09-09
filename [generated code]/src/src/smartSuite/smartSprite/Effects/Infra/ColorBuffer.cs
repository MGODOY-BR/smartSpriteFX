
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Infra{
	/// <summary>
	/// A buffer of colors, used to limt the quality of image
	/// </summary>
	public class ColorBuffer {

		/// <summary>
		/// A buffer of colors, used to limt the quality of image
		/// </summary>
		public ColorBuffer() {
		}

		/// <summary>
		/// It´s a cache of colors.
		/// </summary>
		private HashSet<Color> _colorCacheList;




		/// <summary>
		/// Creates an instance of object
		/// </summary>
		/// <param name="length">It´s the maximum amount of allowed colors.</param>
		public void ColorBuffer(int length) {
			// TODO implement here
		}

		/// <summary>
		/// Registers a color in buffer
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public void Register(Color color) {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Gets a color similar to assigned color
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public Color GetSimilarColor(Color color) {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Clears the color buffer
		/// </summary>
		public void Clear() {
			// TODO implement here
		}

	}
}