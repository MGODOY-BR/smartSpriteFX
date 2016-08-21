
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Animations{
	/// <summary>
	/// Represents a list of pictures to form a animation
	/// </summary>
	public class Animation {

		/// <summary>
		/// Represents a list of pictures to form a animation
		/// </summary>
		public Animation() {
		}

		/// <summary>
		/// It´s the name of animation
		/// </summary>
		public String Name;

		/// <summary>
		/// 
		/// </summary>
		public HashSet<Picture> FrameSet;

		/// <summary>
		/// Loads a folder such as a picture list
		/// </summary>
		/// <param name="folderPath">		public void Load(String folderPath) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Removes a frame
		/// </summary>
		/// <param name="frame">		public void Remove(Picture frame) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Remove a range of frames from frameSet
		/// </summary>
		/// <param name="frameList">		public void RemoveRange(Picture frameList) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Releases the frames of animation
		/// </summary>
		public void Release() {
			// TODO implement here
		}

		/// <summary>
		/// Adds a frame in frameSet
		/// </summary>
		/// <param name="frame">		public void Increase(Picture frame) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Inserts a frame in frameSet
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="index">		public void Insert(Picture frame, int index) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Saves animations in new folder
		/// </summary>
		/// <param name="folderPath">		public void SaveAs(String folderPath) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Opens an animation based on a folder
		/// </summary>
		/// <param name="folderPath"></param>
		/// <returns></returns>
		public static Animation OpenAnimation(String folderPath) {
			// TODO implement here
			return null;
		}

	}
}