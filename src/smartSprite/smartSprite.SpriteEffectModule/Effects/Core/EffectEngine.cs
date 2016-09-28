
using smartSuite.smartSprite.Animations;
using smartSuite.smartSprite.Effects.FilterEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Core{
	/// <summary>
	/// It´s the main engine
	/// </summary>
	internal static class EffectEngine {

		/// <summary>
		/// It´s the reference to an iterator frame
		/// </summary>
		private static FrameIterator _iterator;

		/// <summary>
		/// It´s a list of filters to apply.
		/// </summary>
		private static FilterCollection _filterList;

		/// <summary>
		/// Applies the filter collection to all the animation
		/// </summary>
		/// <returns></returns>
		public static void Apply() {
			// TODO implement here

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
        /// <param name="fullPath">		
        /// </param>
        public static void Initializate(String fullPath) {
			// TODO implement here
		}

	}
}