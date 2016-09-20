﻿using smartSuite.smartSprite.Effects.Filters;
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

            for(int i = 0; i < pointBorderList.Count; i++)
            {
                var pointBorderItem = pointBorderList[i];

                // Getting the original color
                var originalColor = frame.GetPixel((int)pointBorderItem.X, (int)pointBorderItem.Y);

                // Enphasizing the color
                var newColor = ColorUtil.Invert(originalColor);
                //if (!traceBorderPointList.ContainsKey(pointBorderItem.ToString()))
                //{
                //    traceBorderPointList.Add(pointBorderItem.ToString(), new PointInfo(pointBorderItem, newColor));
                //}

                // Getting the trace border
                var list = this.MakeTraceBorder(pointBorderItem);

                foreach (var item in list)
                {
                    var key = item.ToString();
                    if (!traceBorderPointList.ContainsKey(key))
                    {
                        traceBorderPointList.Add(key, new PointInfo(item.Value, newColor));
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
            this._traceBorderWidth = 6;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            throw new NotImplementedException();
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
            float ray = this._traceBorderWidth / (float)2;

            Dictionary<string, smartSuite.smartSprite.Pictures.Point> returnList = 
                new Dictionary<string, smartSuite.smartSprite.Pictures.Point>();

            // Looping through angles x
            for (double x = 0; x < 2 * Math.PI * 180; x++)
            {
                // Getting the y
                double y = Math.Sin(x);

                var point = new smartSuite.smartSprite.Pictures.Point((float)x, (float)y);
                point.X += refPoint.X;
                point.Y += refPoint.Y;

                returnList.Add(point.ToString(), point);
            }

            return returnList;
        }
        
    }
}
