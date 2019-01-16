
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Traces{
	/// <summary>
	/// Represents a trace made by points
	/// </summary>
	public class TracePoints {

		/// <summary>
		/// Represents a trace made by points
		/// </summary>
		public TracePoints() {
		}

		/// <summary>
		/// It is the point list
		/// </summary>
		public HashSet<Point> PointList;

		/// <summary>
		/// 
		/// </summary>
		public GridPart OwnerGridPart;

		/// <summary>
		/// Organizes the points in lines
		/// </summary>
		/// <returns></returns>
		public HashSet<Line> ExtractLines() {
			// TODO implement here
			return null;
		}

	}
}