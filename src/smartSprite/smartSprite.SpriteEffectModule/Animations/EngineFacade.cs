
using smartSuite.smartSprite.Effects.FilterEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Animations{
	/// <summary>
	/// Offers the ways to interact with engine
	/// </summary>
	public sealed class EngineFacade {

		/// <summary>
		/// Offers the ways to interact with engine
		/// </summary>
		public EngineFacade() {
		}

		/// <summary>
		/// It´s the reference to an iterator frame
		/// </summary>
		private FrameIterator _iterator;

		/// <summary>
		/// It´s a list of filters to apply.
		/// </summary>
		private FilterCollection _filterList;

		/// <summary>
		/// Updates the animation to reflect the changes.
		/// </summary>
		public void Refresh() {
			// TODO implement here
		}

	}
}