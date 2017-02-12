
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSpriteFX.Pictures{
    /// <summary>
    /// Represents a cartesyan coordinate
    /// </summary>
    [Serializable]
    public class Point : IComparable<Point>
    {

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

        public int CompareTo(Point other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            #endregion

            if (this.Y < other.Y)
            {
                return -1;
            }
            else if (this.Y > other.Y)
            {
                return 1;
            }
            else
            {
                return this.X.CompareTo(other.X);
            }
        }

        /// <summary>
        /// Returns a clone of point
        /// </summary>
        /// <returns></returns>
        public Point Clone()
        {
            return new Point(this.X, this.Y);
        }

        public override string ToString()
        {
            return String.Format("X = {0}, Y = {1}", this.X, this.Y);
        }
    }
}