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
    public class AlignToBottomFilter : SmartSpriteOriginalFilterBase
    {
        public override bool ApplyFilter(Picture frame, int index)
        {
            var cropFilter = EffectFacade.FindFilter<CropFilter>();
            if(cropFilter == null)
            {
                cropFilter = new CropFilter();
            }

            int previousHeight = frame.Height;
            if (!cropFilter.ApplyFilter(frame, index))
            {
                return false;
            }
            else
            {
                var lineList = frame.GetLines();
                var lineRef = lineList[0];
                var minY = lineRef.Min(p => p.Y);
                var maxY = lineRef.Max(p => p.Y);
                var yOffSetMin = Math.Abs(previousHeight - minY);
                var yOffSetMax = Math.Abs(previousHeight - maxY);
                var yOffSet = yOffSetMin + yOffSetMax;

                // frame.Height = frame.OriginalHeight; // <-- This can't be done because it invalidates the scale filter
                frame.Height = previousHeight;
                foreach (var lineItem in lineList)
                {
                    lineItem.ForEach(l => l.Y = (yOffSetMax - l.Y));
                }
            }

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
            identification.SetName("Align to Bottom");
            identification.SetDescription("A filter used to align the central image to bottom. It brings CropFilter inside and comes up a transparent background.");
            identification.SetGroup("Picture");

            return identification;
        }
    }
}
