
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// Defines how an effect filter must be.
	/// </summary>
	public interface IEffectFilter {

		/// <summary>
		/// Changes the frame
		/// </summary>
		/// <param name="frame">It´s the picture</param>
		/// <param name="index">It´s the index of frame</param>
		/// <summary>
		/// @return An indicator that indicates if the frame is considered or ignored. Use it for skip frames in animation edition. True - considered, False - ignored.
		/// </summary>
		public bool ApplyFilter(Picture frame, int index);

		/// <summary>
		/// Gets the identification of component
		/// </summary>
		/// <returns></returns>
		public Identification GetIdentification();

		/// <summary>
		/// Opens the configuration panel
		/// </summary>
		/// <returns></returns>
		public IConfigurationPanel ShowConfigurationPanel();

		/// <summary>
		/// Reset the filter to original values
		/// </summary>
		public void Reset();

		/// <summary>
		/// Gets the execution of current execution of apply method.
		/// </summary>
		/// <returns></returns>
		public float GetCurrentPercentageExecution();

	}
}