
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using smartSuite.smartSpriteFX.Pictures.PixelPatterns;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.BitmapMatters;

namespace smartSuite.smartSpriteFX.Pictures
{
    /// <summary>
    /// Represents an image
    /// </summary>
    [Serializable]
    public class Picture
    {
        /// <summary>
        /// Represents the width of picture
        /// </summary>
        private int _width;

        /// <summary>
        /// Represents whe height of picture
        /// </summary>
        private int _height;

        /// <summary>
        /// It's a filter used during the loading buffer process
        /// </summary>
        /// <seealso cref="LoadBuffer(Bitmap)"/>
        /// <seealso cref="LoadBuffer(string)"/>
        private IColorFilter _colorFilter;

        /// <summary>
        /// It's the algorithm buffer
        /// </summary>
        private ITraditionalAlgorithmBuffer _bufferAlgorithm;

        /// <summary>
        /// Gets the original width of picture with no filters applied
        /// </summary>
        public int OriginalWidth { get; private set; }

        /// <summary>
        /// Gets the original height of picture with no filters applied
        /// </summary>
        public int OriginalHeight { get; private set; }

        /// <summary>
        /// It´s the transparent color
        /// </summary>
        private Color? _transparentColor = null;

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
        private Picture(String fullPath, IColorFilter colorFilter)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fullPath))
            {
                throw new ArgumentNullException("fullPath");
            }
            if (colorFilter == null)
            {
                throw new ArgumentNullException("colorFilter");
            }

            #endregion

            this._fullPath = fullPath;
            this._colorFilter = colorFilter;

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

            try
            {
                this.BeginBatchUpdate();

                this.Buffer.Merge(other.Buffer);
                this.ColorCount = this.Buffer.CountColor();

                this.EndBatchUpdate();
            }
            catch
            {
                this.CancelBatchUpdate();
                throw;
            }
        }

        /// <summary>
        /// Gets all the pixels from Picture
        /// </summary>
        /// <returns></returns>
        public List<PointInfo> GetAllPixels()
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            return this.Buffer.SELECTALL();
        }

        /// <summary>
        /// Gets all the colors from Picture
        /// </summary>
        /// <returns></returns>
        public List<Color> GetAllColors()
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            return this.Buffer.GetAllColors();
        }

        /// <summary>
        /// Removes the pixel
        /// </summary>
        /// <returns>An indicator informing that the pixel has been excluded.</returns>
        public bool RemovePixel(int x, int y)
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            return this.Buffer.DELETE(x, y);
        }

        /// <summary>
        /// Gets the databases from another picture
        /// </summary>
        /// <param name="other"></param>
        public void ShareDataWithMe(Picture other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            #endregion

            this.Buffer = other.Buffer;
        }

        /// <summary>
        /// Replaces the color for a new one.
        /// </summary>
        /// <param name="oldColor"></param>
        /// <param name="newColor"></param>
        public void ReplaceColor(Color oldColor, Color newColor)
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            this.Buffer.UPDATE(oldColor, newColor);
        }

        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        /// <remarks>
        /// This constructor was intented to be used internally only.
        /// </remarks>
        internal Picture()
        {
            this.Buffer = PictureDatabase.Open();
            this._colorFilter = new NoneColorFilter();
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
            set
            {
                this._width = value;
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
            set
            {
                this._height = value;
            }
        }

        /// <summary>
        /// Sets or gets the transparent color
        /// </summary>
        public Color? TransparentColor
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
        /// Gets the buffer
        /// </summary>
        public IPictureDatabase Buffer { get; private set; }

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

            lock (fullPath)
            {
                using (var bitmap = new Bitmap(fullPath))
                {
                    this._height = bitmap.Height;
                    this._width = bitmap.Width;

                    this.OriginalHeight = this._height;
                    this.OriginalWidth = this._width;

                    this.LoadBuffer(bitmap);
                }
            }
        }

        /// <summary>
        /// Load the buffer
        /// </summary>
        internal void LoadBuffer(Bitmap image)
        {
            // if (this._bufferAlgorithm == null) this._bufferAlgorithm = new TraditionalAlgorithmBuffer(this.Buffer, image, this._colorFilter);
            if (this._bufferAlgorithm == null) this._bufferAlgorithm = new LockBufferAlgorithmBuffer(this.Buffer, image, this._colorFilter);

            this.Buffer = this._bufferAlgorithm.Buffer;
            this.ColorCount = this.Buffer.CountColor();
            this._height = image.Height;
            this._width = image.Width;
        }

        /// <summary>
        /// Loads the color info cache
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Save private picture
            this.Buffer.INSERT(x, y, color);
        }

        /// <summary>
        /// Loads the color info cache
        /// </summary>
        public void SetPixel(List<PointInfo> pointInfoList)
        {
            this.Buffer.INSERT(pointInfoList);
        }

        /// <summary>
        /// Draws a string in picture
        /// </summary>
        /// <param name="text"></param>
        public void DrawString(string text, float offSetX, float offSetY)
        {
            Font font = new Font("Arial", 12f);
            var sizeLetter = System.Windows.Forms.TextRenderer.MeasureText(text.ToString(), font);

            SolidBrush brush = new SolidBrush(Color.Black);

            using (Bitmap bmp = new Bitmap(sizeLetter.Width, sizeLetter.Height, PixelFormat.Format32bppArgb))
            {
                RectangleF rectf = new RectangleF(0, 0, sizeLetter.Width, sizeLetter.Height);

                Graphics graphics = Graphics.FromImage(bmp);

                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                SolidBrush backgroundBrush = new SolidBrush(Color.Yellow);

                graphics.FillRectangle(
                    backgroundBrush,
                    rectf);

                graphics.DrawString(text, font, brush, rectf);
                graphics.Flush();

                #region Updating the internal memory buffer

                var buffer = new PointInfo[sizeLetter.Width * sizeLetter.Height];
                int count = 0;
                for (int y = 0; y < sizeLetter.Height; y++)
                {
                    for (int x = 0; x < sizeLetter.Width; x++)
                    {
                        var pixel = bmp.GetPixel(x, y);

                        if(pixel != Color.Transparent && pixel.ToArgb() != backgroundBrush.Color.ToArgb())
                        {
                            int xx = x + (int)offSetX;
                            int yy = y + (int)offSetY;
                            buffer[count] = new PointInfo(xx, yy, pixel);
                            // this.SetPixel(x + (int)offSetX, y + (int)offSetY, pixel);
                        }
                        count++;
                    }
                }

                var bufferList = new List<PointInfo>(buffer);

                this.SetPixel(bufferList);

                #endregion
            }
        }

        /// <summary>
        /// Formats the coordinates to use as keys.
        /// </summary>
        public string FormatKey(int x, int y)
        {
            // return String.Format("{0}_{1}", x, y);
            return PictureDatabase.FormatKeyIndex(new Point((float)x, (float)y));
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

            var point = new Point();
            point.FillMe(key);

            return point;
        }

        /// <summary>
        /// Creates and returns a copy of the object
        /// </summary>
        /// <returns></returns>
        public Picture Clone()
        {
            return this.Clone(CloneMode.Full);
        }

        /// <summary>
        /// Creates and returns a copy of the object
        /// </summary>
        /// <param name="cloneMode">It´s the mode of clonage</param>
        /// <returns></returns>
        public Picture Clone(CloneMode cloneMode)
        {
            Picture clone = new Picture();

            if (cloneMode == CloneMode.Full)
            {
                clone.Buffer = this.Buffer.Clone();
            }

            // Copying another attributes
            clone._height = this._height;
            clone._width = this._width;
            clone.ColorCount = this.ColorCount;
            clone._transparentColor = this._transparentColor;
            clone._fullPath = this._fullPath;

            clone.OriginalWidth = this.OriginalWidth;
            clone.OriginalHeight = this.OriginalHeight;

            return clone;
        }

        /// <summary>
        /// Gets a pixel from coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color? GetPixel(int x, int y)
        {
            #region Entries validation

            long bufferSize = 0;

            if (this.Buffer != null)
            {
                bufferSize = this.Buffer.COUNT();
            }

            if (bufferSize == 0 && !String.IsNullOrWhiteSpace(this._fullPath))
            {
                this.LoadBuffer(this._fullPath);
            }
            else if (bufferSize == 0)
            {
                throw new ArgumentException("Empty picture.");
            }

            #endregion

            if (this.Buffer == null)
            {
                this.LoadBuffer(this._fullPath);
            }
            var item = this.Buffer.SELECT(x, y);
            if (item == null)
            {
                return null;
            }

            return item.GetInnerColor();
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
            if (this.Buffer == null)
            {
                return new List<Point>();
            }

            #endregion

            List<Point> returnList = new List<Point>();
            var colorCacheList = this.Buffer;
            foreach (var findColorItem in colorList)
            {
                returnList.AddRange(
                    this.Buffer.SELECT(findColorItem));
            }

            return returnList;
        }

        /// <summary>
        /// Gets the point of image considered border
        /// </summary>
        /// <returns></returns>
        public List<PointInfo> ListBorder()
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                return new List<PointInfo>();
            }

            #endregion

            List<PointInfo> returnList = new List<PointInfo>();
            foreach (var pointItem in this.Buffer.SELECTALL())
            {
                #region Entries validation

                // The point can't be transparent
                if (this._transparentColor != null)
                {
                    if (pointItem.Color.ToArgb().Equals(this._transparentColor.Value.ToArgb()))
                    {
                        continue;
                    }
                }

                #endregion

                Point pointLeft = new Point(pointItem.X - 1, pointItem.Y);
                Point pointRight = new Point(pointItem.X + 1, pointItem.Y);
                Point pointTop = new Point(pointItem.X, pointItem.Y - 1);
                Point pointBottom = new Point(pointItem.X, pointItem.Y + 1);

                Point pointCornerTopLeft = new Point(pointItem.X - 1, pointItem.Y - 1);
                Point pointCornerTopRight = new Point(pointItem.X + 1, pointItem.Y - 1);
                Point pointCornerBottomLeft = new Point(pointItem.X - 1, pointItem.Y + 1);
                Point pointCornerBottomRight = new Point(pointItem.X + 1, pointItem.Y + 1);

                Point[] pointArray =
                    new Point[8]{
                            pointLeft,
                            pointRight,
                            pointTop,
                            pointBottom,
                            pointCornerTopLeft,
                            pointCornerTopRight,
                            pointCornerBottomLeft,
                            pointCornerBottomRight
                        };

                foreach (var pointArrayItem in pointArray)
                {
                    var colorInfo = this.Buffer.SELECT((int)pointArrayItem.X, (int)pointArrayItem.Y);
                    if (colorInfo == null) continue;

                    Color colorPixel = colorInfo.GetInnerColor();

                    if(colorPixel.Equals(this._transparentColor))
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
            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            String key = this.FormatKey(x, y);
            lock (key)
            {
                var affectedPixel = this.Buffer.UPDATE(x, y, newColor);
                if (affectedPixel == 0)
                {
                    this.Buffer.INSERT(x, y, newColor);
                }
            }
        }

        /// <summary>
        /// Replaces pixels in PointRange
        /// </summary>
        /// <param name="pointRange"></param>
        public void ReplacePixel(PointRange pointRange)
        {
            #region Entries validation

            if (pointRange == null)
            {
                throw new ArgumentNullException("pointRange");
            }
            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            lock (this.Buffer)
            {
                var affectedRowList =
                        from rowItem in this.Buffer.SELECTALL()
                        where
                            rowItem.X >= pointRange.StartPoint.X &&
                            rowItem.X <= pointRange.StartPoint.X &&
                            rowItem.Y >= pointRange.EndPoint.Y &&
                            rowItem.Y <= pointRange.EndPoint.Y
                        select rowItem;

                try
                {
                    foreach (var affectedRowItem in affectedRowList)
                    {
                        lock (affectedRowItem.ToString())
                        {
                            affectedRowItem.Color = pointRange.Color;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Overwrites the picture
        /// </summary>
        internal void Overwrite(Color? transparentColor)
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
        private void Save(string fileName, Color? transparentColor, IEqualityComparer<Color> colorComparer)
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
            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            var firstPixelRef = this.GetPixel(0, 0);

            Color firstPixel;
            if (firstPixelRef.HasValue)
            {
                firstPixel = firstPixelRef.Value;
            }
            else
            {
                firstPixel = Color.Transparent;
            }

            var pixelList = this.Buffer.SELECTALL();

            using (var pieceBitmap = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb))
            {
                foreach (var pixelItem in pixelList)
                {
                    #region Entries validation

                    if (pixelItem == null)
                    {
                        continue;
                    }

                    #endregion

                    PointInfo pointInfo = pixelItem;
                    #region Entries validation

                    if (pixelItem == null)
                    {
                        pointInfo.Color = firstPixel;
                    }
                    if (pointInfo.X >= this._width)
                    {
                        continue;
                    }
                    if (pointInfo.Y >= this._height)
                    {
                        continue;
                    }

                    #endregion

                    if (transparentColor != null)
                    {
                        if (colorComparer.Equals(pointInfo.Color, transparentColor.Value))
                        {
                            pointInfo.Color = transparentColor.Value;
                        }
                    }

                    pieceBitmap.SetPixel(
                        (int)pointInfo.X,
                        (int)pointInfo.Y,
                        pointInfo.Color);
                }

                // Overwriting piece bitmap
                if (transparentColor != null)
                {
                    pieceBitmap.MakeTransparent(transparentColor.Value);
                }
                pieceBitmap.Save(fileName, ImageFormat.Png);
            }
        }

        #region Elements to manage of object

        public void Dispose()
        {
            if (this.Buffer != null)
            {
                // this.Buffer.Clear();
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
        public void ClearCache()
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            this.Buffer.CLEAR();
        }

        /// <summary>
        /// Gets an instance of picture
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public static Picture GetInstance(String fullFileName)
        {
            return GetInstance(fullFileName, new NoneColorFilter());
        }

        /// <summary>
        /// Gets an instance of picture
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public static Picture GetInstance(String fullFileName, IColorFilter colorFilter)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fullFileName))
            {
                throw new ArgumentNullException("fullFileName");
            }

            #endregion

            return new Picture(fullFileName, colorFilter);
        }

        /// <summary>
        /// Gets an instance of object. This method was projected for unit testing purposes.
        /// </summary>
        /// <returns></returns>
        internal static Picture GetInstance(Bitmap bitmap)
        {
            return GetInstance(bitmap, new NoneColorFilter());
        }

        /// <summary>
        /// Gets an instance of object. This method was projected for unit testing purposes.
        /// </summary>
        /// <returns></returns>
        internal static Picture GetInstance(Bitmap bitmap, IColorFilter colorFilter)
        {
            #region Entries validation

            if (colorFilter == null)
            {
                throw new ArgumentNullException("colorFilter");
            }

            #endregion

            Picture returnValue = new Picture();
            returnValue._height = bitmap.Height;
            returnValue._width = bitmap.Width;
            returnValue._colorFilter = colorFilter;

            returnValue.OriginalHeight = bitmap.Height;
            returnValue.OriginalWidth = bitmap.Width;

            returnValue.LoadBuffer(bitmap);

            return returnValue;
        }

        /// <summary>
        /// Gets the instance of object without load the buffer.
        /// </summary>
        /// <param name="fileNameItem"></param>
        public static Picture GetInstanceLazy(string fileNameItem)
        {
            return GetInstanceLazy(fileNameItem, new NoneColorFilter());
        }

        /// <summary>
        /// Gets the instance of object without load the buffer.
        /// </summary>
        /// <param name="fileNameItem"></param>
        public static Picture GetInstanceLazy(string fileNameItem, IColorFilter colorFilter)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fileNameItem))
            {
                throw new ArgumentNullException("fileNameItem");
            }
            if (colorFilter == null)
            {
                throw new ArgumentNullException("colorFilter");
            }

            #endregion

            Picture resultValue = new Picture();
            resultValue._fullPath = fileNameItem;
            resultValue._colorFilter = colorFilter;
            return resultValue;
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
        public void ReleaseBuffer()
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion
            this.Buffer.CLEAR();
        }

        /// <summary>
        /// Prepare for several updates
        /// </summary>
        public void BeginBatchUpdate()
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            this.Buffer.BeginTransaction();
        }

        /// <summary>
        /// Ends a batch update
        /// </summary>
        public void EndBatchUpdate()
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            this.Buffer.CommitTransaction();
        }

        /// <summary>
        /// Cancels a batch update
        /// </summary>
        public void CancelBatchUpdate()
        {
            #region Entries validation

            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            try
            {
                this.Buffer.RollbackTransaction();
            }
            catch
            {
                // Errors in this place can't turn around the normal flow of work
            }
        }

        /// <summary>
        /// Indicates if a point exists in picture
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(PointInfo point)
        {
            #region Entries validation

            if (point == null)
            {
                throw new ArgumentNullException("point");
            }
            if (this.Buffer == null)
            {
                throw new ArgumentNullException("this.Buffer");
            }

            #endregion

            return this.Buffer.EXISTS(point);
        } 

        /// <summary>
        /// Gets pixels based on column
        /// </summary>
        /// <returns></returns>
        public List<PointInfo>[] GetColumns()
        {
            var bufferList = this.Buffer.SELECTALL();
            List<PointInfo>[] result = new List<PointInfo>[this.Width];

            for (int x = 0; x < this.Width; x++)
            {
                var pixelList = from pixelItem in bufferList
                                where pixelItem.X == x
                                select pixelItem;

                var list = pixelList.ToList();
                list.Sort();
                result[x] = list;
            }

            return result;
        }

        /// <summary>
        /// Gets pixels based on lines
        /// </summary>
        /// <returns></returns>
        public List<PointInfo>[] GetLines()
        {
            var bufferList = this.Buffer.SELECTALL();
            List<PointInfo>[] result = new List<PointInfo>[this.Height];

            for (int y = 0; y < this.Height; y++)
            {
                var pixelList = from pixelItem in bufferList
                                where pixelItem.Y == y
                                select pixelItem;

                var list = pixelList.ToList();
                list.Sort();
                result[y] = list;
            }

            return result;
        }

        /// <summary>
        /// It´s the mode of clone operation
        /// </summary>
        public enum CloneMode
        {
            Full,
            StructureOnly
        }
    }
}