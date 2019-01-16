
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Traces{
	/// <summary>
	/// Represents a line made of ordered points
	/// </summary>
	public class Line {

		/// <summary>
		/// Represents a line made of ordered points
		/// </summary>
		public Line() {
		}

		/// <summary>
		/// 
		/// </summary>
		public HashSet<Point> PointList;


		/// <summary>
		/// Sort the lines
		/// </summary>
		public void Sort() {
			// TODO implement here
		}

		/// <summary>
		/// Informs if the lines are in sequence
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
		public bool IsSequel(Line line) {
			// TODO implement here
			return False;
		}

	}
}