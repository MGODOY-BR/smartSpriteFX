
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents a collection of pieces of a picture
	/// </summary>
	public class PieceCollection {

		/// <summary>
		/// Represents a collection of pieces of a picture
		/// </summary>
		public PieceCollection() {
		}

		/// <summary>
		/// 
		/// </summary>
		private Picture _referencePicture;

		/// <summary>
		/// Represents a list of pieces
		/// </summary>
		public HashSet<Piece> PieceList;

		/// <summary>
		/// It´s list of groups
		/// </summary>
		public HashSet<Group> GroupList;

		/// <summary>
		/// Generates the pieces created
		/// </summary>
		/// <param name="folder">		public void Generate(String folder) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Saves the piece collection information
		/// </summary>
		/// <param name="fileName">		public void Save(String fileName) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Loads a saved piece collection
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public static PieceCollection Load(String fileName) {
			// TODO implement here
			return null;
		}

	}
}