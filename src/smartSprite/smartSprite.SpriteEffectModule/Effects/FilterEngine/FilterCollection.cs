
using smartSuite.smartSprite.Effects.Filters;
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.FilterEngine{
	/// <summary>
	/// It´s a collection of filters
	/// </summary>
	public class FilterCollection {

		/// <summary>
		/// It´s a collection of filters
		/// </summary>
		public FilterCollection() {
		}

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
		public static void Load() {
			// TODO implement here
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
			// TODO implement here
		}

		/// <summary>
		/// Registers a filter to work.
		/// </summary>
		/// <param name="filter">A filter to apply</param>
		public void Register(IEffectFilter filter) {
			// TODO implement here
		}

		/// <summary>
		/// Removes the filter
		/// </summary>
		/// <param name="filter" />		
        public void UnRegister(IEffectFilter filter) {
			// TODO implement here
		}

		/// <summary>
		/// Applies all the filter buffer list in order and returns a new frame
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

	}
}