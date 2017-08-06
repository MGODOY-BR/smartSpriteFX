using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Aligns the image to bottom
    /// </summary>
    public class AlignToBottom : SmartSpriteOriginalFilterBase
    {
        public override bool ApplyFilter(Picture frame, int index)
        {
            throw new NotImplementedException();
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
