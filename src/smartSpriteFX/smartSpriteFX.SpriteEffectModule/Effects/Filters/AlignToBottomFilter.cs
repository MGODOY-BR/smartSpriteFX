using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using smartSuite.smartSpriteFX.Effects.Facade;
using smartSuite.smartSpriteFX.Effects.Infra;
using System.Drawing;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Aligns the image to bottom
    /// </summary>
    public class AlignToBottomFilter : SmartSpriteOriginalFilterBase, IOnlyBottomMarginEffectFilter, ICutFilter
    {
        /// <summary>
        /// Gets or sets the bottom margin
        /// </summary>
        public int BottomMargin { get; set; }

        public override bool ApplyFilter(Picture frame, int index)
        {
            #region Entries validation

            if(this.BottomMargin == 0)
            {
                return false;
            }

            #endregion

            CropFilter cropFilter = 
                cropFilter = new CropFilter();

            var cropFrame = frame.Clone();
            int previousHeight = cropFrame.Height;
            int previousWidth = cropFrame.Width;

            if (!cropFilter.ApplyFilter(cropFrame, index))
            {
                return false;
            }
            else
            {
                var marginBottom = this.BottomMargin;
                var maxY = cropFrame.Height;

                List<PointInfo> pointInfoList = new List<PointInfo>();
                int startY = previousHeight - marginBottom;
                // int endX = cropFrame.Width;
                int sourceY = maxY - 1;
                for (int y = frame.Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < frame.Width; x++)
                    {
                        Color? pixel = null;
                        // if (y <= startY && x < endX)
                        if (y <= startY)
                        {
                            pixel = cropFrame.GetPixel(x, sourceY);
                        }

                        if (pixel == null || pixel == cropFrame.TransparentColor) pixel = Color.Transparent;

                        pointInfoList.Add(
                            new PointInfo(x, y, pixel.Value));
                    }
                    if (y <= startY) sourceY--;
                }

                frame.Buffer.CLEAR();
                frame.SetPixel(pointInfoList);
                frame.Width = cropFrame.Width;
            }

            return true;
        }

        public override void Reset()
        {
            this.BottomMargin = 0;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new MarginPanelControl();
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Align to Bottom");
            identification.SetDescription("A filter used to align the central image to bottom. It brings CropFilter inside.");
            identification.SetGroup("Picture");

            return identification;
        }
    }
}
