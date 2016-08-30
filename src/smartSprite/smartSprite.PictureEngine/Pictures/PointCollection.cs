
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// It´s a collection of points
	/// </summary>
	public class PointCollection
    {
        /// <summary>
        /// It´s a collection of points to be handle
        /// </summary>
        private List<Point> _innerPointList = new List<Point>();

        /// <summary>
        /// Gets the inner point list
        /// </summary>
        public List<Point> InnerPointList
        {
            get
            {
                return _innerPointList;
            }
        }

        /// <summary>
        /// Set a point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPoint(int x, int y)
        {
            var point = this._innerPointList.Find(item => item.X == x && item.Y == y);

            if (point == null)
            {
                this._innerPointList.Add(new Point(x, y));
            }
        }
	}
}