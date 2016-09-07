
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Pictures;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// It´s a filter that split an animation
	/// </summary>
	public class SplitAnimationFilter : SmartSpriteOriginalFilterBase, IEffectFilter {

		/// <summary>
		/// It´s a filter that split an animation
		/// </summary>
		public SplitAnimationFilter() {
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