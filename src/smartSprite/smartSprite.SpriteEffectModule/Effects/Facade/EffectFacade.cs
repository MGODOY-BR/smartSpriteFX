
using smartSuite.smartSprite.Animations;
using smartSuite.smartSprite.Effects.Core;
using smartSuite.smartSprite.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Facade{
	/// <summary>
	/// Offers the ways to interact with engine
	/// </summary>
	public static class EffectFacade {

		/// <summary>
		/// Gets the iterator of animation
		/// </summary>
		/// <returns></returns>
		public static FrameIterator GetIterator() {
            return EffectEngine.GetIterator();
		}

		/// <summary>
		/// Updates the main view board and returns the image.
		/// </summary>
		/// <returns></returns>
		public static Image UpdatePreviewBoard()
        {
            return EffectEngine.UpdatePreviewBoard();
        }

        /// <summary>
        /// Registers a filter
        /// </summary>
        /// <param name="effectFilter">A effect</param>
        /// <param name="frameIndex">An order of filter</param>
        public static void RegisterFilter(IEffectFilter effectFilter, int frameIndex)
        {
            EffectEngine.RegisterFilter(effectFilter, frameIndex);
        }

        /// <summary>
        /// Unregisters a filter
        /// </summary>
        /// <param name="effectFilter">A effect</param>
        public static void UnRegisterFilter(IEffectFilter effectFilter)
        {
            EffectEngine.UnRegisterFilter(effectFilter);
        }

        /// <summary>
        /// Ups the filter in list order
        /// </summary>
        /// <param name="filter">An instance of filter</param>
        public static void UpFilterOrder(IEffectFilter filter)
        {
            EffectEngine.UpFilterOrder(filter);
        }

        /// <summary>
        /// Downs the filter in list order
        /// </summary>
        /// <param name="filter">An instance of filter</param>
        public static void DownFilterOrder(IEffectFilter filter)
        {
            EffectEngine.DownFilterOrder(filter);
        }

    }
}