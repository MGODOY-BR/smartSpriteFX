using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Tools;
using System.Drawing;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// It´s a filter that emphasizes the border of picture
    /// </summary>
    public class BorderFilter : SmartSpriteOriginalFilterBase
    {
        /// <summary>
        /// It´s the width of border.
        /// </summary>
        private float _traceBorderWidth;

        /// <summary>
        /// It´s the borderColor
        /// </summary>
        private Color _borderColor = Color.Black;

        /// <summary>
        /// Gets or sets the weight of trace
        /// </summary>
        public float TraceBorderWidth
        {
            get
            {
                return _traceBorderWidth;
            }

            set
            {
                this._traceBorderWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of trace
        /// </summary>
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }

            set
            {
                this._borderColor = value;
            }
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Border Line");
            identification.SetDescription("A filter which adds a border line around the picture (requires a Transparent Background filter before)");
            identification.SetGroup("Picture");

            return identification;
        }

        /// <summary>
        /// Applies the filter
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool ApplyFilter(Picture frame, int index)
        {
            #region Entries validation

            if (frame == null)
            {
                throw new ArgumentNullException("frame");
            }

            #endregion

            var pointBorderList = frame.ListBorder();

            Dictionary<string, PointInfo> traceBorderPointList =
                new Dictionary<string, PointInfo>();

            // Enphasizing the color
            if (this._borderColor == Color.Transparent)
            {
                this._borderColor = ColorUtil.Invert(frame.TransparentColor);
            }

            for (int i = 0; i < pointBorderList.Count; i++)
            {
                var pointBorderItem = pointBorderList[i];

                // Getting the original color
                var originalColor = frame.GetPixel((int)pointBorderItem.X, (int)pointBorderItem.Y);

                // Getting the trace border
                var list = this.MakeTraceBorder(pointBorderItem);

                foreach (var item in list)
                {
                    var key = item.ToString();
                    if (!traceBorderPointList.ContainsKey(key))
                    {
                        traceBorderPointList.Add(key, new PointInfo(item.Value, this._borderColor));
                    }
                }
            }

            // Drawing the trace border
            foreach (var traceBorderPointItem in traceBorderPointList)
            {
                frame.ReplacePixel(
                    (int)traceBorderPointItem.Value.X, (int)traceBorderPointItem.Value.Y, traceBorderPointItem.Value.Color);
            }

            return true;
        }

        public override void Reset()
        {
            this._traceBorderWidth = 2;
            this._borderColor = Color.Black;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new BorderFilterConfigurationPanelControl();
        }

        /// <summary>
        /// Gets a list of point corresponding to a large trace
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, smartSuite.smartSpriteFX.Pictures.Point> MakeTraceBorder(smartSuite.smartSpriteFX.Pictures.Point refPoint)
        {
            #region Entries validation

            if (refPoint == null)
            {
                throw new ArgumentNullException("refPoint");
            }

            #endregion

            // Calculating the ray of circle
            Dictionary<string, smartSuite.smartSpriteFX.Pictures.Point> returnList = 
                new Dictionary<string, smartSuite.smartSpriteFX.Pictures.Point>();

            for (float x = refPoint.X; x < refPoint.X + this._traceBorderWidth; x++)
            {
                var point =
                    new smartSuite.smartSpriteFX.Pictures.Point(
                        x,
                        refPoint.Y);

                returnList.Add(
                    point.ToString(),
                    point);
            }

            return returnList;
        }
        
    }
}
