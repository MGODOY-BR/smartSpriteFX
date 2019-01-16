using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.AutoPainting.Traces
{
    /// <summary>
    /// Manages the angle evolution
    /// </summary>
    public class AngleManager
    {
        /// <summary>
        /// It's a tolerance required to a difference been considered a angle
        /// </summary>
        public double LeanTolerance { get; set; } = 0.3;

        /// <summary>
        /// It's the tend to lean
        /// </summary>
        private double _leanFactor = 0;

        /// <summary>
        /// It's the corner detected during the scan
        /// </summary>
        public List<Corner> CornerList { get; private set; } = new List<Corner>();

        /// <summary>
        /// It's the calculation lean buffer
        /// </summary>
        private PointRange _buffer;

        /// <summary>
        /// Indicates if the calculation has been started
        /// </summary>
        private bool _hasStarted;

        /// <summary>
        /// Scans the points, looking for lean variance
        /// </summary>
        /// <param name="pointHand"></param>
        public void Scan(params PointHand[] pointHandList)
        {
            #region Entries validation

            if (pointHandList == null)
            {
                throw new ArgumentNullException("pointHandList");
            }

            #endregion

            foreach (var pointHand in pointHandList)
            {
                #region Entries validation

                if (this._buffer == null)
                {
                    this._buffer = pointHand.Clone();
                }

                #endregion

                this._buffer.UpdatePoint(pointHand.EndPoint);
                var leanDelegate = new Func<double>(delegate ()
                {
                    var returnValue =
                        Math.Round(
                            Math.Asin(
                                this._buffer.Size.Y / this._buffer.CalculateHypotenuse()),
                                1,
                                MidpointRounding.AwayFromZero);

                    return returnValue;
                });

                var lean = leanDelegate();

                if (!double.IsNaN(lean) && this.TooDifferent(lean))
                {
                    // Redoing lean calculation
                    // lean = leanDelegate();

                    if (this._hasStarted)
                    {
                        this.CornerList.Add(
                            new Corner
                            {
                                Vertice = this._buffer.StartPoint,
                            });
                    }
                    this._hasStarted = true;
                }
                #region Repositioning the buffer

                if (lean != this._leanFactor)
                {
                    this._buffer.StartPoint = pointHand.StartPoint.Clone();
                }

                #endregion

                if (pointHand.OwnerLine != null)
                {
                    if (pointHand.OwnerLine.ToString() == "(X = 123,2056, Y = 66,5) ... (X = 123,2056, Y = 130,9231)")
                    {
                        //Console.WriteLine(pointHand.ToString() + " - " + lean);
                    }
                }

                this._leanFactor = lean;
            }
        }

        /// <summary>
        /// Gets an indicator informing that the lean is too different from the last one
        /// </summary>
        /// <param name="lean"></param>
        /// <returns></returns>
        private bool TooDifferent(double lean)
        {
            #region Entries validation

            if(double.IsNaN(lean) && double.IsNaN(this._leanFactor))
            {
                return false;
            }
            if(!double.IsNaN(lean) && double.IsNaN(this._leanFactor))
            {
                return true;
            }
            if (double.IsNaN(lean) && !double.IsNaN(this._leanFactor))
            {
                return true;
            }

            #endregion

            return 
                !(
                lean >= this._leanFactor - this.LeanTolerance &&
                lean <= this._leanFactor + this.LeanTolerance
                );
        }
    }
}
