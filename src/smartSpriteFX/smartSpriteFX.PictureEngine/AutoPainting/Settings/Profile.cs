
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Settings{
	/// <summary>
	/// It's the profile
	/// </summary>
	public class Profile {

		/// <summary>
		/// It's the trace color
		/// </summary>
		public ColorInfo TraceColor;

		/// <summary>
		/// It's a list of polygon classification
		/// </summary>
		public List<PolygonClassification> ClassificationList
        {
            get;
            private set;
        }

		/// <summary>
		/// Saves the profile
		/// </summary>
		/// <param name="fileName" />		
        public void Save(string fileName)
        {
            // TODO implement here
            throw new NotImplementedException();
		}

		/// <summary>
		/// Applies the profile
		/// </summary>
		/// <returns></returns>
		public static Profile Load() {
            // TODO implement here
            throw new NotImplementedException();
        }

    }
}