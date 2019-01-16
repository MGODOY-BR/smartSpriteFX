
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting{
	/// <summary>
	/// Represents a polygon
	/// </summary>
	public class Polygon {

		/// <summary>
		/// Represents a polygon
		/// </summary>
		public Polygon() {
		}

		/// <summary>
		/// Gets the status of polygon
		/// </summary>
		public PolygonStatusEnum Status;

		/// <summary>
		/// 
		/// </summary>
		private HashSet<Point> _pointList;

		/// <summary>
		/// 
		/// </summary>
		private ColorInfo _paintingColor;

		/// <summary>
		/// It's the grid part which polygon are inside on
		/// </summary>
		public GridPart _ownerGridPart;

		/// <summary>
		/// 
		/// </summary>
		public PolygonClassification Classification;

		/// <summary>
		/// 
		/// </summary>
		public HashSet<Line> LineList;

		/// <summary>
		/// Paint the polygon color in image
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public void Paint(Image image) {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Tries complete the polygon.
		/// </summary>
		/// <returns></returns>
		public void TryComplete() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Distributes the point list into polygon list
		/// </summary>
		/// <param name="grid">It's the grid</param>
		/// <param name="pointList"></param>
		/// <returns></returns>
		public static HashSet<Polygon> Distribute(Grid grid, HashSet<Point> pointList) {
			// TODO implement here
			return null;
		}

	}
}