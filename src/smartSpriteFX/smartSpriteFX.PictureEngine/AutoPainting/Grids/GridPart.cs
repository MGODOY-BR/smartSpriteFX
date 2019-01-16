
using smartSuite.smartSprite.AutoPainting.Traces;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Grids{
	/// <summary>
	/// Represents a piece of grid
	/// </summary>
	public class GridPart {

        /// <summary>
        /// It's the colorComparer, used in IsPenColor method
        /// </summary>
        private PenColorComparer _penColorComparer = new PenColorComparer();

        /// <summary>
        /// It's the column
        /// </summary>
        public int Column
        {
            get;
            set;
        }

		/// <summary>
		/// It's the row of grid part
		/// </summary>
		public int Row
        {
            get;
            set;
        }

        /// <summary>
        /// It's the size of grid part
        /// </summary>
        public Point Size
        {
            get;
            set;
        }

        /// <summary>
        /// It's the very beginning Point
        /// </summary>
        public Point PointA
        {
            get;
            set;
        }

        /// <summary>
        /// It's the very ending Point
        /// </summary>
        public Point PointB
        {
            get;
            set;
        }

        /// <summary>
        /// It is the point list
        /// </summary>
        public List<Point> PointList
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the line list
        /// </summary>
        public List<Line> LineList { get; set; }

        /// <summary>
        /// It's the parent grid
        /// </summary>
        public Grid ParentGrid { get; private set; }

        public GridPart(Grid parent)
        {
            #region Entries validation

            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            #endregion

            this.ParentGrid = parent;
            this.PointList = new List<Point>();
        }

        /// <summary>
        /// Gets an indicator informing if the point are contained in grid part
        /// </summary>
        /// <param name="pointHand"></param>
        /// <returns></returns>
        public bool HasContained(PointHand pointHand)
        {
            #region Entries validation

            if (pointHand == null)
            {
                throw new ArgumentNullException("pointHand");
            }

            #endregion

            var point = this.PointList.FirstOrDefault(p => pointHand.IsContained((int)p.X, (int)p.Y));
            return point != null;
        }

        /// <summary>
        /// Associates the point to grid part
        /// </summary>
        /// <param name="point"></param>
        public virtual void Associate(Point point, System.Drawing.Color color)
        {
            #region Entries validation

            if (point == null)
            {
                throw new ArgumentNullException("point");
            }
            if (!this._penColorComparer.IsPencilColor(color))
            {
                return;
            }

            #endregion

            lock (this)
            {
                this.PointList.Add(point);
            }
        }

        /// <summary>
        /// Associates the set of point and color to grid part
        /// </summary>
        /// <param name="pointItemIterator"></param>
        public void Associate(IEnumerable<PointInfo> pointItemIterator)
        {
            foreach (var pointItem in pointItemIterator)
            {
                this.Associate(pointItem, pointItem.Color);
            }
        }

        /// <summary>
        /// Gets an indicator informing if the point are contained inside of grid part
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool IsContained(Point point)
        {
            #region Entries validation

            if (this.PointA == null)
            {
                throw new ArgumentNullException("this.PointA");
            }
            if (this.PointB == null)
            {
                throw new ArgumentNullException("this.PointB");
            }
            if (point == null)
            {
                throw new ArgumentNullException("point");
            }

            #endregion

            return
                point.X >= this.PointA.X && point.Y >= this.PointA.Y &&
                point.X <= this.PointB.X && point.Y <= this.PointB.Y;
        }

		/// <summary>
		/// Checks if the grid part is adjacent to this own and returns a degree of relashionship
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public virtual DegreeOfRelashionshipEnum CheckAdjacent(GridPart other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            #endregion

            if(other.Row == this.Row)
            {
                if(other.Column - this.Column == 1)
                {
                    return DegreeOfRelashionshipEnum.RIGHT;
                }
                else if (other.Column - this.Column == -1)
                {
                    return DegreeOfRelashionshipEnum.LEFT;
                }
                else if (other.Column - this.Column == 0)
                {
                    return DegreeOfRelashionshipEnum.OVERPLACED;
                }
            }
            else if(other.Row - this.Row == 1 && other.Column == this.Column)
            {
                return DegreeOfRelashionshipEnum.DOWN;
            }
            else if (other.Row - this.Row == -1 && other.Column == this.Column)
            {
                return DegreeOfRelashionshipEnum.UP;
            }

            return DegreeOfRelashionshipEnum.NOT_RELATIVE;
        }

        /// <summary>
        /// Gets an indicator informing if there any point in edge of grid part
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public virtual bool IsThereAnyPointInEdge()
        {
            return false;
            // throw new NotImplementedException();

            //bool isEdge = false;
            //for (float y = this.PointA.Y; y < this.PointB.Y && !isEdge; y++)
            //{
            //    var edgePoint1 = new Point { Y = y, X = this.PointA.X };
            //    var edgePoint2 = new Point { Y = y, X = this.PointB.X };

            //    isEdge = 
            //        this.PointList.Exists(p => Line.CanBeSequel(p, edgePoint1) || Line.CanBeSequel(p, edgePoint1));
            //}

            //for (float x = this.PointA.X; x < this.PointB.X && !isEdge; x++)
            //{
            //    var edgePoint1 = new Point { Y = this.PointA.Y, X = x };
            //    var edgePoint2 = new Point { Y = this.PointB.Y, X = x };

            //    isEdge =
            //        this.PointList.Exists(p => Line.CanBeSequel(p, edgePoint1) || Line.CanBeSequel(p, edgePoint1));
            //}

            //return isEdge;
        }

        #region Relative gridParts

        /// <summary>
        /// Gets the relative grid part
        /// </summary>
        public GridPart GetUpperSideLeftGridPart()
        {
            return this.ParentGrid.GridPartList.FirstOrDefault(x => x.Row == this.Row - 1 && x.Column == this.Column - 1);
        }

        /// <summary>
        /// Gets the relative grid part
        /// </summary>
        public GridPart GetUpperSideRightGridPart()
        {
            return this.ParentGrid.GridPartList.FirstOrDefault(x => x.Row == this.Row - 1 && x.Column == this.Column + 1);
        }

        /// <summary>
        /// Gets the relative grid part
        /// </summary>
        public GridPart GetUpperSideGridPart()
        {
            return this.ParentGrid.GridPartList.FirstOrDefault(x => x.Row == this.Row - 1 && x.Column == this.Column);
        }

        /// <summary>
        /// Gets the relative grid part
        /// </summary>
        public GridPart GetDownSideLeftGridPart()
        {
            return this.ParentGrid.GridPartList.FirstOrDefault(x => x.Row == this.Row + 1 && x.Column == this.Column - 1);
        }

        /// <summary>
        /// Gets the relative grid part
        /// </summary>
        public GridPart GetDownSideRightGridPart()
        {
            return this.ParentGrid.GridPartList.FirstOrDefault(x => x.Row == this.Row + 1 && x.Column == this.Column + 1);
        }

        /// <summary>
        /// Gets the relative grid part
        /// </summary>
        public GridPart GetDownSideGridPart()
        {
            return this.ParentGrid.GridPartList.FirstOrDefault(x => x.Row == this.Row + 1 && x.Column == this.Column);
        }

        /// <summary>
        /// Gets the relative grid part
        /// </summary>
        public GridPart GetLeftSideGridPart()
        {
            return this.ParentGrid.GridPartList.FirstOrDefault(x => x.Column == this.Column - 1 && x.Row == this.Row);
        }

        /// <summary>
        /// Gets the relative grid part
        /// </summary>
        public GridPart GetRightSideGridPart()
        {
            return this.ParentGrid.GridPartList.FirstOrDefault(x => x.Column == this.Column + 1 && x.Row == this.Row);
        }

        /// <summary>
        /// Gets all the relative part
        /// </summary>
        /// <returns></returns>
        public List<GridPart> GetRelativeParts()
        {
            List<GridPart> relativeList = new List<GridPart>
            {
                this.GetDownSideGridPart(),
                this.GetDownSideLeftGridPart(),
                this.GetUpperSideRightGridPart(),
                this.GetDownSideRightGridPart(),
                this.GetLeftSideGridPart(),
                this.GetRightSideGridPart(),
                this.GetUpperSideGridPart(),
                this.GetUpperSideLeftGridPart(),
            };

            var returnValue = from item in relativeList
                              where item != null
                              select item;

            return returnValue.ToList();
        }

        #endregion
    }
}