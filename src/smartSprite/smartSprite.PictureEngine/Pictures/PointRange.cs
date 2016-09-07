
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// It´s a range of points, similar to a rectangle
	/// </summary>
	public class PointRange
    {
        /// <summary>
        /// It´s a start point of point range
        /// </summary>
        private Point _startPoint;

        /// <summary>
        /// It´s a end point of point range
        /// </summary>
        private Point _endPoint;

        /// <summary>
        /// Set a point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPoint(int x, int y)
        {
            if (this._startPoint == null)
            {
                this._startPoint = new Point(x, y);
            }
            else
            {
                this._endPoint = new Point(x, y);

                if (this._startPoint.CompareTo(this._endPoint) == 1)
                {
                    Point tempPoint = this._startPoint;
                    this._endPoint = this._startPoint;
                    this._endPoint = tempPoint;
                }
            }
        }

        /// <summary>
        /// Gets a indicator informing that the point is contained inside of point range.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsContained(int x, int y)
        {
            #region Entries validation

            if (this._startPoint == null || this._endPoint == null)
            {
                return false;
            }

            #endregion

            return x >= this._startPoint.X && x <= this._endPoint.X &&
                    y >= this._startPoint.Y && y <= this._endPoint.Y;
        }

        /// <summary>
        /// Converts the range in a point list
        /// </summary>
        /// <returns></returns>
        public List<Point> ToPointList()
        {
            #region Entries validation

            if (this._startPoint == null)
            {
                throw new ApplicationException("The point range hasn't been initialized yet. It´s needed called SetPoint at least at once.");
            }
            List<Point> returnList = new List<Point>();
            if (this._endPoint == null)
            {
                returnList.Add(this._startPoint);
                return returnList;
            }

            #endregion

            for (int y = (int)this._startPoint.Y; y < this._endPoint.Y; y++)
            {
                for (int x = (int)this._startPoint.X; x < this._endPoint.X; x++)
                {
                    returnList.Add(new Point(x, y));
                }
            }

            return returnList;
        }
    }
}