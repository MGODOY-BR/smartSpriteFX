
using smartSprite.Templates;
using smartSprite.Utilities;
using smartSuite.smartSprite.Pictures.ColorPattern;
using smartSuite.smartSprite.Pictures.PixelPatterns;
using smartSuite.smartSprite.Unity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace smartSuite.smartSprite.Pictures{
    /// <summary>
    /// Represents a piece of picture, created by user
    /// </summary>
    [Serializable]
    public class Piece : IGameObject, IComparable
    {
        /// <summary>
        /// Gets the generated file name from <see cref="TakePicture(string)"/> method.
        /// </summary>
        private String _takenPictureFullFileName;

		/// <summary>
		/// It´s the name of piece of picture
		/// </summary>
		public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// It´s the parent of piece
        /// </summary>
        public Piece Parent { get; set; }

        /// <summary>
        /// It's the reference picture about the piece is part of it
        /// </summary>
        private Picture _referencePicture;

		/// <summary>
		/// It's the first point of a hypotenuse
		/// </summary>
		public Point PointA
        {
            get;
            set;
        }

		/// <summary>
		/// It's a last point of a hypotenuse
		/// </summary>
		public Point PointB
        {
            get;
            set;
        }

		/// <summary>
		/// 
		/// </summary>
		public Point PointD
        {
            get;
            set;
        }

		/// <summary>
		/// 
		/// </summary>
		public Point PointC
        {
            get;
            set;
        }

		/// <summary>
		/// Creates  instance of an object
		/// </summary>
		/// <param name="picture"></param>
		/// <param name="pointA"></param>
		/// <param name="pointB"></param>
		public Piece(Picture picture, Point pointA, Point pointB)
        {
            #region Entries validation

            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }
            if (pointA == null)
            {
                throw new ArgumentNullException("pointA");
            }
            if (pointB == null)
            {
                throw new ArgumentNullException("pointB");
            }

            #endregion

            this._referencePicture = picture;
            this.PointA = pointA;
            this.PointB = pointB;
            this.PointC = new Point(pointB.X, pointA.Y);
            this.PointD = new Point(pointA.X, pointB.Y);
		}

        /// <summary>
        /// Corrects points based on main points
        /// </summary>
        public void Fix()
        {
            this.PointC.X = this.PointB.X;
            this.PointC.Y = this.PointA.Y;

            this.PointD.X = this.PointA.X;
            this.PointD.Y = this.PointB.Y;
        }

        /// <summary>
        /// Gets the full path, based on the parent pieces
        /// </summary>
        /// <returns></returns>
        internal String GetFullPath()
        {
            #region Entries validation

            if (this.Parent == null)
            {
                return "";
            }

            #endregion

            Piece parent = this.Parent;
            StringCollection pathList = new StringCollection();
            while (parent != null)
            {
                pathList.Add(parent.Name);

                parent = parent.Parent;
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = pathList.Count - 1; i >= 0; i--)
            {
                stringBuilder.Append(pathList[i]);
                stringBuilder.Append(@"\Items\");
            }

            String retorno = stringBuilder.ToString();

            return retorno;
        }

        /// <summary>
        /// Changesthe name of piece
        /// </summary>
        /// <param name="newName"></param>
        public void ChangeName(String newName)
        {
            this.Name = newName;
		}

        /// <summary>
        /// Generates a file with the content of piece of image
        /// </summary>
        /// <param name="fullPath"></param>
        public void TakePicture(String fullPath)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fullPath))
            {
                throw new ArgumentNullException("fullPath");
            }
            if (this._referencePicture == null)
            {
                throw new ArgumentNullException("this._referencePicture");
            }
            if (this.PointB == null)
            {
                throw new ArgumentNullException("this.PointB");
            }
            if (this.PointA == null)
            {
                throw new ArgumentNullException("this.PointA");
            }
            if (this.PointC == null)
            {
                throw new ArgumentNullException("this.PointC");
            }
            if (this.PointD == null)
            {
                throw new ArgumentNullException("this.PointD");
            }
            if (String.IsNullOrEmpty(this.Name))
            {
                throw new ArgumentNullException("this.Name");
            }

            #endregion

            // Creating an object to dettect the transparent background
            BackgroundPattern backgroundPattern = new BackgroundPattern();

            using (var pieceBitmap =
                new Bitmap(
                    (int)(Math.Abs(this.PointB.X - this.PointD.X)),
                    (int)(Math.Abs(this.PointB.Y - this.PointC.Y)), 
                    PixelFormat.Format32bppArgb))
            {
                int extraMargin = 10;    // <-- Extra Margin allows to backgroundPattern learn about the parent image

                int minY = (int)this.PointA.Y - extraMargin;
                int maxY = (int)this.PointB.Y + extraMargin;

                for (int y = minY; y < maxY; y++)
                {
                    int minX = (int)this.PointA.X - extraMargin;
                    int maxX = (int)this.PointB.X + extraMargin;

                    for (int x = minX; x < maxX; x++)
                    {
                        try
                        {
                            var piecePixel =
                                this._referencePicture.GetPixel(x, y);

                            if ((y > this.PointA.Y && y < this.PointB.Y) && (x > this.PointA.X && x < this.PointB.X))
                            {
                                // Creating fragment image
                                pieceBitmap.SetPixel(
                                    Math.Abs((int)this.PointA.X - x),
                                    Math.Abs((int)this.PointC.Y - y),
                                    piecePixel);
                            }
                            else
                            {
                                // Learning pattern
                                backgroundPattern.Learn(x, y, piecePixel);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }
                    }
                    StaminaUtil.GetRestSometimes();
                }

                // Getting the name of piece file
                this._takenPictureFullFileName = Path.Combine(fullPath, this.Name) + ".png";

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                // Saving piece bitmap
                pieceBitmap.Save(this._takenPictureFullFileName, ImageFormat.Png);

                // Making transparent background
                backgroundPattern.DoTransparentBorder(this);

                // Saving Unity metadata
                string metaFileName = this._takenPictureFullFileName + ".meta";
                using (StreamWriter stream = new StreamWriter(metaFileName))
                {
                    // Setting Guid
                    stream.Write(
                        UnityMetaFile.GameObject2D.Replace(
                            UnityMetaFile.GuidPlaceHolder,
                            Guid.NewGuid().ToString().Replace("-", "")));
                }

            }

            if (this.Parent != null)
            {
                // Covering the parent
                this.Parent.OverCover(this);
            }
        }

        /// <summary>
        /// Duplicates the current piece
        /// </summary>
        /// <returns></returns>
        public Piece Duplicate()
        {
            return new Piece(this._referencePicture, this.PointA, this.PointB);
		}

        /// <summary>
        /// Checks if a piece contains another one
        /// </summary>
        /// <returns></returns>
        public bool Contains(Piece other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (this == other)
            {
                return false;
            }

            #endregion

            return this.PointA.X < other.PointA.X &&
                    this.PointA.Y < other.PointA.Y &&
                    this.PointB.X > other.PointB.X &&
                    this.PointB.Y > other.PointB.Y;
        }

        /// <summary>
        /// Covers the area of piece with a suppose existent part behind of image
        /// </summary>
        /// <param name="child">Normally, this parameter is the child</param>
        private void OverCover(Piece child)
        {
            #region Entries validation

            if (child == null)
            {
                throw new ArgumentNullException("child");
            }
            if (String.IsNullOrEmpty(this._takenPictureFullFileName))
            {
                throw new ArgumentNullException("this._takenPictureFullFileName");
            }
            if (!this.Contains(child))
            {
                return;
            }

            #endregion

            // OverwrittenBitmap function is to overcome the Bitmap object bug when used with the same Stream to load and written
            Bitmap overwrittenBitmap = null;

            // Creating the object to study the patterns
            PixelPattern pixelPattern = new PixelPattern();

            // Loading the generated main piece
            using (Bitmap loadBitmap = new Bitmap(this._takenPictureFullFileName))
            {
                overwrittenBitmap = new Bitmap(loadBitmap.Width, loadBitmap.Height);

                int deltaX = (int)this.PointA.X;
                int deltaY = (int)this.PointA.Y;

                for (int y = 0; y < loadBitmap.Height; y++)
                {
                    for (int x = 0; x < loadBitmap.Width; x++)
                    {
                        // Getting original color
                        var color = loadBitmap.GetPixel(x, y);

                        // Studing the original image
                        pixelPattern.Learn(x, y, color);

                        // Covering the pixel
                        if (x >= child.PointA.X - deltaX && x <= child.PointB.X - deltaX)
                        {
                            if (y >= child.PointA.Y - deltaY && y <= child.PointB.Y - deltaY)
                            {
                                overwrittenBitmap.SetPixel(
                                    x,
                                    y,
                                    pixelPattern.GetPattern(x, y));    // <-- Covering the gap

                                continue;
                            }
                        }

                        // Copying the original pixel
                        overwrittenBitmap.SetPixel(x, y, color);
                    }
                }
            }

            overwrittenBitmap.Save(this._takenPictureFullFileName, ImageFormat.Png);
        }

        #region IComparable elements

        public int CompareTo(object obj)
        {
            #region Entries validation

            if (obj == null)
            {
                return 1;
            }
            if (!(obj is Piece))
            {
                throw new NotSupportedException(obj.GetType().Name);
            }

            #endregion

            Piece other = (Piece)obj;

            var referenceX = this.PointA.X;
            var otherX = other.PointA.X;

            var referenceY = this.PointA.Y;
            var otherY = other.PointA.Y;

            if (referenceX < this.PointD.X)
            {
                referenceX = this.PointD.X;
            }
            if (otherX < other.PointD.X)
            {
                otherX = other.PointD.X;
            }

            int compareX = referenceX.CompareTo(otherX);
            int compareY = referenceY.CompareTo(otherY);

            return compareY;
        }

        #endregion

        /// <summary>
        /// Gets the taken picture file name, correspondent to piece
        /// </summary>
        public string GetTakenPictureFullFileName()
        {
            return this._takenPictureFullFileName;
        }

        public override string ToString()
        {
            return String.Format(
                "{0} - A[{1}], B[{2}], C[{3}], D[{4}]",
                this.Name,
                this.PointA.ToString(),
                this.PointB.ToString(),
                this.PointC.ToString(),
                this.PointD.ToString());
        }
    }
}