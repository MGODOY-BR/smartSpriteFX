
using smartSuite.smartSpriteFX.SpriteEffectModule.Animations;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace smartSuite.smartSpriteFX.Animations{
	/// <summary>
	/// Represents a iterator to travel through the pictures.
	/// </summary>
	public class FrameIterator {

		/// <summary>
		/// It´s the index of frame
		/// </summary>
		private int _frameIndex = -1;

		/// <summary>
		/// It´s the current picture
		/// </summary>
		private Picture _current = null;

        /// <summary>
        /// It´s the containt of files
        /// </summary>
        private List<String> _fileList = new List<string>();

        /// <summary>
        /// It´s a comparer used to sort files
        /// </summary>
        private static AnimationComparer _animationComparer = new AnimationComparer();

        /// <summary>
        /// Creates a instance of the object.
        /// </summary>
        /// <remarks>This constructor was created to achieve designs goal. You shouldn't use it on your code.</remarks>
        private FrameIterator()
        {
        }

        /// <summary>
        /// Gets the current frame index
        /// </summary>
        /// <returns></returns>
        internal int GetFrameIndex()
        {
            return this._frameIndex;
        }

        /// <summary>
        /// Gets the file list discovered through Open method
        /// </summary>
        /// <returns></returns>
        internal List<String> GetFileList()
        {
            return this._fileList;
        }

        /// <summary>
        /// Gets the curent picture
        /// </summary>
        /// <returns></returns>
        public Picture GetCurrent()
        {
            this.LoadCurrentPicture();
            return this._current;
		}

		/// <summary>
		/// Goes to forward frame
		/// </summary>
		/// <returns></returns>
		public Boolean Next()
        {
            this._frameIndex++;

            #region Entries validation

            if (this._fileList.Count <= this._frameIndex)
            {
                this._frameIndex--;
                return false;
            }

            #endregion

            return true;
        }

        /// <summary>
        /// Goes back to first frame
        /// </summary>
        /// <returns></returns>
        public void MoveFirst()
        {
            this._frameIndex = 0;
		}

        /// <summary>
        /// Goes back to beginning conditions
        /// </summary>
        public void Reset()
        {
            this._frameIndex = -1;
            this._current = null;
        }

		/// <summary>
		/// Goes to last frame
		/// </summary>
		/// <returns></returns>
		public Boolean MoveLast()
        {
            #region Entries validation

            if (this._fileList.Count == 0)
            {
                return false;
            }

            #endregion

            this._frameIndex = this._fileList.Count - 1;
			return true;
		}

		/// <summary>
		/// Closes and release the object
		/// </summary>
		public void Close()
        {
            this._fileList.Clear();
            if (this._current != null)
            {
                this._current.ReleaseBuffer();
                this._current = null;
            }
            this._frameIndex = -1;
		}

        /// <summary>
        /// Moves the current cursor to the selected frame
        /// </summary>
        /// <param name="frameIndex"></param>
        public void MoveTo(int frameIndex)
        {
            #region Entries validation

            if (frameIndex < 0 || frameIndex > this._fileList.Count)
            {
                throw new ArgumentOutOfRangeException("frameIndex", frameIndex, "frameIndex should be greater then 0 and lower then " + this._fileList.Count);
            }

            #endregion

            this._frameIndex = frameIndex;
        }

        /// <summary>
        /// Gets a frameIterator it has contained just the selected frames
        /// </summary>
        /// <param name="frameList">The selected frames</param>
        /// <returns></returns>
        internal static FrameIterator Open(params Picture[] frameList)
        {
            #region Entries validation

            if (frameList == null)
            {
                throw new ArgumentNullException("frameList");
            }

            #endregion

            FrameIterator iterator = new FrameIterator();
            foreach (var frameItem in frameList)
            {
                #region Entries validation

                if (frameItem == null)
                {
                    throw new ArgumentNullException("frameItem");
                }
                if (String.IsNullOrEmpty(frameItem.FullPath))
                {
                    throw new ArgumentNullException("frameItem.FullPath");
                }

                #endregion

                iterator._fileList.Add(frameItem.FullPath);
            }
            return iterator;
        }

        /// <summary>
        /// Gets a frameIterator from path
        /// </summary>
        /// <param name="fullPath">A path of a file or directory</param>
        /// <returns></returns>
        internal static FrameIterator Open(String fullPath)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fullPath))
            {
                throw new ArgumentNullException("fullPath");
            }
            if (!Directory.Exists(fullPath))
            {
                throw new DirectoryNotFoundException(
                    "There is no folder called [" + fullPath + "].");
            }
            #endregion

            FrameIterator iterator = new FrameIterator();

            FrameIterator.FillFileList(fullPath, iterator._fileList);

            #region Entries validation

            if (iterator._fileList.Count == 0)
            {
                throw new FileNotFoundException(
                    "There´s no supported file found on [" + fullPath + "] (supported files are: PNG, BMP, JPG/JPEG).");
            }

            #endregion

            return iterator;
        }

        /// <summary>
        /// Gets a frameIterator from picture list
        /// </summary>
        /// <param name="pictureList"></param>
        /// <returns></returns>
        internal static FrameIterator Open(List<Picture> pictureList)
        {
            #region Entries validation

            if (pictureList == null)
            {
                throw new ArgumentNullException("pictureList");
            }

            #endregion

            FrameIterator iterator = new FrameIterator();

            foreach (var pictureItem in pictureList)
            {
                iterator._fileList.Add(pictureItem.FullPath);
            }

            return iterator;
        }

        /// <summary>
        /// Gets the total amount of frames
        /// </summary>
        /// <returns></returns>
        public int CountFrames()
        {
            #region Entries validation

            if (this._fileList == null)
            {
                throw new ArgumentNullException("The object hasn't initialized yet.");
            }

            #endregion

            return this._fileList.Count;
        }

        /// <summary>
        /// Fills the file list
        /// </summary>
        private static void FillFileList(string fullPath, List<string> fileList)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fullPath))
            {
                throw new ArgumentNullException("fullPath");
            }
            if (fileList == null)
            {
                throw new ArgumentNullException("fileList");
            }

            #endregion

            List<string> result = GetFileList(fullPath);

            fileList.AddRange(result);
        }

        /// <summary>
        /// Gets the supporte image types
        /// </summary>
        /// <returns></returns>
        internal static String[] GetSupportedImages()
        {
            return new String[4]
            {
                "*.png",
                "*.bmp",
                "*.jpg",
                "*.jpeg"
            };
        }

        /// <summary>
        /// Gets the file list
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        internal static List<string> GetFileList(string fullPath)
        {
            List<string> result = new List<string>();

            foreach (var extension in GetSupportedImages())
            {
                result.AddRange(
                    Directory.GetFiles(fullPath, extension, SearchOption.TopDirectoryOnly));
            }

            result.Sort(_animationComparer);

            return result;
        }

        /// <summary>
        /// Sets the current picture.
        /// </summary>
        /// <param name="picture"></param>
        private void SetCurrent(Picture picture)
        {
            #region Entries validation

            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }

            #endregion
            this._current = picture;
        }

        /// <summary>
        /// Loads the current picture
        /// </summary>
        private void LoadCurrentPicture()
        {
            #region Entries validation

            if (this._fileList.Count == 0)
            {
                throw new InvalidOperationException("Empty picture cache");
            }
            if (this._frameIndex == -1)
            {
                throw new InvalidOperationException("Cursor is empty. Call MoveFirst or MoveNext before invoke this operation");
            }
            var newFilePath = this._fileList[this._frameIndex];
            if (this._current != null)
            {
                if (newFilePath.Equals(this._current.FullPath))
                {
                    return;
                }
                else
                {
                    this._current.ReleaseBuffer();
                }
            }

            #endregion

            this.SetCurrent(
                Picture.GetInstance(newFilePath));
        }

    }
}