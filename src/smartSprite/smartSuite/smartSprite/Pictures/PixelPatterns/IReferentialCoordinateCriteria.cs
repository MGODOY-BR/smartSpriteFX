
using System;
using System.Collections.Generic;
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
		/// <param name="leanrtCache"></param>
		/// <param name="int">x </param>
		/// <param name="int">y </param>
		/// <returns></returns>
		public boolean IsValid(Dictionary leanrtCache, void int x, void int y);

	}
}