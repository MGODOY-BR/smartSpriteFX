
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
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;

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
        /// It´s a image used like a source to <see cref="UpdatePreviewBoard"/> method
        /// </summary>
        private static Picture _sourcePreviewImage;

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
        /// Sets the source preview image to use in <see cref="UpdatePreviewBoard"/> method
        /// </summary>
        /// <param name="sourcePreviewImage"></param>
        public static void SetSourcePreviewImage(Picture sourcePreviewImage)
        {
            #region Entries validation

            if (sourcePreviewImage == null)
            {
                throw new ArgumentNullException("sourcePreviewImage");
            }

            #endregion

            _sourcePreviewImage = sourcePreviewImage;
        }

        /// <summary>
        /// Gets thw source preview image
        /// </summary>
        /// <returns></returns>
        public static Picture GetSourcePreviewImage()
        {
            return _sourcePreviewImage;
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
        /// Reloads the preview board
        /// </summary>
        /// <param name="fullFileName"></param>
        public static void ReloadPreviewBoard(string fullFileName)
        {
            #region Entries validation
            
            if (String.IsNullOrEmpty(fullFileName))
            {
                throw new ArgumentNullException("fullFileName");
            }

            #endregion

            EffectEngine._previewBoard.Load(fullFileName);
        }

        /// <summary>
        /// Applies the filter collection to all animation, but considering UI controls infrastructure
        /// </summary>
        /// <param name="callback">A control used to shows the result of processing, like a progress bar</param>
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
        /// Applies the filter collection in the frame, but considering UI controls infrastructure
        /// </summary>
        /// <param name="callback">A control used to shows the result of processing, like a progress bar</param>
        /// <param name="frame">The frame to apply on</param>
        public static void ApplyFromUI(IApplyFilterCallback callback, Picture frame)
        {
            #region Entries validation

            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            #endregion

            callback.ApplyFilter(frame);
        }

        /// <summary>
        /// Applies the filter collection to all the animation
        /// </summary>
        /// <returns></returns>
        public static void Apply(IApplyFilterCallback callback)
        {
            Apply(callback, null);
        }

        /// <summary>
        /// Applies the filter collection in the frame, but considering UI controls infrastructure
        /// </summary>
        public static void ApplyFromUI(IApplyFilterCallback callback, List<Picture> pictureList)
        {
            EffectEngine._iterator = FrameIterator.Open(pictureList);
            callback.ApplyFilter(pictureList);
        }

        /// <summary>
        /// Applies the filter collection to all the animation
        /// </summary>
        /// <param name="callback">The controls that will be informed about the progress of processing</param>
        /// <param name="selectedFrameList">The list of selected frames to apply the filters. If null, all the frames of animation folder will be used</param>
        /// <returns></returns>
        public static void Apply(IApplyFilterCallback callback, params Picture[] selectedFrameList)
        {
            FrameIterator frameIterator = null;

            if (selectedFrameList == null)
            {
                frameIterator =
                    EffectEngine._iterator;
            }
            else
            {
                frameIterator =
                    FrameIterator.Open(selectedFrameList);
            }

            #region Entries validation

            if (frameIterator == null)
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

            ThreadPool.SetMinThreads(3, 40);
            ThreadPool.SetMaxThreads(10, 80);

            EffectEngine._applyingThreadList.Clear();
            List<WaitHandle> syncList = new List<WaitHandle>();
            frameIterator.Reset();
            EffectEngine._completedTask = 0;

            string outputFilterPath =
                Path.Combine(
                    Path.GetDirectoryName(frameIterator.GetFileList()[0]),
                    "filtered");

            #region Cleaning the path

            if (selectedFrameList == null)
            {
                // HACK: Just remove directory if all the animation has been selected
                if (Directory.Exists(outputFilterPath))
                {
                    var enumeratorFileList =
                        Directory.EnumerateFiles(outputFilterPath).GetEnumerator();

                    while (enumeratorFileList.MoveNext())
                    {
                        if (File.Exists(enumeratorFileList.Current))
                        {
                            File.Delete(enumeratorFileList.Current);
                        }
                    }
                }
            }

            #endregion

            while (frameIterator.Next())
            {
                WaitHandle sync = new AutoResetEvent(false);
                syncList.Add(sync);
                ThreadPool.QueueUserWorkItem(
                    ApplyWaitCallback, 
                    new object[3]
                    {
                        frameIterator.GetCurrent().Clone(),
                        frameIterator.GetFrameIndex(),
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
            if (_sourcePreviewImage == null)
            {
                throw new ArgumentNullException("_sourcePreviewImage", "You must choose an image before");
            }

            #endregion

            var previewFrame = _sourcePreviewImage.Clone();
            var frameIndex = EffectEngine._iterator.GetFrameIndex();

            foreach (var filterItem in EffectEngine._filterList.GetFilterBufferList().Where(f => !(f is FrameRateFilter)))
            {
                Picture draftFrame = previewFrame.Clone(Picture.CloneMode.StructureOnly);
                draftFrame.ShareDataWithMe(previewFrame);

                if (filterItem.ApplyFilter(draftFrame, frameIndex))
                {
                    previewFrame = draftFrame;
                }
            }

            // Saving the result file
            var tempFile = Path.Combine("Temp", Guid.NewGuid().ToString());
            if(!Directory.Exists("Temp"))
            {
                Directory.CreateDirectory("Temp");
            }
            previewFrame.SaveCopy(tempFile);

            // Clearing the preview board
            EffectEngine._previewBoard.Controls.Clear();

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
            Initializate();
            // Loading animation folder
            EffectEngine._iterator = FrameIterator.Open(fullPath);
        }

        /// <summary>
        /// Initializates the engine
        /// </summary>
        public static void Initializate()
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

            PictureDatabase.Clear();
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
        /// Gets the <see cref="TransparentBackgroundFilter"/> in selected filter list.
        /// </summary>
        /// <returns>
        /// The filter in selected filter list, or null if none has been found.
        /// </returns>
        public static TransparentBackgroundFilter GetTransparentBackgroundFilter()
        {
            return EffectEngine.FindFilter<TransparentBackgroundFilter>();
        }

        /// <summary>
        /// Finds the filter in collection of selected filters.
        /// </summary>
        /// <typeparam name="T">The type of desired filter</typeparam>
        /// <returns>The instance of the fires found filter or null if it doesn't exist.</returns>
        public static T FindFilter<T>() where T : IEffectFilter
        {
            var selectedFilterList = EffectEngine.GetSelectedFilterList();
            Type typeFilter = typeof(T);
            foreach (var filterItem in selectedFilterList)
            {
                if (typeFilter.IsInstanceOfType(filterItem))
                {
                    return (T)filterItem;
                }
            }

            return default(T);
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
        /// Gets the preview board
        /// </summary>
        /// <returns></returns>
        internal static PictureBox GetPreviewBoard()
        {
            return EffectEngine._previewBoard;
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
            catch
            {
                throw;
            }
            finally
            {
                frame.ClearCache();
                waitHandle.Set();
            }
        }

        /// <summary>
        /// Saves the filter set
        /// </summary>
        /// <param name="fileName">The file name</param>
        public static void SaveFilterSet(string fileName)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            #endregion

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using (var stream = File.CreateText(fileName))
            {
                var filterSet = EffectEngine.GetSelectedFilterList();
                for (int i = 0; i < filterSet.Count; i++)
                {
                    var filterItem = filterSet[i];
                    var type = filterItem.GetType();

                    #region Property serializarion

                    StringBuilder propertyBuilder = new StringBuilder();
                    foreach (var property in type.GetProperties())
                    {
                        String valueString = null;
                        object valueObject = property.GetValue(filterItem);

                        if (valueObject != null)
                        {
                            if (!property.GetType().IsArray)
                            {
                                valueString = String.Format("[{0}]", valueObject.ToString());
                            }
                            else
                            {
                                StringBuilder propertyValueArray = new StringBuilder();
                                foreach (var valueItem in (object[])valueObject)
                                {
                                    if (valueItem != null)
                                    {
                                        propertyValueArray.AppendFormat("[{0}]", valueItem);
                                    }
                                }
                            }
                        }

                        propertyBuilder.Append("{");
                        propertyBuilder.AppendFormat("{0}{1}", property.Name, valueString);
                        propertyBuilder.Append("}");
                    }

                    #endregion

                    stream.Write(
                        String.Format("{2}-{0}={1}", type.Name, propertyBuilder.ToString(), i));

                    if(i + 1 != filterSet.Count)
                    {
                        stream.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the filter set
        /// </summary>
        /// <param name="fileName"></param>
        public static void LoadFilterSet(string fileName)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            List<IEffectFilter> filterList = FilterCollection.GetFilterPallete();
            if (filterList.Count == 0)
            {
                throw new ArgumentNullException("It´s needs load the filter buffer list first");
            }

            #endregion

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Regex mainRegEx = new Regex(@"^(\d+)-(_{0,1}\w+)=(.*)$", RegexOptions.Compiled);
            Regex propertiesRegEx = new Regex(@"\{(\w+)\[([a-z0-9\.\,\s=]+)\]{1,}\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            List<IEffectFilter> selectedFilterList = EffectEngine.GetSelectedFilterList();
            selectedFilterList.Clear();
            using (var stream = File.OpenText(fileName))
            {
                string line = stream.ReadLine();
                while (line != null)
                {
                    var mainMatch = mainRegEx.Match(line);

                    int index = int.Parse(mainMatch.Groups[1].Value);
                    string filterTypeName = mainMatch.Groups[2].Value;

                    for (int i = 0; i < filterList.Count; i++)
                    {
                        var filterItem = filterList[i];
                        if (filterTypeName.Equals(filterItem.GetType().Name))
                        {
                            EffectEngine.RegisterFilter(filterItem, index);

                            #region Recovering the state

                            var propertyMatchList =
                                propertiesRegEx.Matches(mainMatch.Groups[3].Value);

                            foreach (Match propertyMatchItem in propertyMatchList)
                            {
                                string propertyName = propertyMatchItem.Groups[1].Value;
                                var property = filterItem.GetType().GetProperty(propertyName);

                                try
                                {
                                    if (!property.PropertyType.IsArray)
                                    {
                                        String value = propertyMatchItem.Groups[2].Value;

                                        property.SetValue(
                                            filterItem,
                                            FormatValue(property.PropertyType, value));
                                    }
                                    else
                                    {
                                        for (int ii = 2; ii < propertyMatchItem.Groups.Count; ii++)
                                        {
                                            String value = propertyMatchItem.Groups[ii].Value;

                                            property.SetValue(
                                                filterItem,
                                                FormatValue(property.PropertyType, value));
                                        }
                                    }
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                            }

                            #endregion
                        }
                    }

                    line = stream.ReadLine();
                }
            }
        }

        /// <summary>
        /// Formats the value
        /// </summary>
        /// <param name="propertyType"></param>
        /// <param name="valueString"></param>
        /// <returns></returns>
        private static object FormatValue(Type propertyType, string valueString)
        {
            #region Entries validation

            if (propertyType == null)
            {
                throw new ArgumentNullException("propertyType");
            }
            if (String.IsNullOrEmpty(valueString))
            {
                return null;
            }

            #endregion

            Object value = valueString;

            if (propertyType.GetInterface(typeof(IFromString).Name) != null)
            {
                IFromString deserializable = (IFromString)Activator.CreateInstance(propertyType);
                deserializable.FillMe(valueString);
                value = deserializable;
            }

            return Convert.ChangeType(value, propertyType);
        }
    }

}
