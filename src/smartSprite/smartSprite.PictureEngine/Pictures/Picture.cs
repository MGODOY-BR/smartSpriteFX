
using smartSprite.Pictures.ColorPattern;
using smartSprite.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents an image
	/// </summary>
    [Serializable]
	public class Picture {

        /// <summary>
        /// Represents the width of picture
        /// </summary>
        private int _width;

        /// <summary>
        /// Represents whe height of picture
        /// </summary>
        private int _height;

        /// <summary>
        /// It´s a cache of picture
        /// </summary>
        private static Dictionary<String, Picture> _pictureCache = new Dictionary<String, Picture>();

        /// <summary>
        /// It´s a buffer of picture, where key it's a combination of x and y coordinates. 
        /// </summary>
        [NonSerialized]
        private Dictionary<String, ColorInfo> _buffer;

        /// <summary>
        /// It´s a cache of color info
        /// </summary>
        private static Dictionary<int, ColorInfo> _colorInfoBuffer = new Dictionary<int, ColorInfo>();

        /// <summary>
        /// Creates an image
        /// </summary>
        private Picture(String fullPath)
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

            using (var bitmap = new Bitmap(fullPath))
            {
                this._height = bitmap.Height;
                this._width = bitmap.Width;

                this.LoadBuffer(bitmap);
            }
        }

        public Picture Clone()
        {
            throw new NotImplementedException();
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

            lock (_colorInfoBuffer)
            {
                #region Entries validation

                if (this._buffer != null)
                {
                    return;
                }

                #endregion

                this._buffer = new Dictionary<string, ColorInfo>();

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        var colorInfo =
                            new ColorInfo(
                                image.GetPixel(x, y));

                        var colorInfoKey = 
                            colorInfo.GetInnerColor().ToArgb();

                        // Save color info
                        if (!_colorInfoBuffer.ContainsKey(colorInfoKey))
                        {
                            _colorInfoBuffer.Add(colorInfoKey, colorInfo);
                        }

                        // Save private picture
                        this._buffer.Add(
                            this.FormatKey(x, y),
                            _colorInfoBuffer[colorInfoKey]);
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

            Color pixel = this._buffer[key].GetInnerColor();
            return pixel;
        }

        /// <summary>
        /// Replaces a pixel in the buffer
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="newColor" />		
        public void ReplacePixel(int x, int y, Color newColor)
        {
            #region Entries validation

            if (newColor == null)
            {
                throw new ArgumentNullException("newColor");
            }
            if (this._buffer == null || this._buffer.Count == 0)
            {
                throw new ArgumentNullException("Empty buffer!!!");
            }

            #endregion

            var key = this.FormatKey(x, y);

            lock (_colorInfoBuffer)
            {
                var colorInfo = new ColorInfo(newColor);
                colorInfo = _colorInfoBuffer[colorInfo.GetInnerColor().ToArgb()];

                var colorInfoKey =
                    colorInfo.GetInnerColor().ToArgb();

                if (!this._buffer.ContainsKey(key))
                {
                    this._buffer.Add(key, colorInfo);
                }
                else
                {
                    this._buffer[key] = colorInfo;
                }
            }
        }

        /// <summary>
        /// Overwrites the picture
        /// </summary>
        public void Overwrite(Color transparentColor)
        {
            #region Entries validation

            if (this._width <= 0)
            {
                throw new ArgumentOutOfRangeException("Width not setted");
            }
            if (this._height <= 0)
            {
                throw new ArgumentOutOfRangeException("Height not setted");
            }

            #endregion

            ColorEqualityComparer colorComparer = new ColorEqualityComparer();

            using (var pieceBitmap = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb))
            {
                for (int y = 0; y < this._height; y++)
                {
                    for (int x = 0; x < this._width; x++)
                    {
                        var piecePixel = this.GetPixel(x, y);

                        if (colorComparer.LooksLike(piecePixel, transparentColor))
                        {
                            piecePixel = transparentColor;
                        }

                        pieceBitmap.SetPixel(x, y, piecePixel);
                    }

                    // StaminaUtil.GetRestSometimes();
                }

                // Overwriting piece bitmap
                pieceBitmap.MakeTransparent(transparentColor);
                pieceBitmap.Save(this._fullPath, ImageFormat.Png);
            }
        }

        public void Dispose()
        {
            if (this._buffer != null)
            {
                // this._buffer.Clear();
            }
        }

        public override string ToString()
        {
            return this._fullPath;
        }

        public override bool Equals(object obj)
        {
            #region Entries validation

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (String.IsNullOrEmpty(this._fullPath))
            {
                throw new ArgumentNullException("this._fullPath");
            }
            
            #endregion

            return this._fullPath.Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            #region Entries validation

            if (String.IsNullOrEmpty(this._fullPath))
            {
                throw new ArgumentNullException("this._fullPath");
            }

            #endregion

            return this._fullPath.GetHashCode();
        }

        /// <summary>
        /// Clears the picture cache
        /// </summary>
        public static void ClearCache()
        {
            lock (Picture._pictureCache)
            {
                Picture._pictureCache.Clear();
            }
        }

        /// <summary>
        /// Gets a instance of picture
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public static Picture GetInstance(String fullFileName)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fullFileName))
            {
                throw new ArgumentNullException("fullFileName");
            }

            #endregion

            lock (Picture._pictureCache)
            {
                if (!Picture._pictureCache.ContainsKey(fullFileName))
                {
                    Picture._pictureCache.Add(fullFileName, new Picture(fullFileName));
                }

                return Picture._pictureCache[fullFileName];
            }
        }
    }
}