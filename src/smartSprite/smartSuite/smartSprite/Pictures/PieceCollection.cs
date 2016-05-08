
using smartSprite.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace smartSuite.smartSprite.Pictures{
	/// <summary>
	/// Represents a collection of pieces of a picture
	/// </summary>
    [Serializable]
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

        /// <summary>
        /// Gets the reference picture
        /// </summary>
        public Picture ReferencePicture
        {
            get
            {
                return _referencePicture;
            }
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
            if (this.PieceList == null)
            {
                throw new ArgumentNullException("this.PieceList");
            }
            if (this.PieceList.Count == 0)
            {
                throw new ArgumentNullException("There is no pieces in picture.");
            }

            #endregion

            // Cleaning files
            DirectoryInfo directoryInfo = new DirectoryInfo(folder);
            foreach (var file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directory.Delete(true);
            }

            // It´s a list of group references
            Dictionary<String, Group> groupReferenceList = new Dictionary<string, Group>();
            foreach (var piece in this.PieceList)
            {
                // Creating group
                if (piece.Parent != null)
                {
                    // Infering the name of group
                    var groupName = 
                        piece.Parent.Name + " Group";
                    var fullParentName = 
                        piece.GetFullPath();

                    Group group;
                    if (!groupReferenceList.ContainsKey(fullParentName))
                    {
                        group = new Group();
                        group.Name = groupName;
                        groupReferenceList.Add(fullParentName, group);
                    }
                    else
                    {
                        group = groupReferenceList[fullParentName];
                    }

                    group.RelatedPieceList.Add(piece);
                }
                else
                {
                    // Generating pieces with no groups
                    piece.TakePicture(folder + @"\" + piece.Name);
                }
            }

            // Group generation
            foreach (var groupReference in groupReferenceList)
            {
                string groupFolder = Path.Combine(folder, groupReference.Key);
                Console.WriteLine(groupFolder);
                groupReference.Value.Generate(groupFolder);
            }
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

            using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Loads a saved piece collection
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static PieceCollection Load(String fileName)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File not found.");
            }

            #endregion

            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (PieceCollection)formatter.Deserialize(stream);
            }
        }
    }
}