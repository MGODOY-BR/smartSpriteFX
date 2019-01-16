
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Settings{
	/// <summary>
	/// Represents the classification of polygon
	/// </summary>
	public class PolygonClassification {

		/// <summary>
		/// Represents the classification of polygon
		/// </summary>
		public PolygonClassification() {
		}

		/// <summary>
		/// It's the name of classification
		/// </summary>
		public String Name;

		/// <summary>
		/// It's the classification list
		/// </summary>
		private HashSet<PolygonClassification> _classificationList;

		/// <summary>
		/// 
		/// </summary>
		public PaintingSettings Settings;



		/// <summary>
		/// Creates a new classification with a random name
		/// </summary>
		/// <returns></returns>
		public static PolygonClassification Create() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Incorporates the classification
		/// </summary>
		/// <param name="classification">		public void Incorporate(PolygonClassification classification) {</param>
			// TODO implement here
		}

	}
}