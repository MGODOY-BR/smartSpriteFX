
using smartSuite.smartSprite.Effects.Infra;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// Defines an official filter of SmartSprite library
	/// </summary>
	public abstract class SmartSpriteOriginalFilterBase : IEffectFilter {

		/// <summary>
		/// Defines an official filter of SmartSprite library
		/// </summary>
		public SmartSpriteOriginalFilterBase() {
		}

        public bool ApplyFilter(Picture frame, int index)
        {
            throw new NotImplementedException();
        }

        public Identification GetIdentification()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public IConfigurationPanel ShowConfigurationPanel()
        {
            throw new NotImplementedException();
        }
    }
}