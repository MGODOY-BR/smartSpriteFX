
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Facade{
	/// <summary>
	/// Offers the ways to interact with engine
	/// </summary>
	public sealed class EffectFacade {

		/// <summary>
		/// Offers the ways to interact with engine
		/// </summary>
		public EffectFacade() {
		}

		/// <summary>
		/// Gets the iterator of animation
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

	}
}