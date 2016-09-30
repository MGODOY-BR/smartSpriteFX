
using smartSuite.smartSprite.Animations;
using smartSuite.smartSprite.Effects.FilterEngine;
using smartSuite.smartSprite.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        /// The preview board to see the results
        /// </summary>
        private static PictureBox _previewBoard;

        /// <summary>
        /// Applies the filter collection to all the animation
        /// </summary>
        /// <returns></returns>
        public static void Apply()
        {
            while (EffectEngine._iterator.Next())
            {
                EffectEngine._filterList.Apply(
                    EffectEngine._iterator.GetCurrent(),
                    EffectEngine._iterator.GetFrameIndex());
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
            #region Entries validation

            if (EffectEngine._previewBoard == null)
            {
                throw new ArgumentNullException("previewBoard hasn't setted yet. Call SetPreviewBoard() before.");
            }
            if (EffectEngine._iterator == null)
            {
                throw new ArgumentNullException("EffectEngine._iterator");
            }

            #endregion

            if (EffectEngine._iterator.GetFrameIndex() == -1)
            {
                EffectEngine._iterator.MoveFirst();
            }

            var previewFrame = EffectEngine._iterator.GetCurrent().Clone();
            var frameIndex = EffectEngine._iterator.GetFrameIndex();
            foreach (var filterItem in EffectEngine._filterList.GetFilterBufferList())
            {
                var draftFrame = previewFrame.Clone();
                filterItem.Reset();
                if (filterItem.ApplyFilter(draftFrame, frameIndex))
                {
                    previewFrame = draftFrame;
                }
            }

            // Saving the result file
            var tempFile = Path.Combine("Temp", Guid.NewGuid().ToString());
            previewFrame.SaveCopy(tempFile);

            // Updating the preview board
            EffectEngine._previewBoard.Load(tempFile);

            // Returns a copy of image of preview board
            return (Image)EffectEngine._previewBoard.Image.Clone();
        }

        /// <summary>
        /// Initializates the engine
        /// </summary>
        /// <param name="fullPath">		
        /// </param>
        public static void Initializate(String fullPath)
        {
            // Cleaning the Temp directory
            var tempFiles = Directory.GetFiles("Temp", "*.*");
            foreach (var tempFileItem in tempFiles)
            {
                File.Delete(tempFileItem);
            }

            // Loads the filteres
            FilterCollection.Load();

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

        /// <summary>
        /// Sets the preview board
        /// </summary>
        /// <param name="previewBoard">It´s a board to seet the image</param>		
        internal static void SetPreviewBoard(PictureBox previewBoard)
        {
            #region Entries validation

            if (previewBoard == null)
            {
                throw new ArgumentNullException("previewBoard");
            }

            #endregion

            EffectEngine._previewBoard = previewBoard;
        }

    }
}