
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
		/// It's the name of classification
		/// </summary>
		public String Name
        {
            get;
            set;
        }

		/// <summary>
		/// It's the classification list
		/// </summary>
		private List<PolygonClassification> _classificationList;

        /// <summary>
        /// It's the settigns of classification
        /// </summary>
        public PaintingSettings Settings { get; set; }

        /// <summary>
        /// Creates a new classification with a random name
        /// </summary>
        /// <returns></returns>
        public static PolygonClassification Create() {
			// TODO implement here
			return null;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Incorporates the classification
        /// </summary>
        /// <param name="classification" />		
        public void Incorporate(PolygonClassification classification) {
            // TODO implement here
            throw new NotImplementedException();
		}

	}
}