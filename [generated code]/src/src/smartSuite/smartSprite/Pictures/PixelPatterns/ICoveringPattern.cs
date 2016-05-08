
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures.PixelPatterns{
	/// <summary>
	/// Defines how a pixel covering pattern must be
	/// </summary>
	public interface ICoveringPattern {

		/// <summary>
		/// Gets a pixel correspondent to coordinates
		/// </summary>
		/// <param name="pixelInfoList"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Color GetPixel(HashSet<PixelInfo> pixelInfoList, int x, int y);

	}
}