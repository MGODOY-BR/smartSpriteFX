
using smartSuite.smartSprite.AutoPainting.Grids;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.AutoPainting.Traces{
    /// <summary>
    /// Offers capabilities to extract lines from owner grid.
    /// </summary>
    public class LineBuilder {

        /// <summary>
        /// It's the size of the point
        /// </summary>
        private float _pointRadio;

		/// <summary>
		/// Gets or sets the owner grid part
		/// </summary>
		public GridPart OwnerGridPart
        {
            get;
            set;
        }

        public LineBuilder(GridPart gridPart, float pointRadio)
        {
            #region Entries validation

            if (gridPart == null)
            {
                throw new ArgumentNullException("gridPart");
            }

            #endregion

            this.OwnerGridPart = gridPart;
            this._pointRadio = pointRadio;
        }

		/// <summary>
		/// Organizes the points in lines
		/// </summary>
		/// <returns></returns>
		public List<Line> ExtractLines()
        {
            // Calculating catheti size
            float cathetiSize =
                (int)Math.Sqrt(this._pointRadio * this._pointRadio) / 2;

            List<Line> lineList = new List<Line>();
            Line currentLine = null;

            var offSetX = (int)this.OwnerGridPart.PointA.X;
            var offSetY = (int)this.OwnerGridPart.PointA.Y;

            for (float y = offSetY; y < (int)this.OwnerGridPart.Size.Y + offSetY; y += cathetiSize)
            {
                for (float x = offSetX; x < (int)this.OwnerGridPart.Size.X + offSetX; x+= cathetiSize)
                {
                    PointHand pointHand =
                        new PointHand(
                            new Point(x, y),
                            new Point(x + cathetiSize, y + cathetiSize));

                    #region Entries validation

                    if (!this.OwnerGridPart.HasContained(pointHand))
                    {
                        continue;
                    }

                    #endregion

                    var newLineDelegate = new Action(delegate () {
                        currentLine = new Line(new Point { X = cathetiSize, Y = cathetiSize }, pointHand);
                        currentLine.OnwerGridPart = this.OwnerGridPart;
                        lineList.Add(currentLine);
                    });

                    if(currentLine == null)
                    {
                        newLineDelegate.Invoke();
                    }
                    else
                    {
                        if(!currentLine.Assimilate(pointHand))
                        {
                            newLineDelegate.Invoke();
                        }
                    }
                }
            }

            #region Joining horizontal lines

            #region Horizontal assimilation delegate algorithm

            var mergePointDelegate = new Func<Line, Line, bool>(delegate (Line line, Line other)
            {
                PointHand myFistPoint = line.PointCache.First();
                PointHand myLastPoint = line.PointCache.Last();

                PointHand otherFirstPoint = other.PointCache.First();
                PointHand otherLastPoint = other.PointCache.Last();

                bool lastMerge =
                    line.MergePoint(otherLastPoint);
                bool firstMerge =
                    line.MergePoint(otherFirstPoint);

                return lastMerge || firstMerge;
            });

            #endregion

            var xGroupIterator = 
                lineList.GroupBy(l => l.PointCache.First().StartPoint.X);

            foreach (var group in xGroupIterator)
            {
                Line comparedLine = group.ElementAt(0);
                for (int i = 1; i < group.Count(); i++)
                {
                    Line other = group.ElementAt(i);

                    #region Entries validation

                    //if(other.PointCache.Count > 1)
                    //{
                    //    continue;
                    //}

                    #endregion

                    if (comparedLine.Assimilate(other, mergePointDelegate)) // <-- Use another signature to extends the match
                    {
                        lineList.Remove(other);
                    }
                }
            }

            #endregion

            return lineList;
        }

    }

}