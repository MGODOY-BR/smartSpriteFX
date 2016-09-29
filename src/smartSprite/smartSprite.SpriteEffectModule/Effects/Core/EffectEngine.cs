
using smartSuite.smartSprite.Animations;
using smartSuite.smartSprite.Effects.FilterEngine;
using smartSuite.smartSprite.Effects.Filters;
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
        private static FilterCollection _filterList = new FilterCollection();

		/// <summary>
		/// Applies the filter collection to all the animation
		/// </summary>
		/// <returns></returns>
		public static void Apply()
        {
            int frameIndex = 0;
            while (EffectEngine._iterator.Next())
            {
                EffectEngine._filterList.Apply(
                    EffectEngine._iterator.GetCurrent(),
                    frameIndex);

                frameIndex++;
            }
        }

		/// <summary>
		/// Gets the frame iterator
		/// </summary>
		/// <returns></returns>
		public static FrameIterator GetIterator()
        {
            return EffectEngine._iterator;
		}

		/// <summary>
		/// Updates the main view board and returns the image.
		/// </summary>
		/// <returns></returns>
		public static Image UpdatePreviewBoard()
        {
            throw new NotImplementedException();
		}

        /// <summary>
        /// Initializates the engine
        /// </summary>
        /// <param name="fullPath">		
        /// </param>
        public static void Initializate(String fullPath)
        {
            // Loading animation folder
            EffectEngine._iterator = FrameIterator.Open(fullPath);
        }

        /// <summary>
        /// Registers a filter
        /// </summary>
        /// <param name="effectFilter">A effect</param>
        /// <param name="frameIndex">An order of filter</param>
        public static void RegisterFilter(IEffectFilter effectFilter, int frameIndex)
        {
            EffectEngine._filterList.Register(effectFilter, frameIndex);
        }

        /// <summary>
        /// Unregisters a filter
        /// </summary>
        /// <param name="effectFilter">A effect</param>
        public static void UnRegisterFilter(IEffectFilter effectFilter)
        {
            EffectEngine._filterList.UnRegister(effectFilter);
        }

        /// <summary>
        /// Ups the filter in list order
        /// </summary>
        /// <param name="filter">An instance of filter</param>
        public static void UpFilterOrder(IEffectFilter filter)
        {
            EffectEngine._filterList.UpOrder(filter);
        }

        /// <summary>
        /// Downs the filter in list order
        /// </summary>
        /// <param name="filter">An instance of filter</param>
        public static void DownFilterOrder(IEffectFilter filter)
        {
            EffectEngine._filterList.DownOrder(filter);
        }
    }
}