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

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter which is used to cut frames
    /// </summary>
    public class CutFilter : SmartSpriteOriginalFilterBase
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
        }

        public override bool ApplyFilter(Picture frame, int index)
        {
            #region Point organization

            if (this._pointA == null && this._pointB == null)
            {
                this._pointA.X = 0;
                this._pointA.Y = 0;

                this._pointB.X = frame.Width;
                this._pointB.Y = frame.Height;
            }
            if (this._pointA.CompareTo(this._pointB) > 0)
            {
                this._pointA = this._pointB.Clone();
                this._pointB = this._pointA.Clone();
            }

            #endregion

            var transparentBackground = EffectFacade.GetTransparentBackgroundFilter();

            var originalFrame = frame.Clone();
            frame.ReleaseBuffer();

            frame.Width = (int)Math.Abs(this._pointB.X - this._pointA.X);
            frame.Height = (int)Math.Abs(this._pointB.Y - this._pointA.Y);

            int minX = (int)this._pointA.X;
            int minY = (int)this._pointA.Y;

            if (minX < 0) minX = 0;
            if (minY < 0) minY = 0;

            int maxX = (int)this._pointB.X;
            int maxY = (int)this._pointB.Y;

            if (maxX > originalFrame.Width) maxX = originalFrame.Width;
            if (maxY > originalFrame.Height) maxY = originalFrame.Height;

            var sourceList = originalFrame.GetAllPixels();
            foreach (var sourceItem in sourceList)
            {
                int y = (int)sourceItem.Y;
                int x = (int)sourceItem.X;

                #region Entries validation

                if (y < minY)
                {
                    continue;
                }
                if (y >= maxY)
                {
                    continue;
                }
                if (x < minX)
                {
                    continue;
                }
                if (x >= maxX)
                {
                    continue;
                }

                #endregion

                var pixel = sourceItem.Color;

                if (transparentBackground != null)
                {
                    if (_colorComparer.EqualsButNoAlpha(pixel, transparentBackground.TransparentColor))
                    {
                        pixel = ColorBuffer.GetSlightlyDifferentColor(pixel);
                    }
                }
                else if (_colorComparer.EqualsButNoAlpha(pixel, frame.TransparentColor))
                {
                    pixel = ColorBuffer.GetSlightlyDifferentColor(pixel);
                }
                frame.ReplacePixel(
                    Math.Abs(x - minX),
                    Math.Abs(y - minY),
                    pixel);
            }
            originalFrame.ReleaseBuffer();

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
