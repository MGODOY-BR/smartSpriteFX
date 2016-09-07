
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
        /// Aplies the filter
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract bool ApplyFilter(Picture frame, int index);

        /// <summary>
        /// Resets the state of filter
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Shows de configuration panel
        /// </summary>
        /// <returns></returns>
        public abstract IConfigurationPanel ShowConfigurationPanel();

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public virtual Identification GetIdentification()
        {
            return 
                new Identification(
                    "SmartSprite Built-In Plugin",
                    "Atelier do Software",
                    "It´s a official SmartSprite Plugin");
        }

    }
}