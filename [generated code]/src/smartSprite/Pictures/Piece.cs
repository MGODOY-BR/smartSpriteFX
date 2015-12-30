
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSprite.Pictures{
	/// <summary>
	/// Represents a piece of picture, created by user
	/// </summary>
	public class Piece : GameObject {

		/// <summary>
		/// Represents a piece of picture, created by user
		/// </summary>
		public Piece() {
		}

		/// <summary>
		/// It´s the name of piece of picture
		/// </summary>
		private String _name;

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
		/// Creates  instance of an object
		/// </summary>
		/// <param name="picture"></param>
		/// <param name="pointA"></param>
		/// <param name="pointB"></param>
		public void Piece(Picture picture, Point pointA, Point pointB) {
			// TODO implement here
		}

		/// <summary>
		/// Changesthe name of piece
		/// </summary>
		/// <param name="newName"></param>
		public void ChangeName(String newName) {
			// TODO implement here
		}

		/// <summary>
		/// Generates a file with the content of piece of image
		/// </summary>
		/// <param name="fullPath"></param>
		public void TakePicture(String fullPath) {
			// TODO implement here
		}

	}
}