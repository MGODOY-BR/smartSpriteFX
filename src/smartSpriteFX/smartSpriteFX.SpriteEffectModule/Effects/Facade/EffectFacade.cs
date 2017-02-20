
using smartSuite.smartSpriteFX.Animations;
using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static System.Windows.Forms.Control;

namespace smartSuite.smartSpriteFX.Effects.Facade{
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
        /// Finds the filter in collection of selected filters.
        /// </summary>
        /// <typeparam name="T">The type of desired filter</typeparam>
        /// <returns>The instance of the fires found filter or null if it doesn't exist.</returns>
        public static T FindFilter<T>() where T : IEffectFilter
        {
            return EffectEngine.FindFilter<T>();
        }

        /// <summary>
        /// Gets the <see cref="TransparentBackgroundFilter"/> in selected filter list.
        /// </summary>
        /// <returns>
        /// The filter in selected filter list, or null if none has been found.
        /// </returns>
        public static TransparentBackgroundFilter GetTransparentBackgroundFilter()
        {
            return EffectEngine.GetTransparentBackgroundFilter();
        }

        /// <summary>
        /// Gets the control collection for preview board
        /// </summary>
        /// <returns></returns>
        public static ControlCollection GetControlCollectionFromPreviewBoard()
        {
            return EffectEngine.GetPreviewBoard().Controls;
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