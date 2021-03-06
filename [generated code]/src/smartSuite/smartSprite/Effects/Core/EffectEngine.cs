
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

		/// <summary>
		/// Registers a filter
		/// </summary>
		/// <param name="effectFilter">A effect</param>
		/// <param name="frameIndex">An order of filter</param>
		public static void RegisterFilter(IEffectFilter effectFilter, int frameIndex) {
			// TODO implement here
		}

		/// <summary>
		/// Unregisters a filter
		/// </summary>
		/// <param name="effectFilter">A effect</param>
		public static void UnRegisterFilter(IEffectFilter effectFilter) {
			// TODO implement here
		}

		/// <summary>
		/// Ups the filter in list order
		/// </summary>
		/// <param name="effectFilter">		public static void UpFilterOrder(IEffectFilter effectFilter ) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Downs the filter in list order
		/// </summary>
		/// <param name="effectFilter">		public static void DownFilterOrder(IEffectFilter effectFilter) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Sets the preview board
		/// </summary>
		/// <param name="previewBoard">		static void SetPreviewBoard(PictureBox previewBoard) {</param>
			// TODO implement here
		}

	}
}