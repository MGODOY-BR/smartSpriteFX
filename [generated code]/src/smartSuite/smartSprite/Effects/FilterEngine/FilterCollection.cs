
using System;
using System.Collections.Generic;
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
		private static HashSet<IEffectFilter> _filterPallete;

		/// <summary>
		/// It´s a collection of filter to apply
		/// </summary>
		private List<IEffectFilter> _filterBufferList;

		/// <summary>
		/// 
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
		public static HashSet<IEffectFilter> GetFilterPallete() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Gets a list of filter list
		/// </summary>
		/// <returns></returns>
		public HashSet<IEffectFilter> GetFilterBufferList() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Registers a filter to work.
		/// </summary>
		/// <param name="filter">A filter to apply</param>
		/// <param name="filterOrderIndex">An index of filter to apply the filter.</param>
		public void Register(IEffectFilter filter, void filterOrderIndex) {
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
		/// <param name="filter">		public void UnRegister(IEffectFilter filter) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Applies all the filter buffer list in order
		/// </summary>
		/// <param name="picture"></param>
		/// <param name="frameIndex">It´s the index of frame in animation</param>
		/// <returns></returns>
		public void Apply(Picture picture, int frameIndex) {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Gets error list eventually occurred during the Load() method.
		/// </summary>
		/// <returns></returns>
		public static HashSet<String> GetLoadErrorList() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Ups the filter in list order
		/// </summary>
		/// <param name="filter">An instance of filter</param>
		public void UpOrder(IEffectFilter filter) {
			// TODO implement here
		}

		/// <summary>
		/// Downs the filter in list order
		/// </summary>
		/// <param name="filter">An instance of filter</param>
		public void DownOrder(IEffectFilter filter) {
			// TODO implement here
		}

	}
}