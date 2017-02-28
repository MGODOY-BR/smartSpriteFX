using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Pictures.PixelPatterns;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter used to emulate draw made by hand
    /// </summary>
    public class MadeByHandFilter : SmartSpriteOriginalFilterBase
    {
        public override bool ApplyFilter(Picture frame, int index)
        {
            List<PointInfo>[] columnList =
                frame.GetColumns();

            List<PointInfo>[] rowList =
                frame.GetLines();

            return true;
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            throw new NotImplementedException();
        }
    }
}
