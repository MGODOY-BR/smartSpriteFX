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
            CropFilter cropFilter = 
                cropFilter = new CropFilter();

            int previousHeight = frame.Height;
            #region Fragment of instable code
            // var originalFrame = frame.Clone(Picture.CloneMode.Full);
            #endregion
            if (!cropFilter.ApplyFilter(frame, index))
            {
                return false;
            }
            else
            {
                var marginBottom = this.BottomMargin;
                var pixelList = frame.GetAllPixels();
                var maxY = pixelList.Max(p => p.Y);
                var maxYCut = maxY;
                var offSet = Math.Abs(previousHeight - maxY);

                // frame.Height = frame.OriginalHeight; // <-- This can't be done because it invalidates the scale filter
                frame.Height = previousHeight;

                pixelList.ForEach(p => p.Y = (offSet + p.Y) - marginBottom);

                // Getting the lowest Y after the update
                var minY = pixelList.Min(p => p.Y);

                // Recalculating the heigh to warranty the whole picture in frame
                if (minY < 0)
                {
                    int increment = (int)Math.Abs(minY);
                    frame.Height +=
                        // increment + (int)marginBottom;
                        (int)marginBottom;

                    pixelList.ForEach(p => p.Y += increment);

                    maxY = pixelList.Max(p => p.Y);
                    #region Instable code

                    //// Filling the lack of pixel
                    //if (maxY < frame.Height)
                    //{
                    //    // Getting the includ list
                    //    var includeList =
                    //        originalFrame.GetAllPixels().FindAll(p => p.Y > maxYCut);
                        
                    //    // Updatting the included list
                    //    includeList.ForEach(p => p.Y += increment);

                    //    // Adding the include
                    //    frame.SetPixel(includeList);
                    //}

                    #endregion
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
