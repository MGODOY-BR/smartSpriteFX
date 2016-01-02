
using smartSuite.smartSprite.Unity;
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
		/// It´s the name of group
		/// </summary>
		public String Name
        {
            get;
            set;
        }

		/// <summary>
		/// Join a collection of related pieces of pictures.
		/// </summary>
		public List<Piece> RelatedPieceList
        {
            get;
            set;
        }

		/// <summary>
		/// It's the eventual parent of group
		/// </summary>
		public Group ParentGroup
        {
            get;
            set;
        }

		/// <summary>
		/// It´s a list of child list
		/// </summary>
		public List<Group> ChildGroupList
        {
            get;
            set;
        }

        public Group()
        {
            this.ChildGroupList = new List<Group>();
            this.RelatedPieceList = new List<Piece>();
        }

		/// <summary>
		/// Generates a group structure
		/// </summary>
		/// <param name="folder"></param>
		public void Generate(String folder)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(folder))
            {
                throw new ArgumentNullException("folder");
            }

            #endregion

            throw new NotImplementedException();
		}

	}
}