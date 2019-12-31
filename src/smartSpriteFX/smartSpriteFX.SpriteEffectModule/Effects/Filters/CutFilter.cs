using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using smartSuite.smartSpriteFX.Effects.Facade;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter which is used to cut frames
    /// </summary>
    public class CutFilter : SmartSpriteOriginalFilterBase, ICutFilter
    {
        /// <summary>
        /// A comparer used to compare colors.
        /// </summary>
        private ColorEqualityComparer _colorComparer = new ColorEqualityComparer();

        /// <summary>
        /// It´s a point A
        /// </summary>
        private Point _pointA;

        /// <summary>
        /// It´s the point B
        /// </summary>
        private Point _pointB;

        /// <summary>
        /// Gets the top left most point
        /// </summary>
        public Point PointA
        {
            get
            {
                return _pointA;
            }
            internal set
            {
                _pointA = value;
            }
        }

        /// <summary>
        /// Gets the lower right most point
        /// </summary>
        public Point PointB
        {
            get
            {
                return _pointB;
            }
            internal set
            {
                _pointB = value;
            }
        }

        public override bool ApplyFilter(Picture frame, int index)
        {
            #region Point organization

            if (this._pointA == null && this._pointB == null)
            {
                this._pointA = new Point
                {
                    X = 0,
                    Y = 0,
                };

                this._pointB = new Point
                {
                    X = frame.Width,
                    Y = frame.Height,
                };
            }
            if (this._pointA == null)
            {
                this._pointA = new Point
                {
                    X = 0,
                    Y = 0,
                };
            }

            if (this._pointB == null)
            {
                this._pointB = new Point
                {
                    X = frame.Width,
                    Y = frame.Height,
                };
            }
            if (this._pointA.CompareTo(this._pointB) > 0)
            {
                this._pointA = this._pointB.Clone();
                this._pointB = this._pointA.Clone();
            }

            #endregion

            int minX = (int)this._pointA.X;
            int minY = (int)this._pointA.Y;

            if (minX < 0) minX = 0;
            if (minY < 0) minY = 0;

            int maxX = (int)this._pointB.X;
            int maxY = (int)this._pointB.Y;

            if (maxX > frame.Width) maxX = frame.Width;
            if (maxY > frame.Height) maxY = frame.Height;

            frame.Width = (int)Math.Abs(this._pointB.X - this._pointA.X);
            frame.Height = (int)Math.Abs(this._pointB.Y - this._pointA.Y);

            var sourceList = frame.GetAllPixels();
            List<PointInfo> pointList = new List<PointInfo>();
            foreach (var sourceItem in sourceList)
            {
                int y = (int)sourceItem.Y;
                int x = (int)sourceItem.X;

                #region Entries validation

                if (y < minY) continue;
                if (y >= maxY) continue;
                if (x < minX) continue;
                if (x >= maxX) continue;

                #endregion

                pointList.Add(
                    new PointInfo(Math.Abs(x - minX), Math.Abs(y - minY), sourceItem.Color));
            }
            frame.ClearCache();
            frame.SetPixel(pointList);

            return true;
        }

        public override void Reset()
        {
            this._pointA = null;
            this._pointB = null;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new CutConfigurationPanelControl();
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Free Cut");
            identification.SetDescription("A filter used to cut pieces from frames");
            identification.SetGroup("Picture");

            return identification;
        }

        /// <summary>
        /// Sets the points regardless of the state of filter
        /// </summary>
        public void SetPoint(Point pointA, Point pointB)
        {
            this._pointA = pointA;
            this._pointB = pointB;
        }

    }
}
