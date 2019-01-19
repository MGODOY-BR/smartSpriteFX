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

            int previousHeight = frame.Height;

            if (!cropFilter.ApplyFilter(frame, index))
            {
                return false;
            }
            else
            {
                var marginBottom = this.BottomMargin;
                var maxY =
                    frame.GetAllPixels()
                        .Where(
                            p => p.Color != Color.Empty && 
                            p.Color != Color.Transparent &&
                            (frame.TransparentColor.HasValue && p.Color.ToArgb() != frame.TransparentColor.Value.ToArgb()))
                        .Max(p => p.Y);
                var maxYCut = maxY;
                var offSet = Math.Abs(previousHeight - maxY);

                frame.Height = previousHeight;

                // pixelList.ForEach(p => p.Y = (offSet + p.Y) - marginBottom);
                // frame.Buffer.CLEAR();
                // frame.SetPixel(pixelList);

                int sourceY = (int)offSet - marginBottom;

                for (int x = 0; x < frame.Width; x++)
                {
                    for (int y = 0; y < frame.Height; y++)
                    {
                        var pixel = frame.GetPixel(x, sourceY);

                        if (pixel == null || pixel == frame.TransparentColor) pixel = Color.Empty;
                        frame.SetPixel(x, y, pixel.Value);

                        sourceY = ((int)offSet + y) - marginBottom;
                    }
                }
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
