
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Grids{
	/// <summary>
	/// Represents a grid covering the image
	/// </summary>
	public class Grid {

		/// <summary>
		/// It's the row count
		/// </summary>
		public int RowCount
        {
            get;
            set;
        }

		/// <summary>
		/// It's the column count
		/// </summary>
		public int ColumnCount
        {
            get;
            set;
        }

        /// <summary>
        /// It's the size of whole grid area, in pixels
        /// </summary>
        public Point Size
        {
            get;
            set;
        }

        /// <summary>
        /// It's the start position in picture
        /// </summary>
        public Point Start
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of grid parts 
        /// </summary>
        public List<GridPart> GridPartList
        {
            get;
            private set;
        }

        /// <summary>
        /// It's the precision of grid
        /// </summary>
        public float Precision { get; set; }

        public Grid()
        {
            this.GridPartList = new List<GridPart>();
            this.Start = new Point
            {
                X = 0,
                Y = 0,
            };
        }

        /// <summary>
        /// Builds the grid
        /// </summary>
        public void Build(PointInfo[] pointList)
        {
            #region Entries validation

            if (this.Size == null)
            {
                throw new ArgumentNullException("this.Size");
            }
            if (this.Start == null)
            {
                throw new ArgumentNullException("this.Start");
            }
            if (pointList == null)
            {
                throw new ArgumentNullException("pointList");
            }
            if(this.GridPartList.Count > 0)
            {
                throw new InvalidOperationException("Grid has already been built");
            }

            #endregion

            float gridPartHeight = this.Size.Y / this.RowCount;
            float gridPartWidth = this.Size.X / this.ColumnCount;
            int row = 0;

            for (float y = this.Start.Y; y < this.Start.Y + this.Size.Y; y += gridPartHeight)
            {
                int column = 0;
                for (float x = this.Start.X; x < this.Start.X + this.Size.X; x += gridPartWidth)
                {
                    GridPart gridPart = new GridPart(this)
                    {
                        Size = new Point
                        {
                            X = gridPartWidth,
                            Y = gridPartHeight
                        },
                        Column = column,
                        Row = row,
                        PointA = new Point
                        {
                            X = x,
                            Y = y
                        },
                        PointB = new Point
                        {
                            X = x + gridPartWidth,
                            Y = y + gridPartHeight
                        },
                    };
                    var pointItemIterator = from pointItem in pointList
                                            where
                                                pointItem.X >= gridPart.PointA.X &&
                                                pointItem.Y >= gridPart.PointA.Y &&
                                                pointItem.X <= gridPart.PointB.X &&
                                                pointItem.Y <= gridPart.PointB.Y
                                            select pointItem;

                    gridPart.Associate(pointItemIterator);

                    this.GridPartList.Add(gridPart);

                    column++;
                }
                row++;
            }

        }

	}
}