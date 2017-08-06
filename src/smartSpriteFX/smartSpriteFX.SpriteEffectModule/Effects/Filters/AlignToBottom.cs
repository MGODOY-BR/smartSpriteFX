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

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Aligns the image to bottom
    /// </summary>
    public class AlignToBottom : SmartSpriteOriginalFilterBase
    {
        public override bool ApplyFilter(Picture frame, int index)
        {
            var cropFilter = EffectFacade.FindFilter<CropFilter>();
            if(cropFilter == null)
            {
                cropFilter = new CropFilter();
            }

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
                var yOffSetMin = Math.Abs(frame.OriginalHeight - minY);
                var yOffSetMax = Math.Abs(frame.OriginalHeight - maxY);
                var yOffSet = yOffSetMin + yOffSetMax;

                frame.Height = frame.OriginalHeight;
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
    }
}
