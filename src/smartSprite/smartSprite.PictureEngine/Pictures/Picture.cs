
using smartSprite.PictureEngine.Pictures.ColorPattern;
using smartSprite.Pictures.ColorPattern;
using smartSprite.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        /// It´s the transparent color
        /// </summary>
        private Color _transparentColor = Color.Transparent;

        /// <summary>
        /// It´s a cache of pictures
        /// </summary>
        private static Dictionary<String, Picture> _pictureCache = new Dictionary<String, Picture>();

        /// <summary>
        /// It´s a buffer of colors, where key it's a combination of x and y coordinates. 
        /// </summary>
        [NonSerialized]
        private Dictionary<String, ColorInfo> _buffer;

        /// <summary>
        /// It´s a cache of colors, used to save memory
        /// </summary>
        private static Dictionary<int, ColorInfo> _colorInfoBuffer = new Dictionary<int, ColorInfo>();

        /// <summary>
        /// Gets the amount of color of current picture
        /// </summary>
        public long ColorCount
        {
            get;
            private set;
        }

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
        /// Merge the buffer of another picture
        /// </summary>
        /// <param name="other"></param>
        internal void Merge(Picture other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            #endregion

            if (other._transparentColor != Color.Transparent)
            {
                this._transparentColor = other._transparentColor;
            }

            HashSet<int> colorSet = new HashSet<int>();
            for (int y = 0; y < other.Height; y++)
            {
                for (int x = 0; x < other.Width; x++)
                {
                    var color = other.GetPixel(x, y);
                    this.ReplacePixel(x, y, color);

                    var argb = color.ToArgb();
                    if (!colorSet.Contains(argb))
                    {
                        colorSet.Add(argb);
                    }
                }
            }

            this.ColorCount = colorSet.LongCount();
        }

        /// <summary>
        /// Replaces the color for a new one.
        /// </summary>
        /// <param name="oldColor"></param>
        /// <param name="newColor"></param>
        public void ReplaceColor(Color oldColor, Color newColor)
        {
            foreach (var item in this._buffer)
            {
                if (item.Value.GetInnerColor().ToArgb() == oldColor.ToArgb())
                {
                    item.Value.SetInnerColor(newColor);
                }
            }
        }

        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        /// <remarks>
        /// This constructor was intented to be used internally only.
        /// </remarks>
        internal Picture()
        {
            this._buffer = new Dictionary<string, ColorInfo>();
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
        /// Gets the width of picture
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// Gets the height of picture
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Sets or gets the transparent color
        /// </summary>
        public Color TransparentColor
        {
            get
            {
                return _transparentColor;
            }
            internal set
            {
                this._transparentColor = value;
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
            if (!File.Exists(fullPath))
            {
                throw new ArgumentNullException("File [" + fullPath + "] not found");
            }

            #endregion

            using (var bitmap = new Bitmap(fullPath))
            {
                this._height = bitmap.Height;
                this._width = bitmap.Width;

                this.LoadBuffer(bitmap);
            }
        }

        /// <summary>
        /// Load the buffer
        /// </summary>
        internal void LoadBuffer(Bitmap image)
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
                HashSet<int> colorSet = new HashSet<int>();

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        var colorInfo =
                            new ColorInfo(
                                image.GetPixel(x, y));

                        var argb = colorInfo.GetInnerColor().ToArgb();
                        if (!colorSet.Contains(argb))
                        {
                            colorSet.Add(argb);
                        }

                        this.LoadColorInfoCache(y, x, colorInfo);
                    }
                }

                this.ColorCount = colorSet.LongCount();
            }
        }

        /// <summary>
        /// Loads the color info cache
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="colorInfo"></param>
        private void LoadColorInfoCache(int y, int x, ColorInfo colorInfo)
        {
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

        /// <summary>
        /// Formats the coordinates to use as keys.
        /// </summary>
        private string FormatKey(int x, int y)
        {
            return String.Format("{0}_{1}", x, y);
        }

        /// <summary>
        /// Converts key used on cache to point
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Point ToPoint(String key)
        {
            #region Entries validation
            
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            #endregion

            var keyComponents = key.Split('_');
            return new Point(float.Parse(keyComponents[0]), float.Parse(keyComponents[1]));
        }

        /// <summary>
        /// Creates and returns a copy of the object
        /// </summary>
        /// <returns></returns>
        internal Picture Clone()
        {
            Picture clone = new Picture();

            // Copying the buffer
            foreach (var bufferItem in this._buffer)
            {
                clone._buffer.Add(
                    bufferItem.Key, bufferItem.Value.Clone());
            }

            // Copying another attributes
            clone._height = this._height;
            clone._width = this._width;
            clone.ColorCount = this.ColorCount;
            clone._transparentColor = this._transparentColor;

            return clone;
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
        /// Finds a list of points for the color list
        /// </summary>
        /// <returns></returns>
        public List<Point> Find(params Color[] colorList)
        {
            #region Entries validation

            if (colorList == null)
            {
                throw new ArgumentNullException("colorList");
            }
            if (colorList.Length == 0)
            {
                throw new ArgumentOutOfRangeException("It´s needed one color at least");
            }
            if (this._buffer == null)
            {
                return new List<Point>();
            }

            #endregion

            List<Point> returnList = new List<Point>();
            var colorCacheList = this._buffer;
            foreach (KeyValuePair<String, ColorInfo> colorCacheItem in colorCacheList)
            {
                foreach (var findColorItem in colorList)
                {
                    ColorInfo findColorInfo = new ColorInfo(findColorItem);

                    if (findColorInfo.Equals(colorCacheItem.Value))
                    {
                        returnList.Add(
                            this.ToPoint(colorCacheItem.Key));
                    }
                }
            }

            return returnList;
        }

        /// <summary>
        /// Gets the point of image considered borderers
        /// </summary>
        /// <returns></returns>
        public List<Point> ListBorder()
        {
            #region Entries validation

            if (this._buffer == null)
            {
                return new List<Point>();
            }

            #endregion

            List<Point> returnList = new List<Point>();

            foreach (var bufferItem in this._buffer)
            {
                var pointItem = this.ToPoint(bufferItem.Key);

                #region Entries validation

                // The point can't be transparent
                if (bufferItem.Value.GetInnerColor().ToArgb().Equals(this._transparentColor.ToArgb()))
                {
                    continue;
                }

                #endregion

                string keyLeft = this.FormatKey((int)pointItem.X - 1, (int)pointItem.Y);
                string keyRight = this.FormatKey((int)pointItem.X + 1, (int)pointItem.Y);
                string keyTop = this.FormatKey((int)pointItem.X, (int)pointItem.Y - 1);
                string keyBottom = this.FormatKey((int)pointItem.X, (int)pointItem.Y + 1);

                string keyCornerTopLeft = this.FormatKey((int)pointItem.X - 1, (int)pointItem.Y - 1);
                string keyCornerTopRight = this.FormatKey((int)pointItem.X + 1, (int)pointItem.Y - 1);
                string keyCornerBottomLeft = this.FormatKey((int)pointItem.X - 1, (int)pointItem.Y + 1);
                string keyCornerBottomRight = this.FormatKey((int)pointItem.X + 1, (int)pointItem.Y + 1);

                String[] keyArray =
                    new String[8]{
                            keyLeft,
                            keyRight,
                            keyTop,
                            keyBottom,
                            keyCornerTopLeft,
                            keyCornerTopRight,
                            keyCornerBottomLeft,
                            keyCornerBottomRight
                        };

                foreach (var keyItem in keyArray)
                {
                    #region Entries validation

                    if (!this._buffer.ContainsKey(keyItem))
                    {
                        continue;
                    }

                    #endregion

                    var colorItem = this._buffer[keyItem];

                    if (colorItem.GetInnerColor().ToArgb().Equals(this._transparentColor.ToArgb()))
                    {
                        returnList.Add(pointItem);
                    }
                }
            }

            return returnList;
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

            #endregion

            var key = this.FormatKey(x, y);

            lock (_colorInfoBuffer)
            {
                var colorInfo = new ColorInfo(newColor);
                var keyArgb = colorInfo.GetInnerColor().ToArgb();

                if (!_colorInfoBuffer.ContainsKey(keyArgb))
                {
                    _colorInfoBuffer.Add(keyArgb, colorInfo);
                }
                colorInfo = _colorInfoBuffer[keyArgb];

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
        internal void Overwrite(Color transparentColor)
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

            this._transparentColor = transparentColor;

            this.Save(this._fullPath, transparentColor, new LookLikeColorComparer());
        }

        /// <summary>
        /// Saves the file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="transparentColor"></param>
        private void Save(string fileName, Color transparentColor, IEqualityComparer<Color> colorComparer)
        {
            #region Entries validation
            
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            if (colorComparer == null)
            {
                throw new ArgumentNullException("colorComparer");
            }

            #endregion

            using (var pieceBitmap = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb))
            {
                for (int y = 0; y < this._height; y++)
                {
                    for (int x = 0; x < this._width; x++)
                    {
                        var piecePixel = this.GetPixel(x, y);

                        if (colorComparer.Equals(piecePixel, transparentColor))
                        {
                            piecePixel = transparentColor;
                        }

                        pieceBitmap.SetPixel(x, y, piecePixel);
                    }
                }

                // Overwriting piece bitmap
                pieceBitmap.MakeTransparent(transparentColor);
                pieceBitmap.Save(fileName, ImageFormat.Png);
            }
        }

        #region Elements to manage of object

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

            return this._fullPath.Equals(((Picture)obj)._fullPath);
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

        #endregion

        /// <summary>
        /// Clears the picture cache
        /// </summary>
        public static void ClearCache()
        {
            lock (Picture._pictureCache)
            {
                Picture._colorInfoBuffer.Clear();
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

        /// <summary>
        /// Saves a copy of picture.
        /// </summary>
        /// <param name="copyFileName" />		
        internal void SaveCopy(String copyFileName)
        {
            #region Entries validation
            
            if (String.IsNullOrEmpty(copyFileName))
            {
                throw new ArgumentNullException("copyFileName");
            }

            #endregion

            this.Save(copyFileName, this._transparentColor, new ColorEqualityComparer());
        }
        
        /// <summary>
        /// Releases buffer
        /// </summary>
        internal void ReleaseBuffer()
        {
            this._buffer.Clear();
            this._transparentColor = Color.Transparent;
        }
    }
}