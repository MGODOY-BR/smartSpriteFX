using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Core;
using System.Drawing;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter that automatically reduce the frames just to figure
    /// </summary>
    public class CropFilter : SmartSpriteOriginalFilterBase
    {
        /// <summary>
        /// It´s a transparent backgroundfilter
        /// </summary>
        private TransparentBackgroundFilter _transparentBackgroundFilter = new TransparentBackgroundFilter();

        /// <summary>
        /// It´s a cutFilter
        /// </summary>
        private CutFilter _cutFilter = new CutFilter();

        /// <summary>
        /// Gets the detected transparent color
        /// </summary>
        /// <returns></returns>
        public Color GetTransparentColor()
        {
            #region Entries validation

            if(_transparentBackgroundFilter == null)
            {
                return Color.Transparent;
            }

            #endregion

            return _transparentBackgroundFilter.TransparentColor;
        }

        /// <summary>
        /// Gets or create a transparent background filter
        /// </summary>
        /// <returns></returns>
        private static TransparentBackgroundFilter CreateTransparentBackgroundFilter()
        {
            var resultValue = EffectEngine.GetTransparentBackgroundFilter();

            if(resultValue == null)
            {
                resultValue = new TransparentBackgroundFilter();
            }
            return resultValue;
        }

        public override bool ApplyFilter(Picture frame, int index)
        {
            this._transparentBackgroundFilter.ApplyFilter(frame, index);
            var listBorder = frame.ListBorder();

            var minX = listBorder.Min(pointItem => pointItem.X);
            var maxX = listBorder.Max(pointItem => pointItem.X);
            var minY = listBorder.Min(pointItem => pointItem.Y);
            var maxY = listBorder.Max(pointItem => pointItem.Y);

            this._cutFilter.SetPoint(
                new Pictures.Point(minX, minY), 
                new Pictures.Point(maxX, maxY));

            this._cutFilter.ApplyFilter(frame, index);

            return true;
        }

        public override void Reset()
        {
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new NoneConfigurationPanelControl();
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Crop");
            identification.SetDescription("A filter used to automatically maintain just the image inside of frame, removing the empty spaces in frame");
            identification.SetGroup("Picture");

            return identification;
        }

    }
}
