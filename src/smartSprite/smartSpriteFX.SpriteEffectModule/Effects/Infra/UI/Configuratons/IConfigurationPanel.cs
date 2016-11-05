
using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons{
	/// <summary>
	/// 
	/// </summary>
	public interface IConfigurationPanel {

		/// <summary>
		/// Gets the panel
		/// </summary>
		/// <param name="effectFilter">It´s the effect filter that is being configurated.</param>
		/// <returns></returns>
		System.Windows.Forms.UserControl GetPanel(IEffectFilter effectFilter);

	}
}