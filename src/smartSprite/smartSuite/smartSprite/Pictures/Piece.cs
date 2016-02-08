
using smartSuite.smartSprite.Unity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents a piece of picture, created by user
	/// </summary>
	public class Piece : IGameObject, IComparable
    {
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

            var pieceBitmap =
                new Bitmap(
                    (int)(this.PointB.X - this.PointD.X),
                    (int)(this.PointB.Y - this.PointC.Y));

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
            String name = Path.Combine(fullPath, this.Name) + ".png";
            pieceBitmap.Save(name);
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
            throw new NotImplementedException();
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