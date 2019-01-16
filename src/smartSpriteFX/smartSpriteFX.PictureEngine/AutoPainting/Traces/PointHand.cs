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
    /// Represents a point made by hand
    /// </summary>
    public class PointHand : PointRange
    {
        public PointHand() : base()
        {
        }

        public PointHand(Point startPoint, Point endPoint) : base(startPoint, endPoint)
        {
        }

        public PointHand(float x, float y)
        {
            this.StartPoint = new Point(x, y);
            this.EndPoint = new Point(x, y);
        }

        public PointHand(Point point)
        {
            this.StartPoint = point;
            this.EndPoint = point.Clone();
        }

        public PointHand Clone()
        {
            var returnValue = new PointHand();
            returnValue.SetPoint(this.StartPoint.X, this.StartPoint.Y, this.EndPoint.X, this.EndPoint.Y);
            return returnValue;
        }

        /// <summary>
        /// Gets the owner line
        /// </summary>
        public Line OwnerLine { get; set; }

        /// <summary>
        /// Calculate the middle points
        /// </summary>
        /// <returns></returns>
        public Point CalculateMiddlePoints()
        {
            var hypotenuse = this.CalculateHypotenuse();
            var perpendicularBissector = hypotenuse / 2;
            var a = (float)Math.Sqrt(perpendicularBissector * perpendicularBissector / 2);
            return new Point
            {
                X = this.StartPoint.X + a,
                Y = this.StartPoint.Y + a,
            };
        }

        /// <summary>
        /// Gets the edges of point
        /// </summary>
        /// <returns></returns>
        public List<Point> GetEdges()
        {
            return new List<Point>
            {
                this.StartPoint,
                new Point
                {
                    X = this.EndPoint.X,
                    Y = this.StartPoint.Y,
                },
                this.EndPoint,
                new Point
                {
                    X = this.StartPoint.X,
                    Y = this.EndPoint.Y,
                },
            };
        }

        public override string ToString()
        {
            return this.StartPoint.ToString();
        }
    }
}
