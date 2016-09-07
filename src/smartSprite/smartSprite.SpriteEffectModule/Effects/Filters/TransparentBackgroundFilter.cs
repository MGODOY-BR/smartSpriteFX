
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Pictures;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// Represents a filter to infers transparent background
	/// </summary>
	public class TransparentBackgroundFilter : SmartSpriteOriginalFilterBase , IEffectFilter {

		/// <summary>
		/// Represents a filter to infers transparent background
		/// </summary>
		public TransparentBackgroundFilter() {
		}

        public override bool ApplyFilter(Picture frame, int index)
        {
            throw new NotImplementedException();
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