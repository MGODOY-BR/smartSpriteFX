
using smartSuite.smartSprite.Pictures.PixelPatterns;
using smartSuite.smartSprite.Unity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

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

            using (var pieceBitmap =
                new Bitmap(
                    (int)(this.PointB.X - this.PointD.X),
                    (int)(this.PointB.Y - this.PointC.Y)))
            {

                for (float y = this.PointA.Y; y < this.PointB.Y; y++)
                {
                    for (float x = this.PointA.X; x < this.PointB.X; x++)
                    {
                        var piecePixel =
                            this._referencePicture.GetPixel(
                                (int)x,
                                (int)y);

                        pieceBitmap.SetPixel(
                            Math.Abs((int)this.PointA.X - (int)x),
                            Math.Abs((int)this.PointC.Y - (int)y),
                            piecePixel);
                    }
                }

                // Getting the name of piece file
                this._takenPictureFullFileName = Path.Combine(fullPath, this.Name) + ".png";

                // Saving piece bitmap
                pieceBitmap.Save(this._takenPictureFullFileName, ImageFormat.Png);
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
        /// Covers the area of piece with a supposedly existent part behind of image
        /// </summary>
        /// <param name="other">Normally, this parameter is the parent</param>
        private void OverCover(Piece other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (String.IsNullOrEmpty(this._takenPictureFullFileName))
            {
                throw new ArgumentNullException("this._takenPictureFullFileName");
            }
            if (!this.Contains(other))
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

                for (int y = 0; y < loadBitmap.Height; y++)
                {
                    for (int x = 0; x < loadBitmap.Width; x++)
                    {
                        int absoluteX = (int)this.PointA.X + x;
                        int absoluteY = (int)this.PointA.Y + y;

                        // Covering the pixel
                        if (absoluteX >= other.PointA.X && absoluteX <= other.PointB.X)
                        {
                            if (absoluteY >= other.PointA.Y && absoluteY <= other.PointB.Y)
                            {
                                overwrittenBitmap.SetPixel(
                                    Math.Abs(x),
                                    Math.Abs(y),
                                    pixelPattern.GetPattern(x, y));    // <-- Covering the gap

                                continue;
                            }
                        }

                        // Getting original color
                        var color = loadBitmap.GetPixel(x, y);

                        // Studing the original image
                        pixelPattern.Learn(x, y, color);  

                        // Copying the pixel
                        overwrittenBitmap.SetPixel(x, y, loadBitmap.GetPixel(x, y));
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

            return this.PointA.X.CompareTo(other.PointA.X);
        }

        #endregion
    }
}