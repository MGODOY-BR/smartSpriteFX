
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Grids{
	/// <summary>
	/// Represents a piece of grid
	/// </summary>
	public class GridPart {

		/// <summary>
		/// Represents a piece of grid
		/// </summary>
		public GridPart() {
		}

		/// <summary>
		/// It's the column
		/// </summary>
		public int Column;

		/// <summary>
		/// It's the row of grid part
		/// </summary>
		public int Row;

		/// <summary>
		/// It's the size of grid part
		/// </summary>
		public Point Size;

		/// <summary>
		/// It's the very beginning Point
		/// </summary>
		public Point PointA;

		/// <summary>
		/// It's the very ending Point
		/// </summary>
		public Point PointB;


		/// <summary>
		/// Gets an indicator informing if the point are contained inside of grid part
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public boolean IsContained(Point point) {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Checks if the grid part is adjacent to this own and returns a degree of relashionship
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public DegreeOfRelashionshipEnum CheckAdjacent(GridPart other) {
			// TODO implement here
			return null;
		}

	}
}