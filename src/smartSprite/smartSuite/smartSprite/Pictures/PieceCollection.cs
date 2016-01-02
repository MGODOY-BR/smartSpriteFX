
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
		/// It´s a reference picture from where the cut was taken
		/// </summary>
		private Picture _referencePicture;

		/// <summary>
		/// Represents a list of pieces
		/// </summary>
		public List<Piece> PieceList
        {
            get;
            set;
        }

		/// <summary>
		/// It´s list of groups
		/// </summary>
		public List<Group> GroupList
        {
            get;
            set;
        }

        public PieceCollection(Picture picture)
        {
            #region Entries validation

            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }

            #endregion

            this.PieceList = new List<Piece>();
            this.GroupList = new List<Group>();
            this._referencePicture = picture;
        }

		/// <summary>
		/// Generates the pieces created
		/// </summary>
		/// <param name="folder"></param>
		public void Generate(String folder) {

            #region Entries validation

            if (String.IsNullOrEmpty(folder))
            {
                throw new ArgumentNullException("folder");
            }

            #endregion

            // TODO implement here
            throw new NotImplementedException();
		}

		/// <summary>
		/// Saves the piece collection information
		/// </summary>
		/// <param name="fileName"></param>
		public void Save(String fileName) {

            #region Entries validation
            
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            #endregion
            // TODO implement here
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads a saved piece collection
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static PieceCollection Load(String fileName) {

            #region Entries validation

            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            #endregion
            // TODO implement here
            throw new NotImplementedException();
        }

    }
}