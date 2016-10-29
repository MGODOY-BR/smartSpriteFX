
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
        /// It´s a list of different point ranges
        /// </summary>
        private List<PointRange> _rangeList = new List<PointRange>();

        /// <summary>
        /// It´s the current Point range.
        /// </summary>
        private PointRange _currentRange;

        /// <summary>
        /// Prepare the range
        /// </summary>
        public void Prepare()
        {
            this._currentRange = new PointRange();
            this._rangeList.Add(this._currentRange);
        }

        /// <summary>
        /// Set a point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPoint(int x, int y)
        {
            lock (this._currentRange)
            {
                if (this._currentRange._startPoint == null)
                {
                    this._currentRange._startPoint = new Point(x, y);
                }
                else
                {
                    this._currentRange._endPoint = new Point(x, y);

                    if (this._currentRange._startPoint.CompareTo(this._currentRange._endPoint) == 1)
                    {
                        Point tempPoint = this._currentRange._startPoint;
                        this._endPoint = this._currentRange._startPoint;
                        this._endPoint = tempPoint;
                    }
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

            foreach (var pointRange in this._rangeList)
            {
                if (pointRange.HaveThisContained(x, y))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifies if the current point range have the X and Y contained.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool HaveThisContained(int x, int y)
        {
            #region Entries validation

            if (this._startPoint == null)
            {
                return false;
            }
            if (this._endPoint == null)
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
            List<Point> returnList = new List<Point>();

            foreach (var rangeItem in this._rangeList)
            {
                returnList.AddRange(rangeItem.ConvertMeToPointList());
            }

            return returnList;
        }

        /// <summary>
        /// Converts the current point list to a set of points
        /// </summary>
        /// <returns></returns>
        private List<Point> ConvertMeToPointList()
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

        /// <summary>
        /// Sets the range
        /// </summary>
        /// <param name="initialX"></param>
        /// <param name="initialY"></param>
        /// <param name="finalX"></param>
        /// <param name="finalY"></param>
        public void SetPoint(int initialX, int initialY, int finalX, int finalY)
        {
            this._startPoint = new Point(initialX, initialY);
            this._endPoint = new Point(finalX, finalY);
        }
    }
}