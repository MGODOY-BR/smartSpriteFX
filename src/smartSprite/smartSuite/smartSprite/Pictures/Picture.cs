
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents an image
	/// </summary>
	public class Picture {

		/// <summary>
		/// Represents an image
		/// </summary>
		public Picture(String fullPath)
        {
            this._fullPath = fullPath;
		}

		/// <summary>
		/// It´s the fullname of picture
		/// </summary>
		private String _fullPath;

		/// <summary>
		/// It's the last clicked point
		/// </summary>
		public Point LastPoint
        {
            get;
            set;
        }

	}
}