
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Forms{
	/// <summary>
	/// Manages the piece of pictures
	/// </summary>
	public static class PieceManager {

        /// <summary>
        /// It´s a list of pieces
        /// </summary>
        public static PieceCollection PieceList
        {
            get;
            set;
        }

		/// <summary>
		/// Creates an instance of a piece of picture
		/// </summary>
		/// <param name="picture"></param>
		/// <param name="pointA"></param>
		/// <param name="pointB"></param>
		/// <returns></returns>
		public static Piece GetInstance(Picture picture, Point pointA, Point pointB)
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

            Piece piece = new Piece(picture, pointA, pointB);
            PieceList.PieceList.Add(piece);
            return piece;
        }

		/// <summary>
		/// Loads a collection of piece of pictures
		/// </summary>
		/// <param name="fileName"></param>
		public static void Load(String fileName) {
            throw new NotImplementedException();
		}

	}
}