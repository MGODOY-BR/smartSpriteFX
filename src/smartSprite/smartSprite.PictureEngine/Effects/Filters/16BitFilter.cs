
using smartSuite.smartSprite.Effects.Infra;
using smartSuite.smartSprite.Effects.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// Represents a filter to convert the frame to 16 bit appearence.
	/// </summary>
	public class _16BitFilter : SmartSpriteOriginalFilterBase {

		/// <summary>
		/// Represents a filter to convert the frame to 16 bit appearence.
		/// </summary>
		public _16BitFilter() {
		}

		/// <summary>
		/// 
		/// </summary>
		private ColorBuffer _buffer;

		/// <summary>
		/// 
		/// </summary>
		private ResolutionTranslator _translator;

	}
}