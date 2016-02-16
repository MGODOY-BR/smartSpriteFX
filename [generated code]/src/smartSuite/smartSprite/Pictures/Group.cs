
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents a group of piece to join the related pieces of image
	/// </summary>
	public class Group : IGameObject {

		/// <summary>
		/// Represents a group of piece to join the related pieces of image
		/// </summary>
		public Group() {
		}

		/// <summary>
		/// It´s the name of group
		/// </summary>
		public String Name;

		/// <summary>
		/// Join a collection of related pieces of pictures.
		/// </summary>
		public HashSet<Piece> RelatedPieceList;

		/// <summary>
		/// It's the eventual parent of group
		/// </summary>
		public Group ParentGroup;

		/// <summary>
		/// It´s a list of child list
		/// </summary>
		public HashSet<Group> ChildGroupList;

		/// <summary>
		/// Generates a group structure
		/// </summary>
		/// <param name="folder">		public void Generate(String folder) {</param>
			// TODO implement here
		}

	}
}