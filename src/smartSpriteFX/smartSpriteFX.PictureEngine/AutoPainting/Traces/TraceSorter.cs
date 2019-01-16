using smartSuite.smartSprite.AutoPainting.Traces;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces
{
    /// <summary>
    /// Puts a list of point in sort of trace
    /// </summary>
    public class TraceSorter
    {
        /// <summary>
        /// Sorts the point to follow the trace and returns the result
        /// </summary>
        public void Sort(List<PointHand> pointList)
        {
            Depot depot = null;
            StubbornBehaviourScanner stubbornScanner = null;

            #region Looking for the pseudo-edges

            Line sampleLine = pointList.First().OwnerLine;
            Point minPoint = new Point
            {
                X = pointList.Min(p => p.StartPoint.X),
                Y = pointList.Min(p => p.StartPoint.Y),
            };
            Point maxPoint = new Point
            {
                X = pointList.Max(p => p.StartPoint.X),
                Y = pointList.Max(p => p.StartPoint.Y),
            };

            #endregion

            #region Decting the final point

            var getLineDelegate = new Func<List<PointHand>, float, List<PointHand>>(delegate(List<PointHand> sourceList, float y)
            {
                return sourceList.FindAll(p => p.StartPoint.Y == y);
            });

            var lastPoints = getLineDelegate(pointList, maxPoint.Y);
            var minX = lastPoints.Min(p => p.StartPoint.X);
            var maxX = lastPoints.Max(p => p.StartPoint.X);

            var currentX = maxX;
            var currentY = maxPoint.Y;

            #endregion

            #region Y iterator Algorithms

            // Up direction
            var yUpIteratorAlgorithm = new AxisIteratorAlgorithm
            {
                Start = currentY,
                RuleFunctionDelegate = ((sourceList, y) => sourceList.Count == 0 ? false : y >= sourceList.Min(p => p.StartPoint.Y)),
                StepFunctionDelegate = (() => sampleLine.PointRadio.Y * -1),
                EdgeFunctionDelegate = new Func<List<PointHand>, float, float, bool>(delegate(List<PointHand> sourceList, float x, float y) {
                    return !sourceList.Exists(p => p.StartPoint.X == x && p.StartPoint.Y == y);
                }),
                IsLastPointFunctionDelegate = new Func<List<PointHand>, float, bool>(delegate(List<PointHand> sourceList, float coordinate)
                {
                    if(sourceList.Count == 0) return true;
                    return coordinate == sourceList.Min(p => p.StartPoint.Y);
                }),
                GetLastXFunctionDelegate = new Func<List<PointHand>, float>(delegate (List<PointHand> sourceList)
                {
                    return sourceList.Min(p => p.StartPoint.X);
                }),
            };
            // Down direction
            var yDownIteratorAlgorithm = new AxisIteratorAlgorithm
            {
                Start = currentY,
                RuleFunctionDelegate = ((sourceList, y) => sourceList.Count == 0 ? false : y <= sourceList.Max(p => p.StartPoint.Y)),
                StepFunctionDelegate = (() => sampleLine.PointRadio.Y),
                EdgeFunctionDelegate = new Func<List<PointHand>, float, float, bool>(delegate (List<PointHand> sourceList, float x, float y) {
                    return !sourceList.Exists(p => p.StartPoint.X == x && p.StartPoint.Y == y);
                }),
                IsLastPointFunctionDelegate = new Func<List<PointHand>, float, bool>(delegate (List<PointHand> sourceList, float coordinate)
                {
                    if (sourceList.Count == 0) return true;
                    return coordinate == sourceList.Max(p => p.StartPoint.Y);
                }),
                GetLastXFunctionDelegate = new Func<List<PointHand>, float>(delegate (List<PointHand> sourceList)
                {
                    return sourceList.Max(p => p.StartPoint.X);
                }),
            };

            yUpIteratorAlgorithm.OpositeAlgorithmDelegate = () => yDownIteratorAlgorithm;
            yDownIteratorAlgorithm.OpositeAlgorithmDelegate = () => yUpIteratorAlgorithm;

            AxisIteratorAlgorithm yIteratorAlgorithm = yUpIteratorAlgorithm;

            #endregion

            #region X iterator Algorithms

            // Up direction
            var xUpIteratorAlgorithm = new AxisIteratorAlgorithm
            {
                Start = currentX,
                RuleFunctionDelegate = ((sourceList, x) => sourceList.Count == 0 ? false : x >= sourceList.Min(p => p.StartPoint.X)),
                StepFunctionDelegate = (() => sampleLine.PointRadio.X * -1),
                EdgeFunctionDelegate = new Func<List<PointHand>, float, float, bool>(delegate (List<PointHand> sourceList, float x, float y) {
                    return !sourceList.Exists(p => p.StartPoint.Y == y && p.StartPoint.X == x);
                }),
                IsLastPointFunctionDelegate = new Func<List<PointHand>, float, bool>(delegate (List<PointHand> sourceList, float coordinate)
                {
                    if (sourceList.Count == 0) return true;
                    return coordinate == sourceList.Min(p => p.StartPoint.X);
                }),
                GetLastXFunctionDelegate = new Func<List<PointHand>, float>(delegate (List<PointHand> sourceList)
                {
                    throw new NotSupportedException();
                }),
            };
            // Down direction
            var xDownIteratorAlgorithm = new AxisIteratorAlgorithm
            {
                Start = currentY,
                RuleFunctionDelegate = ((sourceList, x) => sourceList.Count == 0 ? false : x <= sourceList.Max(p => p.StartPoint.X)),
                StepFunctionDelegate = (() => sampleLine.PointRadio.X),
                EdgeFunctionDelegate = new Func<List<PointHand>, float, float, bool>(delegate (List<PointHand> sourceList, float x, float y) {
                    return !sourceList.Exists(p => p.StartPoint.Y == y && p.StartPoint.X == x);
                }),
                IsLastPointFunctionDelegate = new Func<List<PointHand>, float, bool>(delegate (List<PointHand> sourceList, float coordinate)
                {
                    if (sourceList.Count == 0) return true;
                    return coordinate == sourceList.Max(p => p.StartPoint.X);
                }),
                GetLastXFunctionDelegate = new Func<List<PointHand>, float>(delegate (List<PointHand> sourceList)
                {
                    throw new NotSupportedException();
                }),
            };

            xUpIteratorAlgorithm.OpositeAlgorithmDelegate = () => xDownIteratorAlgorithm;
            xDownIteratorAlgorithm.OpositeAlgorithmDelegate = () => xUpIteratorAlgorithm;

            AxisIteratorAlgorithm xIteratorAlgorithm = xUpIteratorAlgorithm;

            #endregion

            #region Other functions

            // Getting the point
            var getNextPointDelegate = new Func<List<PointHand>, float, float, PointHand>(delegate (List<PointHand> sourceList, float xx, float yy) {
                return sourceList.Find(p => p.StartPoint.X == xx && p.StartPoint.Y == yy);
            });

            #endregion

            if (depot == null) depot = new Depot(pointList, lastPoints, minPoint, maxPoint);

            EdgeDetection edgeDetection = new EdgeDetection();
            var edge = edgeDetection.GetEdge(pointList, maxX, maxPoint.Y);
            currentX = edge.X;
            currentY = edge.Y;

            var yStubborn = new StubbornBehaviourScanner();

            if (stubbornScanner == null) stubbornScanner = new StubbornBehaviourScanner();
            TimeOutManager timeOutManager = new TimeOutManager();

            while (depot.Count < pointList.Count && !timeOutManager.HasTimeOut())
            {
                for (float y = currentY; yIteratorAlgorithm.RuleFunctionDelegate(pointList, y); y += yIteratorAlgorithm.StepFunctionDelegate())
                {
                    currentY = y;

                    // Getting current points in Y axis
                    var currentPoints = getLineDelegate(pointList, currentY);

                    if (currentPoints.Count == 0) continue;

                    #region There is no X in current line, acquiring the last X in a line

                    // There are some point in this line?
                    if (xIteratorAlgorithm.EdgeFunctionDelegate(pointList, currentX, currentY))
                    {
                        // Getting the last X of this line
                        // currentX = currentPoints.Max(p => p.StartPoint.X);
                        currentX = yIteratorAlgorithm.GetLastXFunctionDelegate(currentPoints);
                    }

                    #endregion

                    if (depot.HasFullScanned(currentPoints))
                    {
                        // continue;
                        if (stubbornScanner.StubbornCount > 0)
                        {
                            // Enroscado em x = 130
                            currentX += xIteratorAlgorithm.StepFunctionDelegate();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    while (xIteratorAlgorithm.RuleFunctionDelegate(currentPoints, currentX))
                    {
                        var point = 
                            getNextPointDelegate(currentPoints, currentX, currentY);

                        if (point == null)
                        {
                            break;
                        }

                        if (!depot.Contains(point))
                        {
                            depot.Put(point);
                        }
                        else
                        {
                            if (stubbornScanner.Scan(currentX, currentY))
                            {
                                break;
                            }
                            else
                            {
                                if (xIteratorAlgorithm.IsLastPointFunctionDelegate(currentPoints, currentX))
                                {
                                    break;
                                }
                            }
                        }

                        #region Advancing X

                        currentX += xIteratorAlgorithm.StepFunctionDelegate();

                        #endregion
                    }

                    if (xIteratorAlgorithm.EdgeFunctionDelegate(pointList, currentX, currentY))
                    {
                        xIteratorAlgorithm = xIteratorAlgorithm.OpositeAlgorithmDelegate();
                        // Puts in new axes perspective
                        currentX += xIteratorAlgorithm.StepFunctionDelegate();
                    }
                }

                yIteratorAlgorithm = yIteratorAlgorithm.OpositeAlgorithmDelegate();
                if (yStubborn.Scan(currentX, currentY))
                {
                    break;
                }
            }

            depot.Overhide(pointList);
        }

        /// <summary>
        /// Represents the iterator algorithmn for the axis
        /// </summary>
        class AxisIteratorAlgorithm
        {
            /// <summary>
            /// It's the start point
            /// </summary>
            public float Start { get; set; }

            /// <summary>
            /// It's the rule to be executing
            /// </summary>
            /// <remarks>
            /// param -> The current axis
            /// </remarks>
            public Func<List<PointHand>, float, bool> RuleFunctionDelegate { get; set; }

            /// <summary>
            /// The step function
            /// </summary>
            public Func<float> StepFunctionDelegate { get; set; }

            /// <summary>
            /// The function to be used in the edge of Y
            /// </summary>
            public Func<List<PointHand>, float, float, bool> EdgeFunctionDelegate;

            /// <summary>
            /// Gets the indicator informing if the point is the last of the set
            /// </summary>
            public Func<List<PointHand>, float, bool> IsLastPointFunctionDelegate;

            /// <summary>
            /// The oposite algorithm delegate
            /// </summary>
            public Func<AxisIteratorAlgorithm> OpositeAlgorithmDelegate;

            /// <summary>
            /// Gets the last in the set
            /// </summary>
            public Func<List<PointHand>, float> GetLastXFunctionDelegate;

        }
        
        /// <summary>
        /// Controls the queue order
        /// </summary>
        class Depot
        {
            private Stack<PointHand> _innerStack;
            private Queue<PointHand> _innerQueue;

            /// <summary>
            /// Gets the edges
            /// </summary>
            public PointRange Edges { get; private set; } = new PointRange();

            public Depot(List<PointHand> source, List<PointHand> lastLine, Point minPoint, Point maxPoint)
            {
                var maxC = source.Where(p => p.StartPoint.X == minPoint.X).Max(p => p.StartPoint.Y);

                // Handle single vertical lines
                var group = source.GroupBy(p => p.StartPoint.X);
                if(group.Count() == 1)
                {
                    this._innerStack = new Stack<PointHand>();
                    return;
                }

                if (lastLine.Count == 1)
                {
                    if (maxC < maxPoint.Y)
                    {
                        this._innerStack = new Stack<PointHand>();
                    }
                    else
                    {
                        this._innerQueue = new Queue<PointHand>();
                    }
                }
                else
                {
                    this._innerStack = new Stack<PointHand>();
                }
            }

            public bool Contains(PointHand pointHand)
            {
                var container = this.GetContainer();
                var queue = container as Queue<PointHand>;
                var stack = container as Stack<PointHand>;

                if(queue != null)
                {
                    return queue.Contains(pointHand);
                }
                if(stack != null)
                {
                    return stack.Contains(pointHand);
                }

                throw new InvalidOperationException();
            }

            /// <summary>
            /// Gets an indicator informing if all the source had been scanned already
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public bool HasFullScanned(List<PointHand> source)
            {
                if (source.Count == 0) return false;

                bool returnValue = true;
                foreach (var sourceItem in source)
                {
                    returnValue &= this.Contains(sourceItem);
                }

                return returnValue;
            }

            public int Count
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the latest point used through the Put method
            /// </summary>
            public PointHand LatestPoint { get; private set; }

            /// <summary>
            /// Put a point in one of queues
            /// </summary>
            /// <param name="pointHand"></param>
            public void Put(PointHand pointHand)
            {
                if(this._innerStack != null)
                {
                    this._innerStack.Push(pointHand);
                }
                else
                {
                    this._innerQueue.Enqueue(pointHand);
                }
                this.LatestPoint = pointHand;

                this.Count++;

                #region Handling the edges

                var edges = pointHand.GetEdges();
                var minX = edges.Min(p => p.X);
                var maxX = edges.Max(p => p.X);
                var minY = edges.Min(p => p.Y);
                var maxY = edges.Max(p => p.Y);

                if (this.Edges.StartPoint == null || minX < this.Edges.StartPoint.X)
                {
                    if (this.Edges.StartPoint == null) this.Edges.StartPoint = new Point();
                    this.Edges.StartPoint.X = minX;
                }
                if (this.Edges.StartPoint == null || minY < this.Edges.StartPoint.Y)
                {
                    if (this.Edges.StartPoint == null) this.Edges.StartPoint = new Point();
                    this.Edges.StartPoint.Y = minY;
                }
                if (this.Edges.EndPoint == null || maxX > this.Edges.EndPoint.X)
                {
                    if (this.Edges.EndPoint == null) this.Edges.EndPoint = new Point();
                    this.Edges.EndPoint.X = maxX;
                }
                if (this.Edges.EndPoint == null || maxY > this.Edges.EndPoint.Y)
                {
                    if (this.Edges.EndPoint == null) this.Edges.EndPoint = new Point();
                    this.Edges.EndPoint.Y = maxY;
                }

                #endregion
            }

            /// <summary>
            /// Overhides the original list
            /// </summary>
            /// <param name="list"></param>
            public void Overhide(List<PointHand> list)
            {
                System.Collections.ICollection container = GetContainer();

                list.Clear();
                while (container.Count > 0)
                {
                    if (this._innerStack != null)
                    {
                        list.Add(
                            this._innerStack.Pop());
                    }
                    else
                    {
                        list.Add(
                            this._innerQueue.Dequeue());
                    }
                }
            }

            private System.Collections.ICollection GetContainer()
            {
                System.Collections.ICollection depot;
                if (_innerQueue != null)
                {
                    depot = _innerQueue;
                }
                else
                {
                    depot = _innerStack;
                }

                return depot;
            }
        }

        /// <summary>
        /// Scans the stubborn behaviour
        /// </summary>
        class StubbornBehaviourScanner
        {
            /// <summary>
            /// It's the last point scan
            /// </summary>
            private Point _pointScan;

            /// <summary>
            /// Gets the amount of stubborn loopings in the same coordinates
            /// </summary>
            public int StubbornCount { get; private set; }

            /// <summary>
            /// Scans the time
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public bool Scan(float x, float y)
            {
                try
                {
                    #region Entries validation

                    if (this._pointScan == null)
                    {
                        this.StubbornCount = 0;
                        return false;
                    }

                    #endregion

                    if (this._pointScan.X == x && this._pointScan.Y == y)
                    {
                        this.StubbornCount++;
                        return true;
                    }
                }
                finally
                {
                    this._pointScan = new Point(x, y);
                }

                this.StubbornCount = 0;
                return false;
            }

            /// <summary>
            /// Resets the state of stubborn
            /// </summary>
            public void Reset()
            {
                this.StubbornCount = 0;
                this._pointScan = null;
            }
        }

        /// <summary>
        /// Manages the timeout
        /// </summary>
        class TimeOutManager
        {
            private DateTime _startTime;

            public TimeOutManager()
            {
                _startTime = DateTime.Now;
            }

            /// <summary>
            /// Indicates if the time has gone
            /// </summary>
            public bool HasTimeOut()
            {
                return DateTime.Now.Subtract(_startTime).TotalMilliseconds > 3000;
            }
        }

        /// <summary>
        /// Detects the Edges
        /// </summary>
        class EdgeDetection
        {
            /// <summary>
            /// Gets the edge
            /// </summary>
            /// <returns></returns>
            public Point GetEdge(List<PointHand> source, float inferredX, float inferredY)
            {
                List<PointHand> disconnectedPointList = new List<PointHand>();
                List<PointHand> connectedPointList = new List<PointHand>();
                int tolerance = (int)source.First().Size.X + 1;

                foreach (var sourceItem in source)
                {
                    Point refPoint = sourceItem.StartPoint;

                    var result = source
                        .FindAll(p => p.EndPoint.NearlyEqualsTo(refPoint, tolerance));

                    if (result.Count() < 3)
                    {
                        disconnectedPointList.Add(sourceItem);
                    }
                }

                #region Check per preferences

                var prefered = disconnectedPointList.LastOrDefault(p => p.StartPoint.X == inferredX && p.StartPoint.Y == inferredY);
                if(prefered != null)
                {
                    return prefered.StartPoint;
                }

                #endregion

                var lastDisconnectedPointLast = disconnectedPointList.LastOrDefault();
                if (lastDisconnectedPointLast == null) return new Point(inferredX, inferredY);
                return lastDisconnectedPointLast.StartPoint;
            }
        }

    }
}
