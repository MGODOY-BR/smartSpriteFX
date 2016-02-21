
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures.PixelPatterns{
	/// <summary>
	/// Defines how is the criteria to search some referential pixel line
	/// </summary>
	public interface IReferentialCoordinateCriteria {

		/// <summary>
		/// Gets a boolean informing the validation of  the coordinate
		/// </summary>
		bool IsValid(Dictionary<String, Color> leanrtCache, int x, int y);

	}
}