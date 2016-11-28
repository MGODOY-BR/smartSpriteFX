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

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter which is used to cut frames
    /// </summary>
    public class CutFilter : SmartSpriteOriginalFilterBase
    {
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

            var originalFrame = frame.Clone();
            frame.ReleaseBuffer();

            frame.Width = (int)(this._pointB.X - this._pointA.X);
            frame.Height = (int)(this._pointB.Y - this._pointA.Y);

            for (int y = 0; y < originalFrame.Height; y++)
            {
                #region Entries validation

                if (y < this._pointA.Y || y > this._pointB.Y)
                {
                    continue;
                }

                #endregion

                for (int x = 0; x < originalFrame.Width; x++)
                {
                    #region Entries validation

                    if (x < this._pointA.X || x > this._pointB.X)
                    {
                        continue;
                    }

                    #endregion

                    var pixel = originalFrame.GetPixel(x, y);
                    if (pixel.HasValue)
                    {
                        frame.ReplacePixel(x, y, pixel.Value);
                    }
                }
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
            identification.SetName("Cut");
            identification.SetDescription("A filter which cuts frames to remove unnecessary blank spaces from frames");
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
