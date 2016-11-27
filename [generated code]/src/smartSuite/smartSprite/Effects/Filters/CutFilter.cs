
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// Represents a filter which is used to cut frames
	/// </summary>
	public class CutFilter : SmartSpriteOriginalFilterBase {

		/// <summary>
		/// Represents a filter which is used to cut frames
		/// </summary>
		public CutFilter() {
		}

		/// <summary>
		/// It´s a point A
		/// </summary>
		private Point _pointA;

		/// <summary>
		/// It´s the point B
		/// </summary>
		private Point _pointB;

	}
}