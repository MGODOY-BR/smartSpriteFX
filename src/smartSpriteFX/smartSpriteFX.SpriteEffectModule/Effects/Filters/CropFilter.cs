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

        public override bool ApplyFilter(Picture frame, int index)
        {
            this._transparentBackgroundFilter.ApplyFilter(frame, index);
            var listBorder = frame.ListBorder();

            var minX = listBorder.Min(pointItem => pointItem.X);
            var maxX = listBorder.Max(pointItem => pointItem.X);
            var minY = listBorder.Min(pointItem => pointItem.Y);
            var maxY = listBorder.Max(pointItem => pointItem.Y);

            this._cutFilter.SetPoint(
                new Point(minX, minY), 
                new Point(maxX, maxY));

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
