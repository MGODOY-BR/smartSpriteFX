using smartSuite.smartSprite.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Pictures;
using smartSprite.SpriteEffectModule.Effects.Tools;
using System.Drawing;
using smartSprite.PictureEngine.Pictures;
using smartSuite.smartSprite.Effects.Infra;
using smartSprite.SpriteEffectModule.Effects.Filters.UI;

namespace smartSprite.SpriteEffectModule.Effects.Filters
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
        private Color _borderColor = Color.Transparent;

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Border Line");
            identification.setDescription("A filter which adds a border line around the picture (requires a Transparent Background filter before)");

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
            this._borderColor = Color.Transparent;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new NoneConfigurationPanelControl();
        }

        /// <summary>
        /// Gets a list of point corresponding to a large trace
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, smartSuite.smartSprite.Pictures.Point> MakeTraceBorder(smartSuite.smartSprite.Pictures.Point refPoint)
        {
            #region Entries validation

            if (refPoint == null)
            {
                throw new ArgumentNullException("refPoint");
            }

            #endregion

            // Calculating the ray of circle
            Dictionary<string, smartSuite.smartSprite.Pictures.Point> returnList = 
                new Dictionary<string, smartSuite.smartSprite.Pictures.Point>();

            for (float x = refPoint.X; x < refPoint.X + this._traceBorderWidth; x++)
            {
                var point =
                    new smartSuite.smartSprite.Pictures.Point(
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
