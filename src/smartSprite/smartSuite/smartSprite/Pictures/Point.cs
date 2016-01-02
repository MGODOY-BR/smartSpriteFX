
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents a cartesyan coordinate
	/// </summary>
	public class Point {

        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// It's a coordinate X
        /// </summary>
        public float X
        {
            get;
            set;
        }

		/// <summary>
		/// It´s a coordinate Y
		/// </summary>
		public float Y
        {
            get;
            set;
        }
	}
}