
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace smartSuite.smartSprite.Animations{
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
        /// Creats a instance of the object.
        /// </summary>
        /// <remarks>This constructor was created to achieve designs goal. You shouldn't use it on your code.</remarks>
        private FrameIterator()
        {
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

            if (this._fileList.Count < this._frameIndex)
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
		/// Closes ans release the object
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
		/// Gets a frameIterator from path
		/// </summary>
		/// <param name="fullPath">A path of a file or directory</param>
		/// <returns></returns>
		public static FrameIterator Open(String fullPath)
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

            iterator._fileList.AddRange(
                Directory.GetFiles(fullPath, "*.png", SearchOption.AllDirectories));
            iterator._fileList.AddRange(
                Directory.GetFiles(fullPath, "*.bmp", SearchOption.AllDirectories));
            iterator._fileList.AddRange(
                Directory.GetFiles(fullPath, "*.jpg", SearchOption.AllDirectories));
            iterator._fileList.AddRange(
                Directory.GetFiles(fullPath, "*.jpeg", SearchOption.AllDirectories));

            #region Entries validation

            if (iterator._fileList.Count == 0)
            {
                throw new FileNotFoundException(
                    "There´s no supported file found on [" + fullPath + "] (supported files are: PNG, BMP, JPG/JPEG).");
            }

            #endregion

            Regex regEx = new Regex(@"^(\d+)", RegexOptions.Compiled);

            // ordering the list, following the prefix number
            iterator._fileList.Sort(delegate (String path, String other)
            {
                if (regEx.IsMatch(path) && regEx.IsMatch(other))
                {
                    var pathRegEx = regEx.Match(path);
                    var otherRegEx = regEx.Match(other);

                    return
                        int.Parse(pathRegEx.Groups[2].Value)
                            .CompareTo(
                                int.Parse(otherRegEx.Groups[1].Value));
                }
                else
                {
                    return path.CompareTo(other);
                }
            });

            return iterator;
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

            this._current = Picture.GetInstance(newFilePath);
        }

    }
}