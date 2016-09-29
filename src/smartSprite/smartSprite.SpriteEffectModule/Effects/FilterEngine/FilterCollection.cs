
using smartSuite.smartSprite.Effects.Filters;
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace smartSuite.smartSprite.Effects.FilterEngine{
	/// <summary>
	/// It´s a collection of filters
	/// </summary>
	public class FilterCollection {

        /// <summary>
        /// It´s the error list collection occurred during the Load method
        /// </summary>
        private static List<String> _loadErrorList = new List<string>();

        /// <summary>
        /// It´s a collection of all filters that can be used.
        /// </summary>
        private static List<IEffectFilter> _filterPallete = new List<IEffectFilter>();

		/// <summary>
		/// It´s a collection of filter to apply
		/// </summary>
		private List<IEffectFilter> _filterBufferList = new List<IEffectFilter>();

        /// <summary>
        /// It´s a original picture
        /// </summary>
        private Picture _picture;

		/// <summary>
		/// Load the filter pallete
		/// </summary>
		public static void Load()
        {
            FilterCollection._filterPallete.Clear();
            FilterCollection._loadErrorList.Clear();

            List<Type> typeList = new List<Type>();

            // Adding the built-in plugins
            typeList.AddRange(
                typeof(FilterCollection).Assembly.GetTypes().Where(t => typeof(IEffectFilter).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));

            // Getting the third-party plugins
            foreach (var fileName in Directory.GetFiles(@"ThirdPartyEffectModulePlugin"))
            {
                if (!fileName.EndsWith("dll"))
                {
                    continue;
                }

                String fullFileName =
                    Path.Combine(
                        Path.GetDirectoryName(typeof(FilterCollection).Assembly.Location),
                        fileName);

                typeList.AddRange(
                        Assembly.LoadFile(fullFileName).GetTypes().Where(t => typeof(IEffectFilter).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract));
            }

            // Creating objects
            foreach (var type in typeList)
            {
                try
                {
                    FilterCollection._filterPallete.Add(
                        (IEffectFilter)Activator.CreateInstance(type));
                }
                catch (Exception ex)
                {
                    FilterCollection._loadErrorList.Add(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Gets the list pallete
        /// </summary>
        /// <returns></returns>
        public static List<IEffectFilter> GetFilterPallete() {
            return _filterPallete;
		}

		/// <summary>
		/// Gets a list of filter list
		/// </summary>
		/// <returns></returns>
		public List<IEffectFilter> GetFilterBufferList() {
			return this._filterBufferList;
		}

		/// <summary>
		/// Registers a filter to work.
		/// </summary>
		/// <param name="filter">A filter to apply</param>
		/// <param name="filterOrderIndex">An index of filter to apply the filter.</param>
		public void Register(IEffectFilter filter, int filterOrderIndex)
        {
            #region Entries validation

            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            #endregion

            if (this._filterBufferList.Count == 0)
            {
                this._filterBufferList.Add(filter);
            }
            else
            {
                this._filterBufferList.Insert(filterOrderIndex, filter);
            }
        }

        /// <summary>
        /// Ups the filter in list order
        /// </summary>
        /// <param name="filter">An instance of filter</param>
        public void UpOrder(IEffectFilter filter)
        {
            #region Entries validation

            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (this._filterBufferList.Count == 0)
            {
                throw new InvalidOperationException("Some filter should be registered before change order");
            }

            #endregion

            int oldIndex = -1;
            for (int i = 0; i < this._filterBufferList.Count; i++)
            {
                if (this._filterBufferList[i] == filter)
                {
                    oldIndex = i;
                    break;
                }
            }

            if (oldIndex == -1)
            {
                throw new InvalidOperationException("Filter should be registered before change order");
            }
            if (oldIndex == 0)
            {
                return;
            }

            this._filterBufferList.RemoveAt(oldIndex);
            this._filterBufferList.Insert(oldIndex - 1, filter);
        }

        /// <summary>
        /// Downs the filter in list order
        /// </summary>
        /// <param name="filter">An instance of filter</param>
        public void DownOrder(IEffectFilter filter)
        {
            #region Entries validation

            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            if (this._filterBufferList.Count == 0)
            {
                throw new InvalidOperationException("Some filter should be registered before change order");
            }

            #endregion

            int oldIndex = -1;
            for (int i = 0; i < this._filterBufferList.Count; i++)
            {
                if (this._filterBufferList[i] == filter)
                {
                    oldIndex = i;
                    break;
                }
            }

            if (oldIndex == -1)
            {
                throw new InvalidOperationException("Filter should be registered before change order");
            }
            if (oldIndex + 1 == this._filterBufferList.Count)
            {
                return;
            }

            this._filterBufferList.RemoveAt(oldIndex);
            this._filterBufferList.Insert(oldIndex + 1, filter);
        }

        /// <summary>
        /// Registers a filter to work.
        /// </summary>
        /// <param name="filter">A filter to apply</param>
        public void Register(IEffectFilter filter)
        {
            this.Register(filter, 0);
		}

		/// <summary>
		/// Removes the filter
		/// </summary>
		/// <param name="filter" />		
        public void UnRegister(IEffectFilter filter)
        {
            #region Entries validation

            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            #endregion

            this._filterBufferList.Remove(filter);
		}

		/// <summary>
		/// Applies all the filter buffer list in order
		/// </summary>
		/// <param name="picture"></param>
        /// <param name="frameIndex">It´s the frame index in an animation</param>
		/// <returns></returns>
		public void Apply(Picture picture, int frameIndex)
        {
            #region Entries validation

            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }

            #endregion

            this._picture = picture;

            for (int i = 0; i < this._filterBufferList.Count; i++)
            {
                var filter = this._filterBufferList[i];

                if(filter.ApplyFilter(this._picture, frameIndex))
                {
                    String baseFolder =
                        Path.GetDirectoryName(
                            this._picture.FullPath);

                    String baseFile =
                        Path.GetFileName(
                            this._picture.FullPath);

                    String folder =
                        Path.Combine(baseFolder, "filtered");

                    String file =
                        Path.Combine(
                            folder,
                            Path.GetFileNameWithoutExtension(baseFile) + "." + frameIndex.ToString() + ".filtered." + Path.GetExtension(baseFile));

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    this._picture.SaveCopy(file);
                }
            }
        }

        /// <summary>
        /// Gets error list eventually occurred during the Load() method.
        /// </summary>
        /// <returns></returns>
        public static List<String> GetLoadErrorList()
        {
            return FilterCollection._loadErrorList;
        }
    }
}