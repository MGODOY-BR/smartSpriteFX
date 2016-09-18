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

            foreach (var pointBorderItem in pointBorderList)
            {
                // Getting the original color
                var originalColor = frame.GetPixel((int)pointBorderItem.X, (int)pointBorderItem.Y);

                // Enphasizing the color
                var newColor = ColorUtil.Invert(originalColor);

                // TODO: aumentar a largura do traço usando círculo e diâmetro e substituir
                List<smartSuite.smartSprite.Pictures.Point> traceBorderPointList = 
                    this.MakeTraceBorder(pointBorderItem);
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
        private List<smartSuite.smartSprite.Pictures.Point> MakeTraceBorder(smartSuite.smartSprite.Pictures.Point refPoint)
        {
            #region Entries validation

            if (refPoint == null)
            {
                throw new ArgumentNullException("refPoint");
            }

            #endregion

            // Calculating the area of circle
            double circleArea = Math.PI * Math.Pow((double)(this._traceBorderWidth / (float)2), (double)2);

            // Calculating scan boundaries
            int minX = Convert.ToInt32(refPoint.X - this._traceBorderWidth);
            int maxX = Convert.ToInt32(refPoint.X + this._traceBorderWidth);
            int minY = Convert.ToInt32(refPoint.Y - this._traceBorderWidth);
            int maxY = Convert.ToInt32(refPoint.Y + this._traceBorderWidth);

            List<smartSuite.smartSprite.Pictures.Point> returnList = new List<smartSuite.smartSprite.Pictures.Point>();

            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    double circleFactor = Math.Sqrt((x * x) + (y * y)); // <-- Circle factor is a hipotenuse indeed


                }
            }

            return returnList;
        }
    }
}
