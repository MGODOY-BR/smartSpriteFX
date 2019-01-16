
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Grids{
	/// <summary>
	/// Represents a grid covering the image
	/// </summary>
	public class Grid {

		/// <summary>
		/// Represents a grid covering the image
		/// </summary>
		public Grid() {
		}

		/// <summary>
		/// It's the row count
		/// </summary>
		public int RowCount;

		/// <summary>
		/// It's the column count
		/// </summary>
		public int ColumnCount;

		/// <summary>
		/// It's the size of whole grid area, in pixels
		/// </summary>
		public Point Size;

		/// <summary>
		/// 
		/// </summary>
		public HashSet<GridPart> GridPartList;

		/// <summary>
		/// Builds the grid
		/// </summary>
		public void Build() {
			// TODO implement here
		}

	}
}