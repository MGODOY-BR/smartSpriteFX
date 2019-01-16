
using smartSuite.smartSprite.AutoPainting.Grids;
using smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static smartSuite.smartSpriteFX.Pictures.Point;

namespace smartSuite.smartSprite.AutoPainting.Traces{
    /// <summary>
    /// Represents a line made of ordered points
    /// </summary>
    public class Line : IComparable<Line>, IEquatable<Line>
    {
        /// <summary>
        /// It's the angle manager
        /// </summary>
        private AngleManager _angleManager = new AngleManager();
        
        /// <summary>
        /// Gets or sets the point radio
        /// </summary>
        public Point PointRadio { get; set; }

        /// <summary>
        /// It's the cache of points
        /// </summary>
        public List<PointHand> PointCache { get; set; } = new List<PointHand>();

        /// <summary>
        /// Gets or sets the parent polygon
        /// </summary>
        public List<Polygon> OwnerPolygonList { get; set; }

        /// <summary>
        /// It's an eventual corner between the lines
        /// </summary>
        public List<Corner> CornerList
        {
            get
            {
                return this._angleManager.CornerList;
            }
        }

        /// <summary>
        /// Gets an indicator informing if the line has been analysed
        /// </summary>
        public bool WasAnalysed { get; set; }

        public Line()
        {
            this.PointRadio = new Point(1, 1);
            this.OwnerPolygonList = new List<Polygon>();
        }

        public Line(Point pointRadio)
        {
            #region Entries validation

            if (pointRadio == null)
            {
                throw new ArgumentNullException("pointRadio");
            }

            #endregion

            this.PointRadio = pointRadio;
            this.OwnerPolygonList = new List<Polygon>();
        }

        public Line(Point pointRadio, PointHand pointItem)
        {
            #region Entries validation

            if (pointRadio == null)
            {
                throw new ArgumentNullException("pointRadio");
            }
            if (pointItem == null)
            {
                throw new ArgumentNullException("pointItem");
            }

            #endregion

            this.PointRadio = pointRadio;

            this.PointCache.Add(pointItem);
            pointItem.OwnerLine = this;
            this.OwnerPolygonList = new List<Polygon>();
        }

        /// <summary>
        /// Creates a line between the points
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        public Line(Point pointRadio, PointHand startPoint, PointHand endPoint)
        {
            #region Entries validation

            if (pointRadio == null)
            {
                throw new ArgumentNullException("pointRadio");
            }
            if (startPoint == null)
            {
                throw new ArgumentNullException("startPoint");
            }
            if (endPoint == null)
            {
                throw new ArgumentNullException("endPoint");
            }

            #endregion

            if(startPoint.CompareTo(endPoint) == 1)
            {
                var tempPoint = startPoint.Clone();
                startPoint = endPoint.Clone();
                endPoint = tempPoint.Clone();
            }

            this.PointRadio = pointRadio;

            PointHand pointHand = new PointHand();
            pointHand.SetPoint(startPoint.StartPoint.X, startPoint.StartPoint.Y, endPoint.EndPoint.X, endPoint.EndPoint.Y);
            this.PointCache.Add(pointHand);
            pointHand.OwnerLine = this;
            this.OwnerPolygonList = new List<Polygon>();
        }

        /// <summary>
        /// Sort the lines
        /// </summary>
        public void Sort()
        {
            TraceSorter sorter = new TraceSorter();
            sorter.Sort(this.PointCache);

            this._angleManager.Scan(
                this.PointCache.ToArray());
        }

        /// <summary>
        /// Informs if the lines are in sequence
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool IsSequel(Line line)
        {
            #region Entries validation

            if (line == null)
            {
                throw new ArgumentNullException("line");
            }
            if(this.IsEmpty)
            {
                return false;
            }
            if(line.IsEmpty)
            {
                return false;
            }

            #endregion

            PointHand myLastPoint = this.PointCache.Last();
            PointHand otherFirstPoint = line.PointCache.First();
            PointHand myFistPoint = this.PointCache.First();
            PointHand otherLastPoint = line.PointCache.Last();

            return
                // Comparing oposite points
                this.CanBeSequel(myLastPoint, otherFirstPoint) ||
                this.CanBeSequel(myFistPoint, otherLastPoint) ||
                // Comparing similar points
                this.CanBeSequel(myFistPoint, otherFirstPoint) ||
                this.CanBeSequel(myLastPoint, otherLastPoint);
        }

        /// <summary>
        /// Gets an indicator informing if there some points in line
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty
        {
            get
            {
                #region Entries validation

                if(this.PointCache.Count == 0)
                {
                    return true;
                }
                if(this.PointCache.Count == 1)
                {
                    return this.PointCache.First().Size.X == 0 && this.PointCache.First().Size.Y == 0;
                }

                #endregion

                return false;
            }
        }

        /// <summary>
        /// It's the owner grid part
        /// </summary>
        public GridPart OnwerGridPart { get; set; }

        /// <summary>
        /// Assimilates (if it can) the point and returns an indicator about it.
        /// </summary>
        /// <returns></returns>
        // public bool Assimilate(PointHand pointHand)
        public bool Assimilate(PointHand newPoint)
        {
            #region Entries validation

            if (newPoint == null)
            {
                throw new ArgumentNullException("newPoint");
            }
            if(this.IsEmpty)
            {
                this.PointCache.Add(newPoint);
                newPoint.OwnerLine = this;
                return true;
            }
            if(newPoint.OwnerLine != null)
            {
                return false;
            }

            #endregion

            var firstPoint = this.PointCache.First();
            var lastPoint = this.PointCache.Last();

            if(this.CanBeSequel(lastPoint, newPoint))
            {
                if (this.IsInclined(lastPoint, newPoint))
                {
                    this.PointCache.Add(newPoint);
                }
                else
                {
                    lastPoint.UpdatePoint(newPoint.EndPoint);
                }

                newPoint.OwnerLine = this;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the edges
        /// </summary>
        /// <returns></returns>
        public PointHand[] GetEdges()
        {
            var topLeft = this.PointCache.First();
            var bottomRight = this.PointCache.Last();

            return new PointHand[2]
            {
                topLeft,
                bottomRight,
            };
        }

        /// <summary>
        /// Get an indicator informing if the both points compounds a corner
        /// </summary>
        /// <returns></returns>
        public bool IsCorner(PointHand point, PointHand other)
        {
            #region Entries validation

            if(!point.IsContained(other.StartPoint) && !point.IsContained(other.EndPoint))
            {
                return false;
            }

            #endregion

            double sin = CalculateSin(point, other);

            // Trigonometric cicle: https://i.ytimg.com/vi/PYo4yaxPFZs/maxresdefault.jpg
            // 1 = Pi
            // 1.5 = 3 Pi / 2
            // return sin > 1.25;
            // Console.WriteLine(sin);
            return sin >= 0.8 || sin < 0;
        }

        /// <summary>
        /// Calculate the sin between the points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public double CalculateSin(PointHand point, PointHand other)
        {
            #region Entries validation

            if (point == null)
            {
                throw new ArgumentNullException("point");
            }
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (this.PointRadio == null)
            {
                throw new ArgumentNullException("this.PointRadio");
            }

            #endregion

            var a = other.EndPoint.X - point.StartPoint.X;
            var b = other.EndPoint.Y - point.StartPoint.Y;
            var hipotenuse = Math.Sqrt((a * a) + (b * b));
            var sin = b / hipotenuse;

            return sin;
        }

        /// <summary>
        /// Gets an indicator informing if the other point is inclined relating to the current
        /// </summary>
        /// <returns></returns>
        private bool IsInclined(PointHand point, PointHand other)
        {
            #region Entries validation

            if (point == null)
            {
                throw new ArgumentNullException("point");
            }
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (this.PointRadio == null)
            {
                throw new ArgumentNullException("this.PointRadio");
            }

            #endregion

            if (
                other.StartPoint.Y > this.GetMaxY(point) / 2f
                )
            {
                return true;
            }
            if (
                other.StartPoint.Y < this.GetMaxY(point) / 2f
                )
            {
                return true;
            }
            if (
                other.StartPoint.Y < this.GetMinY(point) ||
                other.EndPoint.Y > this.GetMaxY(point))
            {
                return true;
            }
            if (
                other.StartPoint.X < this.GetMinX(point) ||
                other.EndPoint.X > this.GetMaxX(point))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Assimilates (if it can) the line and returns an indicator about it.
        /// </summary>
        /// <returns></returns>
        public bool Assimilate(Line otherLine)
        {
            var mergeLineDelegate = new Func<Line, Line, bool>(delegate (Line line, Line other)
            {
                //PointHand myLastPoint = this.PointCache.Last().Value;
                //PointHand otherFirstPoint = other.PointCache.First().Value;
                //PointHand myFistPoint = this.PointCache.First().Value;
                PointHand otherLastPoint = other.PointCache.Last();

                return this.MergePoint(otherLastPoint);
            });

            return this.Assimilate(otherLine, mergeLineDelegate);
        }

        /// <summary>
        /// Merges the point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool MergePoint(PointHand point)
        {
            for (float y = point.StartPoint.Y; y < point.EndPoint.Y; y++)
            {
                for (float x = point.StartPoint.X; x < point.EndPoint.X; x++)
                {
                    #region Exiting when find some filled point

                    if (this.PointCache.Contains(point))
                    {
                        break;
                    }

                    #endregion

                    if (!this.Assimilate(point))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Assimilates (if it can) the line and returns an indicator about it.
        /// </summary>
        /// <returns></returns>
        public bool Assimilate(Line other, Func<Line, Line, bool> mergeLineDelegate)
        {
            if(this.IsSequel(other))
            {
                bool returnValue =
                    mergeLineDelegate(this, other);

                return returnValue;
            }

            return false;
        }

        /// <summary>
        /// Checks if the other point can complete a sequel of another one
        /// </summary>
        /// <returns></returns>
        public bool CanBeSequel(PointHand trace, PointHand otherTrace)
        {
            #region Entries validation

            if (trace == null)
            {
                throw new ArgumentNullException("trace");
            }
            if (otherTrace == null)
            {
                throw new ArgumentNullException("otherTrace");
            }

            #endregion

            return
                (
                    (
                        this.GetMinX(trace) <= otherTrace.StartPoint.X &&
                        this.GetMinY(trace) <= otherTrace.StartPoint.Y
                    ) &&
                    (
                        this.GetMaxX(trace) >= otherTrace.EndPoint.X &&
                        this.GetMaxY(trace) >= otherTrace.EndPoint.Y
                    )
                ) ||
                (
                    (
                        this.GetMinX(trace) <= otherTrace.EndPoint.X &&
                        this.GetMinY(trace) <= otherTrace.EndPoint.Y
                    )
                ) ||
                (
                    trace.StartPoint.NearlyEqualsTo(otherTrace.StartPoint) ||
                    trace.EndPoint.NearlyEqualsTo(otherTrace.EndPoint)
                ) ||
                (
                    trace.StartPoint.NearlyEqualsTo(otherTrace.EndPoint) ||
                    trace.EndPoint.NearlyEqualsTo(otherTrace.StartPoint)
                );
        }

        /// <summary>
        /// Classifies the line
        /// </summary>
        /// <returns></returns>
        private SegmentClassificationEnum Classify(PointHand pointHandA, PointHand pointHandB)
        {
            #region Entries validation

            if (pointHandA == null)
            {
                throw new ArgumentNullException("pointHandA");
            }
            if (pointHandB == null)
            {
                throw new ArgumentNullException("pointHandB");
            }
            if (!this.CanBeSequel(pointHandA, pointHandB))
            {
                return SegmentClassificationEnum.UNRELATED;
            }

            #endregion

            var horizontal = new Func<SegmentClassificationEnum>(delegate ()
            {
                if (pointHandA.StartPoint.X < this.GetMinX(pointHandB) || pointHandA.EndPoint.X > this.GetMaxX(pointHandB))
                {
                    return SegmentClassificationEnum.STRAIGHT_HORIZONTALLY;
                }
                return SegmentClassificationEnum.UNDETERMINED;
            });

            var vertical = new Func<SegmentClassificationEnum>(delegate ()
            {
                if (pointHandA.StartPoint.Y < this.GetMinY(pointHandB) || pointHandA.EndPoint.Y > this.GetMaxY(pointHandB))
                {
                    return SegmentClassificationEnum.STRAIGHT_VERTICALLY;
                }
                return SegmentClassificationEnum.UNDETERMINED;
            });

            var horizontalVote = horizontal.Invoke();
            var verticalVote = vertical.Invoke();

            if(horizontalVote == SegmentClassificationEnum.STRAIGHT_HORIZONTALLY && verticalVote == SegmentClassificationEnum.UNDETERMINED)
            {
                return SegmentClassificationEnum.STRAIGHT_HORIZONTALLY;
            }
            else if (horizontalVote == SegmentClassificationEnum.UNDETERMINED && verticalVote == SegmentClassificationEnum.STRAIGHT_VERTICALLY)
            {
                return SegmentClassificationEnum.STRAIGHT_VERTICALLY;
            }
            else if(horizontalVote == SegmentClassificationEnum.STRAIGHT_HORIZONTALLY && verticalVote == SegmentClassificationEnum.STRAIGHT_VERTICALLY)
            {
                return SegmentClassificationEnum.INCLINATION;
            }
            else
            {
                return SegmentClassificationEnum.UNDETERMINED;
            }
        }

        /// <summary>
        /// Gets the minimum X
        /// </summary>
        /// <returns></returns>
        private float GetMinX(PointHand trace)
        {
            return trace.StartPoint.X - this.PointRadio.X;
        }

        /// <summary>
        /// Gets the maximum X
        /// </summary>
        /// <returns></returns>
        private float GetMaxX(PointHand trace)
        {
            return trace.EndPoint.X + this.PointRadio.X;
        }

        /// <summary>
        /// Gets the minimum Y
        /// </summary>
        /// <returns></returns>
        private float GetMinY(PointHand trace)
        {
            return trace.StartPoint.Y - this.PointRadio.Y;
        }

        /// <summary>
        /// Gets the maximum Y
        /// </summary>
        /// <returns></returns>
        private float GetMaxY(PointHand trace)
        {
            return trace.EndPoint.Y + this.PointRadio.Y;
        }

        public override string ToString()
        {
            #region Entries validation

            if(this.PointCache.Count == 0)
            {
                return "[EMPTY]";
            }

            #endregion

            // return String.Format("({0}) ... ({1})", this.PointCache.FirstOrDefault(), this.PointCache.LastOrDefault());
            return String.Format("({0}) ... ({1})", this.PointCache.FirstOrDefault().StartPoint, this.PointCache.LastOrDefault().StartPoint);
        }

        public int CompareTo(Line other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if(this.ToString() == other.ToString())
            {
                return 0;
            }
            if(this.IsEmpty && !other.IsEmpty)
            {
                return -1;
            }
            if (!this.IsEmpty && other.IsEmpty)
            {
                return 1;
            }
            if (this.IsEmpty && other.IsEmpty)
            {
                return 0;
            }

            #endregion

            return this.PointCache.First().StartPoint.CompareTo(other.PointCache.First().StartPoint);
        }

        public bool Equals(Line other)
        {
            return this.CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}