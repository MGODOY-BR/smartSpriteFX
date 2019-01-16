
using smartSuite.smartSprite.AutoPainting.Grids;
using smartSuite.smartSprite.AutoPainting.Settings;
using smartSuite.smartSprite.AutoPainting.Traces;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using static smartSuite.smartSpriteFX.Pictures.Point;

namespace smartSuite.smartSprite.AutoPainting{
	/// <summary>
	/// Represents a polygon
	/// </summary>
	public class Polygon {

		/// <summary>
		/// Gets the status of polygon
		/// </summary>
		public PolygonStatusEnum Status
        {
            get;
            private set;
        }

		/// <summary>
		/// It's the color of paiting
		/// </summary>
		private ColorInfo _paintingColor;

		/// <summary>
		/// It's the classification of polygon
		/// </summary>
		public PolygonClassification Classification;

        /// <summary>
        /// Gets the line list
        /// </summary>
        public List<Line> LineList { get; private set; }

        /// <summary>
        /// Contains a list of corners
        /// </summary>
        public List<Corner> CornerList { get; private set; }

        /// <summary>
        /// It's the grid generated
        /// </summary>
        public Grid Grid { get; private set; }

        public Polygon()
        {
            this.LineList = new List<Line>();
            this.CornerList = new List<Corner>();
        }

        /// <summary>
        /// Paint the polygon color in image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public void Paint(Image image) {
            // TODO implement here
            throw new NotImplementedException();
		}

        /// <summary>
        /// Draws the debug
        /// </summary>
        public void DrawDebug(string fileName)
        {
            Random random = new Random();
            this.LineList.ForEach(delegate (Line line)
            {
                System.Drawing.Color color = System.Drawing.Color.FromArgb(
                    255,                        // A
                    random.Next(0, 255),        // R 
                    150,                        // G
                    random.Next(20, 200));      // B

                line.PointCache.ForEach(p => p.Color = color);
            });

            Picture debugPicture = new Picture();
            debugPicture.Width = (int)this.Grid.Size.X;
            debugPicture.Height = (int)this.Grid.Size.Y;

            this.LineList.ForEach(l => l.PointCache.ForEach(p => debugPicture.SetPixel(p.ToPointInfoList())));

            foreach (var gridPart in this.Grid.GridPartList)
            {
                for (var y = gridPart.PointA.Y; y < gridPart.PointB.Y; y += gridPart.Size.Y)
                {
                    for (var x = gridPart.PointA.X; x < gridPart.PointB.X; x++)
                    {
                        debugPicture.SetPixel((int)x, (int)y, System.Drawing.Color.Silver);
                    }
                }
                for (var y = gridPart.PointA.Y; y < gridPart.PointB.Y; y++)
                {
                    for (var x = gridPart.PointA.X; x < gridPart.PointB.X; x += gridPart.Size.X)
                    {
                        debugPicture.SetPixel((int)x, (int)y, System.Drawing.Color.Silver);
                    }
                }
            }

            #region Printing corner

            int i = 0;
            this.CornerList.ForEach(new Action<Corner>(delegate (Corner corner)
            {
                //corner.DrawDebug(i.ToString(), debugPicture);
                i++;
            }));

            #endregion

            debugPicture.SaveCopy(fileName);
        }

        /// <summary>
        /// Gets the point list
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public List<smartSpriteFX.Pictures.Point> GetPointList()
        {
            throw new NotImplementedException();
            //var returnValue = new List<smartSpriteFX.Pictures.Point>();
            //foreach (var lineItem in this.LineList)
            //{
            //    returnValue.AddRange(lineItem.PointList.Values);
            //}
            //return returnValue;
        }

		/// <summary>
		/// Tries complete the polygon.
		/// </summary>
		/// <returns></returns>
		public void TryComplete() {
            // TODO implement here
            throw new NotImplementedException();
        }

        /// <summary>
        /// Distributes the point list into polygon list
        /// </summary>
        /// <returns></returns>
        public static List<Polygon> Distribute(Grid grid, float pointRadio, PointInfo[] pointList)
        {
            #region Entries validation

            if (pointList == null)
            {
                throw new ArgumentNullException("pointList");
            }
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }

            #endregion

            grid.Build(pointList);

            #region Extracting lines

            var lineList = new List<Line>();
            foreach (var gridPart in grid.GridPartList)
            {
                #region Entries validation

                // Ignoring scretchs (considering lines inside of gridPart)
                //if (!gridPart.IsThereAnyPointInEdge())
                //{
                //    continue;
                //}

                #endregion

                var lineBuilder = new LineBuilder(gridPart, pointRadio);

                lineList.AddRange(
                    lineBuilder.ExtractLines());

                // gridPart.LineList = lineList;
            }

            #endregion

            #region Tracking sequence lines for building polygon

            List<Polygon> returnValue = new List<Polygon>();
            
            Line line = null;
            do
            {
                line = lineList.FirstOrDefault(l => !l.WasAnalysed && l.OwnerPolygonList.Count == 0);

                if(line == null)
                {
                    break;
                }

                Polygon polygon = new Polygon
                {
                    Grid = grid,
                };
                returnValue.Add(polygon);

                var polygonIncreaseDelegate = new Action<Line>(delegate(Line other){
                    polygon.LineList.Add(other);
                    line.OwnerPolygonList.Add(polygon);
                    line.WasAnalysed = true;
                });

                Line nextLine = null;
                while (line != null)
                {
                    if(nextLine == null) polygonIncreaseDelegate.Invoke(line);

                    // line = Polygon.NextLine(line);
                    nextLine = Polygon.NextLine(line, lineList);
                    if (nextLine != null)
                    {
                        // New line
                        if (!line.Assimilate(nextLine))
                        {
                            line = nextLine;
                            polygonIncreaseDelegate.Invoke(line);
                        }
                        else
                        {
                            nextLine.WasAnalysed = true;
                        }
                    }
                    else
                    {
                        // Lines are over
                        break;
                    }
                }

                polygon.Status =
                    polygon.CalculateStatus();
            }
            while (line != null) ;

            #endregion

            #region Detecting corners

            foreach (var polygon in returnValue)
            {
                polygon.CornerList.Clear();
                polygon.LineList.ForEach(delegate(Line l) {
                    l.Sort();
                    // Console.WriteLine(l + " " + l.CornerList.Count + " corners");
                    polygon.CornerList.AddRange(l.CornerList);
                 });
            }

            #endregion

            return returnValue;
        }

        /// <summary>
        /// Calculates the state classification of polygon
        /// </summary>
        /// <returns></returns>
        public PolygonStatusEnum CalculateStatus()
        {
            #region Entries validation

            #endregion

            var lastLine = this.LineList.Last();
            var startLine = this.LineList.First();

            // if (lastLine.IsSequel(startLine) && startLine != lastLine)
            if(startLine.IsSequel(lastLine) && startLine != lastLine)
            {
                return PolygonStatusEnum.COMPLETE;
            }
            else
            {
                return PolygonStatusEnum.INCOMPLETE;
            }
        }

        /// <summary>
        /// Gets the next line
        /// </summary>
        private static Line NextLine(Line line, List<Line> lineList)
        {
            #region Entries validation

            if (line == null)
            {
                throw new ArgumentNullException("line");
            }
            if (line.OnwerGridPart == null)
            {
                throw new ArgumentNullException("line.OnwerGridPart");
            }

            #endregion

            try
            {
                var sequelLine =
                    lineList.Find(l => line.IsSequel(l) && !l.WasAnalysed);

                if (sequelLine != null)
                {
                    return sequelLine;
                }

                return null;
            }
            finally
            {
                line.WasAnalysed = true;
            }
        }
    }
}