
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

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
        /// It´s the transparent color
        /// </summary>
        private Color _transparentColor = Color.Transparent;

        /// <summary>
        /// It´s a buffer of colors, where key it's a combination of x and y coordinates. 
        /// </summary>
        [NonSerialized]
        private PictureDatabase _buffer;

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
                    if (color != null)
                    {
                        this.ReplacePixel(x, y, color.Value);
                        var argb = color.Value.ToArgb();
                        if (!colorSet.Contains(argb))
                        {
                            colorSet.Add(argb);
                        }
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
            #region Entries validation

            if (this._buffer == null)
            {
                throw new ArgumentNullException("this._buffer");
            }

            #endregion

            this._buffer.UPDATE(oldColor, newColor);
        }

        /// <summary>
        /// Creates an instance of the object.
        /// </summary>
        /// <remarks>
        /// This constructor was intented to be used internally only.
        /// </remarks>
        internal Picture()
        {
            this._buffer = PictureDatabase.Open();
            this._buffer.CLEAR();
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

            lock (fullPath)
            {
                using (var bitmap = new Bitmap(fullPath))
                {
                    this._height = bitmap.Height;
                    this._width = bitmap.Width;

                    this.LoadBuffer(bitmap);
                }
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

            #region Entries validation

            if (this._buffer != null && this._buffer.COUNT() > 0)
            {
                return;
            }

            #endregion

            this._buffer = PictureDatabase.Open();
            this._buffer.CreateDatabase();
            this._buffer.CLEAR();

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
            this._height = image.Height;
            this._width = image.Width;
        }

        /// <summary>
        /// Loads the color info cache
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="colorInfo"></param>
        private void LoadColorInfoCache(int y, int x, ColorInfo colorInfo)
        {
            // Save private picture
            this._buffer.INSERT(x, y, colorInfo.GetInnerColor());
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

            clone._buffer = this._buffer.Clone();

            // Copying another attributes
            clone._height = this._height;
            clone._width = this._width;
            clone.ColorCount = this.ColorCount;
            clone._transparentColor = this._transparentColor;
            clone._fullPath = this._fullPath;

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

            if (this._buffer.COUNT() == 0)
            {
                throw new ArgumentException("Empty picture.");
            }

            #endregion

            if (this._buffer == null)
            {
                this.LoadBuffer(this._fullPath);
            }
            var item = this._buffer.SELECT(x, y);
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
            if (this._buffer == null)
            {
                return new List<Point>();
            }

            #endregion

            List<Point> returnList = new List<Point>();
            var colorCacheList = this._buffer;
            foreach (var findColorItem in colorList)
            {
                returnList.AddRange(
                    this._buffer.SELECT(findColorItem));
            }

            return returnList;
        }

        /// <summary>
        /// Gets the point of image considered border
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

            #region Obsolete Code

            /*
            String sql = @"
            CREATE TEMPORARY TABLE TMP_PICTURE
            (
                SESSIONID TEXT NOT NULL,
                X INTEGER  NOT NULL,
                Y INTEGER  NOT NULL,
                A INTEGER  NOT NULL,
                R INTEGER  NOT NULL,
                G INTEGER  NOT NULL,
                B INTEGER  NOT NULL,
                PRIMARY KEY (SESSIONID, X, Y)
            );
            CREATE INDEX TMP_PICTURE_A ON TMP_PICTURE (A);
            CREATE INDEX TMP_PICTURE_R ON TMP_PICTURE (R);
            CREATE INDEX TMP_PICTURE_G ON TMP_PICTURE (G);
            CREATE INDEX TMP_PICTURE_B ON TMP_PICTURE (B);

            INSERT INTO TMP_PICTURE
            (
                SESSIONID,
                X,
                Y,
                A,
                R,
                G,
                B
            )
            SELECT
                SESSIONID,
                X,
                Y,
                A,
                R,
                G,
                B
            FROM TB_PICTURE
            WHERE
                SESSIONID = @SESSIONID AND
                -- A = @A AND
                R = @R AND
                G = @G AND
                B = @B;

            SELECT
                T_ITEM.X,
                T_ITEM.Y
            FROM
                TB_PICTURE T_ITEM
            WHERE
                T_ITEM.SESSIONID = @SESSIONID AND
                (
                    R <> @R AND
                    G <> @G AND
                    B <> @B
                ) AND 
                EXISTS
                (
                    SELECT 1 FROM
                    TMP_PICTURE T_ITEM_ASIDE
                    WHERE
                        T_ITEM_ASIDE.SESSIONID = T_ITEM.SESSIONID
                        AND 
                        (
                            -- left and right
                            (
                                T_ITEM_ASIDE.Y = T_ITEM.Y AND 
                                (
                                    T_ITEM_ASIDE.X = T_ITEM.X - 1 OR
                                    T_ITEM_ASIDE.X = T_ITEM.X + 1
                                )
                            )
                            OR
                            -- top and bottom
                            (
                                T_ITEM_ASIDE.X = T_ITEM.X AND 
                                (
                                    T_ITEM_ASIDE.Y = T_ITEM.Y - 1 OR
                                    T_ITEM_ASIDE.Y = T_ITEM.Y + 1
                                )
                            )
                        )
                    )
            ";

            List<Point> returnList = new List<Point>();

            using (var dataReader =
                this._buffer.SELECT(
                    sql,
                    PictureDatabase.CreateDbParameter("@A", this._transparentColor.A),
                    PictureDatabase.CreateDbParameter("@R", this._transparentColor.R),
                    PictureDatabase.CreateDbParameter("@G", this._transparentColor.G),
                    PictureDatabase.CreateDbParameter("@B", this._transparentColor.B)))
            {
                while (dataReader.Read())
                {
                    returnList.Add(
                            new Point(
                                dataReader.GetInt32(0),
                                dataReader.GetInt32(1))
                        );
                }
            }
            return returnList;
            */

            #endregion

            String sql = @"            
            SELECT
                -- SESSIONID,
                X,
                Y,
                A,
                R,
                G,
                B
            FROM TB_PICTURE
            WHERE
                SESSIONID = @SESSIONID AND
                -- A = @A AND
                NOT (
                    R = @R AND
                    G = @G AND
                    B = @B
                );
            ";

            List<Point> returnList = new List<Point>();

            using (var dataReader =
                this._buffer.SELECT(
                    sql,
                    PictureDatabase.CreateDbParameter("@A", this._transparentColor.A),
                    PictureDatabase.CreateDbParameter("@R", this._transparentColor.R),
                    PictureDatabase.CreateDbParameter("@G", this._transparentColor.G),
                    PictureDatabase.CreateDbParameter("@B", this._transparentColor.B)))
            {
                while (dataReader.Read())
                {
                    PointInfo pointItem =
                        new PointInfo(
                            dataReader.GetInt32(0),
                            dataReader.GetInt32(1),
                            Color.FromArgb(
                                dataReader.GetInt32(2),
                                dataReader.GetInt32(3),
                                dataReader.GetInt32(4),
                                dataReader.GetInt32(5)));

                    #region Entries validation

                    // The point can't be transparent
                    if (pointItem.Color.ToArgb().Equals(this._transparentColor.ToArgb()))
                    {
                        continue;
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
                        if (this._buffer.COUNT(pointArrayItem, this._transparentColor) > 0)
                        {
                            returnList.Add(pointItem);
                        }
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
            if (this._buffer == null)
            {
                throw new ArgumentNullException("this._buffer");
            }

            #endregion

            var affectedPixel = this._buffer.UPDATE(x, y, newColor);
            if (affectedPixel == 0)
            {
                this._buffer.INSERT(x, y, newColor);
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

            Color firstPixel = this.GetPixel(0, 0).Value;
            using (var pieceBitmap = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb))
            {
                for (int y = 0; y < this._height; y++)
                {
                    for (int x = 0; x < this._width; x++)
                    {
                        var piecePixel = this.GetPixel(x, y);

                        #region Entries validation

                        if (piecePixel == null)
                        {
                            piecePixel = firstPixel;
                        }

                        #endregion

                        if (colorComparer.Equals(piecePixel.Value, transparentColor))
                        {
                            piecePixel = transparentColor;
                        }

                        pieceBitmap.SetPixel(x, y, piecePixel.Value);
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
        public void ClearCache()
        {
            #region Entries validation

            if (this._buffer == null)
            {
                throw new ArgumentNullException("this._buffer");
            }

            #endregion

            this._buffer.CLEAR();
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

            /*
            lock (Picture._pictureCache)
            {
                if (!Picture._pictureCache.ContainsKey(fullFileName))
                {
                    Picture._pictureCache.Add(fullFileName, new Picture(fullFileName));
                }

                return Picture._pictureCache[fullFileName];
            }
            */
            return new Picture(fullFileName);
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
            #region Entries validation

            if (this._buffer == null)
            {
                throw new ArgumentNullException("this._buffer");
            }

            #endregion
            this._buffer.CLEAR();
        }
    }
}