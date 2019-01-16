
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSpriteFX.Pictures{
	/// <summary>
	/// It´s a range of points, similar to a rectangle
	/// </summary>
	public class PointRange : IComparable<PointRange>, IEquatable<PointRange>
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
        /// It´s the color of range
        /// </summary>
        public Color Color { get; set; }

        public PointRange()
        {

        }

        public int CompareTo(PointRange other)
        {
            #region Entries validation

            if (other == null)
            {
                return 1;
            }
            if(this.ToString().Equals(other.ToString()))
            {
                return 0;
            }

            #endregion

            return this.StartPoint.CompareTo(other.StartPoint);
        }

        /// <summary>
        /// Creates an instance of the object
        /// </summary>
        public PointRange(Point startPoint, Point endPoint)
        {
            #region Entries validation

            if (startPoint == null)
            {
                throw new ArgumentNullException("startPoint");
            }
            if (endPoint == null)
            {
                throw new ArgumentNullException("endPoint");
            }

            #endregion

            this._startPoint = startPoint;
            this._endPoint = endPoint;
        }

        /// <summary>
        /// Gets or sets the start point
        /// </summary>
        public Point StartPoint
        {
            get
            {
                return _startPoint;
            }
            set
            {
                this._startPoint = value;
            }
        }

        /// <summary>
        /// Gets or sets the endpoint
        /// </summary>
        public Point EndPoint
        {
            get
            {
                return _endPoint;
            }
            set
            {
                this._endPoint = value;
            }
        }

        /// <summary>
        /// Gets the size of range
        /// </summary>
        public Point Size
        {
            get
            {
                return new Point(
                    this._endPoint.X - this._startPoint.X,
                    this._endPoint.Y - this._startPoint.Y);
            }
        }

        /// <summary>
        /// It´s the current Point range.
        /// </summary>
        [Obsolete("This element is obsolet and can't be used.")]
        private PointRange _currentRange;

        /// <summary>
        /// Set a point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [Obsolete("This method is obsolet and can't be used.", true)]
        public void SetPoint(float x, float y)
        {
            if(this._currentRange == null)
            {
                this._currentRange = new PointRange();
            }

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
        public bool IsContained(Point point)
        {
            return this.IsContained(point.X, point.Y);
        }

        /// <summary>
        /// Gets a indicator informing that the point is contained inside of point range.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsContained(float x, float y)
        {
            #region Entries validation

            if (this._startPoint == null || this._endPoint == null)
            {
                return false;
            }

            #endregion

            if (this.HaveThisContained(x, y))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verifies if the current point range have the X and Y contained.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool HaveThisContained(float x, float y)
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

            returnList.AddRange(this.ConvertMeToPointList());

            return returnList;
        }

        /// <summary>
        /// Converts the range in a point list
        /// </summary>
        /// <returns></returns>
        public List<PointInfo> ToPointInfoList()
        {
            List<PointInfo> returnList = new List<PointInfo>();

            returnList.AddRange(this.ConvertMeToPointInfoList());

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
        /// Converts the current point list to a set of points
        /// </summary>
        /// <returns></returns>
        private List<PointInfo> ConvertMeToPointInfoList()
        {
            #region Entries validation

            if (this._startPoint == null)
            {
                throw new ApplicationException("The point range hasn't been initialized yet. It´s needed called SetPoint at least at once.");
            }
            List<PointInfo> returnList = new List<PointInfo>();
            if (this._endPoint == null)
            {
                returnList.Add(
                    new PointInfo(this._startPoint, this.Color));
                return returnList;
            }

            #endregion

            for (int y = (int)this._startPoint.Y; y < this._endPoint.Y; y++)
            {
                for (int x = (int)this._startPoint.X; x < this._endPoint.X; x++)
                {
                    returnList.Add(
                        new PointInfo(x, y, this.Color));
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
        public void SetPoint(float initialX, float initialY, float finalX, float finalY)
        {
            this._startPoint = new Point(initialX, initialY);
            this._endPoint = new Point(finalX, finalY);
        }

        /// <summary>
        /// Updates the points
        /// </summary>
        /// <param name="initialX"></param>
        /// <param name="initialY"></param>
        /// <param name="finalX"></param>
        /// <param name="finalY"></param>
        public void UpdatePoint(float initialX, float initialY, float finalX, float finalY)
        {
            if (this._startPoint == null)
            {
                this._startPoint = new Point(initialX, initialY);
            }

            if (this._endPoint == null)
            {
                this._endPoint = new Point(finalX, finalY);
            }
            else
            {
                this._startPoint.X = initialX;
                this._startPoint.Y = initialY;
                this._endPoint.X = finalX;
                this._endPoint.Y = finalY;
            }
        }

        /// <summary>
        /// Updates the end points
        /// </summary>
        /// <param name="finalX"></param>
        /// <param name="finalY"></param>
        public void UpdatePoint(float finalX, float finalY)
        {
            if (this._startPoint == null)
            {
                this._startPoint = new Point(finalX, finalY);
            }

            if (this._endPoint == null)
            {
                this._endPoint = new Point(finalX, finalY);
            }
            else
            {
                this._endPoint.X = finalX;
                this._endPoint.Y = finalY;
            }
        }

        /// <summary>
        /// Updates the end points
        /// </summary>
        public void UpdatePoint(Point endPoint)
        {
            this.UpdatePoint(endPoint.X, endPoint.Y);
        }

        /// <summary>
        /// Updates the whole range
        /// </summary>
        public void UpdatePoint(Point startPoint, Point endPoint)
        {
            this.UpdatePoint(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        }

        /// <summary>
        /// Calculate the hypotenuse
        /// </summary>
        /// <returns></returns>
        public double CalculateHypotenuse()
        {
            var a = this.EndPoint.X - this.StartPoint.X;
            var b = this.EndPoint.Y - this.StartPoint.Y;
            return Math.Sqrt((a * a) + (b * b));
        }

        public override string ToString()
        {
            return this.StartPoint.ToString() + " / " + this.EndPoint.ToString();
        }

        public bool Equals(PointRange other)
        {
            return this.CompareTo(other) == 0;
        }
    }
}