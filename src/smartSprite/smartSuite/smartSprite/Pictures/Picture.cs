
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents an image
	/// </summary>
    [Serializable]
	public class Picture {

        /// <summary>
        /// It´s a buffer of picture, where key it's a combination of x and y coordinates. 
        /// </summary>
        [NonSerialized]
        private Dictionary<String, Color> _buffer;

        /// <summary>
        /// Creates an image
        /// </summary>
        public Picture(String fullPath)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fullPath))
            {
                throw new ArgumentNullException("fullPath");
            }

            #endregion

            this._fullPath = fullPath;

            // Loading the bit array
            this.LoadBuffer(fullPath);
        }

        /// <summary>
        /// It´s the fullname of picture
        /// </summary>
        private String _fullPath;

		/// <summary>
		/// It's the last clicked point
		/// </summary>
		public Point LastPoint
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the fullpath of image
        /// </summary>
        public string FullPath
        {
            get
            {
                return _fullPath;
            }
        }

        /// <summary>
        /// Loads the buffer of image
        /// </summary>
        /// <param name="fullPath"></param>
        private void LoadBuffer(string fullPath)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fullPath))
            {
                throw new ArgumentNullException("fullPath");
            }

            #endregion

            Image image = Image.FromFile(fullPath);
            this.LoadBuffer((Bitmap)image);
        }

        /// <summary>
        /// Load the buffer
        /// </summary>
        private void LoadBuffer(Bitmap image)
        {
            #region Entries validation

            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            #endregion

            lock (this)
            {
                #region Entries validation

                if (this._buffer != null)
                {
                    return;
                }

                #endregion

                this._buffer = new Dictionary<string, Color>();

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        this._buffer.Add(
                            this.FormatKey(x, y),
                            image.GetPixel(x, y));
                    }
                }
            }
        }

        /// <summary>
        /// Formats the coordinates to use as keys.
        /// </summary>
        private string FormatKey(int x, int y)
        {
            return String.Format("{0}_{1}", x, y);
        }

        /// <summary>
        /// Gets a pixel from coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            var key = this.FormatKey(x, y);

            if (String.IsNullOrEmpty(this._fullPath))
            {
                throw new ArgumentNullException("this._fullPath");
            }
            if (this._buffer == null)
            {
                this.LoadBuffer(this._fullPath);
            }
            if (!this._buffer.ContainsKey(key))
            {
                throw new IndexOutOfRangeException("Coordinates out of range of picture.");
            }

            Color pixel = this._buffer[key];
            return pixel;
        }

        public void Dispose()
        {
            if (this._buffer != null)
            {
                this._buffer.Clear();
            }
        }

    }
}