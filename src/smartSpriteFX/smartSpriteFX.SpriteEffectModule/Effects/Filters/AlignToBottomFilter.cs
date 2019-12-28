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
                this.BottomMargin = 1;
            }

            #endregion

            CropFilter cropFilter = 
                cropFilter = new CropFilter();

            var cropFrame = frame.Clone();
            int previousHeight = frame.Height;
            int previousWidth = frame.Width;

            if (!cropFilter.ApplyFilter(cropFrame, index))
            {
                return false;
            }
            else
            {
                var marginBottom = this.BottomMargin;

                List<PointInfo> pointInfoList = new List<PointInfo>();

                frame.Height = previousHeight;
                frame.Width = previousWidth;
                frame.Buffer.BackgroundColor = frame.GetPixel(0, 0);
                frame.Buffer.CLEAR();

                for (int y = 0; y < cropFrame.Height; y++)
                {
                    for (int x = 0; x < cropFrame.Width; x++)
                    {
                        var pixel = cropFrame.GetPixel(x, y);
                        if (!pixel.HasValue) continue;
                        int newY = y + previousHeight - cropFrame.Height;
                        int newX = x + (frame.Width - cropFrame.Width) / 2;

                        pointInfoList.Add(new PointInfo(newX, newY, pixel.Value)); 
                    }
                }

                frame.SetPixel(pointInfoList);
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
