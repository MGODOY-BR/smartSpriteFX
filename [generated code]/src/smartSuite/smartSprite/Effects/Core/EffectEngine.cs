
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Core{
	/// <summary>
	/// It´s the main engine
	/// </summary>
	protected class EffectEngine {

		/// <summary>
		/// It´s the main engine
		/// </summary>
		protected EffectEngine() {
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
		/// Applies the filter collection to all the animation
		/// </summary>
		/// <returns></returns>
		public static void Apply() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Gets the frame iterator
		/// </summary>
		/// <returns></returns>
		public static FrameIterator GetIterator() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Updates the main view board and returns the image.
		/// </summary>
		/// <returns></returns>
		public static Image UpdatePreviewBoard() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Initializates the engine
		/// </summary>
		/// <param name="fullPath">		public static void Initializate(String fullPath) {</param>
			// TODO implement here
		}

	}
}