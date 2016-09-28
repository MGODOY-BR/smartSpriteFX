
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Animations{
	/// <summary>
	/// Represents a iterator to travel through the pictures.
	/// </summary>
	public class FrameIterator {

		/// <summary>
		/// Represents a iterator to travel through the pictures.
		/// </summary>
		public FrameIterator() {
		}

		/// <summary>
		/// It´s the index of frame
		/// </summary>
		private int _frameIndex;

		/// <summary>
		/// It´s the current picture
		/// </summary>
		private Picture _current;

		/// <summary>
		/// Gets the curent picture
		/// </summary>
		/// <returns></returns>
		public Picture GetCurrent() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Goes to forward frame
		/// </summary>
		/// <returns></returns>
		public boolean Next() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Goes back to first frame
		/// </summary>
		/// <returns></returns>
		public void MoveFirst() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Goes to last frame
		/// </summary>
		/// <returns></returns>
		public boolean MoveLast() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Closes ans release the object
		/// </summary>
		public void Close() {
			// TODO implement here
		}

		/// <summary>
		/// Moves the current cursor to the selected frame
		/// </summary>
		/// <param name="frameIndex">		public void MoveTo(int frameIndex) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Gets a frameIterator from path
		/// </summary>
		/// <param name="fullPath">A path of a file or directory</param>
		/// <returns></returns>
		static FrameIterator Open(String fullPath) {
			// TODO implement here
			return null;
		}

	}
}