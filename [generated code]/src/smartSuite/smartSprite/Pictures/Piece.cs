
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents a piece of picture, created by user
	/// </summary>
	public class Piece : IGameObject {

		/// <summary>
		/// Represents a piece of picture, created by user
		/// </summary>
		public Piece() {
		}

		/// <summary>
		/// It´s the name of piece of picture
		/// </summary>
		public String Name;

		/// <summary>
		/// It's the reference picture about the piece is part of it
		/// </summary>
		private Picture _referencePicture;

		/// <summary>
		/// It's the first point ofa hypotenuse
		/// </summary>
		public Point PointA;

		/// <summary>
		/// It's a last point of a hypotenuse
		/// </summary>
		public Point PointB;

		/// <summary>
		/// 
		/// </summary>
		public Point PointC;

		/// <summary>
		/// 
		/// </summary>
		public Point PointD;

		/// <summary>
		/// It´s the parent of piece
		/// </summary>
		public Piece Parent;

		/// <summary>
		/// Creates  instance of an object
		/// </summary>
		/// <param name="picture"></param>
		/// <param name="pointA"></param>
		/// <param name="pointB">		public void Piece(Picture picture, Point pointA, Point pointB) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Changesthe name of piece
		/// </summary>
		/// <param name="newName">		public void ChangeName(String newName) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Generates a file with the content of piece of image
		/// </summary>
		/// <param name="fullPath">		public void TakePicture(String fullPath) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Duplicates the current piece
		/// </summary>
		/// <returns></returns>
		public Piece Duplicate() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Covers the area of piece with a supposedly existent part behind of image
		/// </summary>
		/// <param name="other">Normally, this parameter is the parent</param>
		private void OverCover(Piece other) {
			// TODO implement here
		}

	}
}