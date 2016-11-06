
using smartSuite.smartSpriteFX.SpriteEffectModule.Infra.UI;
using smartSuite.smartSpriteFX.Animations;
using smartSuite.smartSpriteFX.Effects.FilterEngine;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace smartSuite.smartSpriteFX.Effects.Core{
	/// <summary>
	/// It´s the main engine
	/// </summary>
	internal static class EffectEngine {

        /// <summary>
        /// It's the number of completed tasks
        /// </summary>
        private static int _completedTask;

        /// <summary>
        /// It´s the output path
        /// </summary>
        private static string _outputPath;

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
        /// It´s the list of thread used during Apply method
        /// </summary>
        private static List<Thread> _applyingThreadList = new List<Thread>();

        /// <summary>
        /// It´s an instance of callback used to echo the responses to user, or even get the requests from it
        /// </summary>
        private static IApplyFilterCallback _callback;

        /// <summary>
        /// Gets the output path
        /// </summary>
        /// <returns></returns>
        public static string GetOutputPath()
        {
            return EffectEngine._outputPath;
        }

        /// <summary>
        /// Applies the filter collection to all the animation
        /// </summary>
        /// <returns></returns>
        public static void Apply()
        {
            EffectEngine.Apply(null);
        }

        /// <summary>
        /// Cancels the apply method
        /// </summary>
        internal static void CancelApplying()
        {
            lock (_applyingThreadList)
            {
                for (int i = 0; i < _applyingThreadList.Count; i++)
                {
                    var thread = _applyingThreadList[i];
                    thread.Abort();
                }
                _applyingThreadList.Clear();
            }
        }

        /// <summary>
        /// Applies the filter collection to all animation, but considering UI controls infrastructure
        /// </summary>
        /// <param name="callback"></param>
        public static void ApplyFromUI(IApplyFilterCallback callback)
        {
            #region Entries validation

            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            #endregion

            callback.ApplyFilter();
        }

        /// <summary>
        /// Applies the filter collection to all the animation
        /// </summary>
        /// <returns></returns>
        public static void Apply(IApplyFilterCallback callback)
        {
            #region Entries validation

            if (EffectEngine._iterator == null)
            {
                throw new ArgumentNullException("EffectEngine._iterator");
            }
            if (EffectEngine._applyingThreadList == null)
            {
                throw new ArgumentNullException("EffectEngine._applyingThreadList");
            }

            #endregion

            EffectEngine._callback = callback;
            EffectEngine._outputPath = null;

            ThreadPool.SetMinThreads(3, 30);
            ThreadPool.SetMaxThreads(7, 70);

            EffectEngine._applyingThreadList.Clear();
            List<WaitHandle> syncList = new List<WaitHandle>();
            EffectEngine._iterator.Reset();
            EffectEngine._completedTask = 0;

            while (EffectEngine._iterator.Next())
            {
                WaitHandle sync = new AutoResetEvent(false);
                syncList.Add(sync);
                ThreadPool.QueueUserWorkItem(
                    ApplyWaitCallback, 
                    new object[3]
                    {
                        EffectEngine._iterator.GetCurrent().Clone(),
                        EffectEngine._iterator.GetFrameIndex(),
                        sync
                    });
            }

            #region Wainting to finish

            for (int i = 0; i < syncList.Count; i++)
            {
                var syncItem = syncList[i];

                syncItem.WaitOne();
            }

            #endregion

            if (callback != null)
            {
                callback.UpdateProgress(100f, true);
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
                throw new ArgumentNullException("EffectEngine._previewBoard", "previewBoard hasn't setted yet. Call SetPreviewBoard() before.");
            }
            if (EffectEngine._iterator == null)
            {
                throw new ArgumentNullException("EffectEngine._iterator");
            }
            if (EffectEngine._filterList == null)
            {
                throw new ArgumentNullException("EffectEngine._filterList");
            }
            if (EffectEngine._filterList.Count() == 0)
            {
                throw new ArgumentException("EffectEngine._filterList", "None filter has been selected!");
            }

            #endregion

            if (EffectEngine._iterator.GetFrameIndex() == -1)
            {
                EffectEngine._iterator.MoveFirst();
            }

            var previewFrame = EffectEngine._iterator.GetCurrent().Clone();
            var frameIndex = EffectEngine._iterator.GetFrameIndex();
            previewFrame.ClearCache();
            foreach (var filterItem in EffectEngine._filterList.GetFilterBufferList())
            {
                var draftFrame = previewFrame.Clone();
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
        /// <param name="fullPath">It´s the path of animation</param>
        public static void Initializate(String fullPath)
        {
            // Cleaning the Temp directory
            var tempFiles = Directory.GetFiles("Temp", "*.*");
            foreach (var tempFileItem in tempFiles)
            {
                try
                {
                    File.Delete(tempFileItem);
                }
                catch (IOException ex)
                {
                    // Errors in this operation cannot impact the whole operation
                    Console.WriteLine("Error during cleanup initialize operation: " + ex.Message);
                }
            }

            // Loads the filteres
            FilterCollection.Load();

            // Loading animation folder
            EffectEngine._iterator = FrameIterator.Open(fullPath);
        }

        /// <summary>
        /// Gets the selected filter list
        /// </summary>
        /// <returns></returns>
        public static List<IEffectFilter> GetSelectedFilterList()
        {
            return EffectEngine._filterList.GetFilterBufferList();
        }

        /// <summary>
        /// Clears all the filter buffer
        /// </summary>
        public static void ClearFilter()
        {
            lock (EffectEngine._filterList)
            {
                EffectEngine._filterList.Clear();
            }
        }

        /// <summary>
        /// Registers a filter
        /// </summary>
        /// <param name="effectFilter">A effect</param>
        public static void RegisterFilter(IEffectFilter effectFilter)
        {
            lock (EffectEngine._filterList)
            {
                EffectEngine._filterList.Register(effectFilter);
            }
        }

        /// <summary>
        /// Registers a filter
        /// </summary>
        /// <param name="effectFilter">A effect</param>
        /// <param name="filterOrderIndex">An order of filter</param>
        public static void RegisterFilter(IEffectFilter effectFilter, int filterOrderIndex)
        {
            lock (EffectEngine._filterList)
            {
                EffectEngine._filterList.Register(effectFilter, filterOrderIndex);
            }
        }

        /// <summary>
        /// Unregisters a filter
        /// </summary>
        /// <param name="effectFilter">A effect</param>
        public static void UnRegisterFilter(IEffectFilter effectFilter)
        {
            lock (EffectEngine._filterList)
            {
                EffectEngine._filterList.UnRegister(effectFilter);
            }
        }

        /// <summary>
        /// Ups the filter in list order
        /// </summary>
        /// <param name="filter">An instance of filter</param>
        public static void UpFilterOrder(IEffectFilter filter)
        {
            lock (EffectEngine._filterList)
            {
                EffectEngine._filterList.UpOrder(filter);
            }
        }

        /// <summary>
        /// Downs the filter in list order
        /// </summary>
        /// <param name="filter">An instance of filter</param>
        public static void DownFilterOrder(IEffectFilter filter)
        {
            lock (EffectEngine._filterList)
            {
                EffectEngine._filterList.DownOrder(filter);
            }
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

        /// <summary>
        /// It´s a WaitCallBack delegate used to parallelized the process.
        /// </summary>
        /// <param name="state">A state made by an Object[3], composed by:
        /// <list type="bullet">
        /// <item>frame - is a Picture object handled as a frame in an animation</item>
        /// <item>index - is a integer which is a index of animation</item>
        /// <item>waitHandle = is a AutoResetEvent used like a synchronizer of multi-thread operation</item>
        /// </list>
        /// </param>
        private static void ApplyWaitCallback(object state)
        {
            #region Entries validation

            if (state == null)
            {
                throw new ArgumentNullException("state");
            }
            object[] stateArray = (object[])state;
            if (stateArray.Length != 3)
            {
                throw new ArgumentOutOfRangeException("stateArray", stateArray.Length, "StateArray must have 3 dimensions");
            }

            #endregion

            lock (EffectEngine._applyingThreadList)
            {
                EffectEngine._applyingThreadList.Add(Thread.CurrentThread);
            }
            Picture frame = (Picture)stateArray[0];
            int index = (int)stateArray[1];
            AutoResetEvent waitHandle = (AutoResetEvent)stateArray[2];

            try
            {
                EffectEngine._outputPath =
                    EffectEngine._filterList.Apply(frame, index);

                if (EffectEngine._callback != null)
                {
                    lock (typeof(EffectEngine))
                    {
                        EffectEngine._completedTask++;
                        float percentage = (float)EffectEngine._completedTask / (float)EffectEngine.GetIterator().CountFrames() * (float)100;
                        EffectEngine._callback.UpdateProgress(percentage, false);
                    }
                }
            }
            finally
            {
                waitHandle.Set();
            }
        }
    }
}