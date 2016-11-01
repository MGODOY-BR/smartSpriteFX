
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Infra{
	/// <summary>
	/// Represents the identification notes from component
	/// </summary>
	public class Identification {

		/// <summary>
		/// It´s the description of filter
		/// </summary>
		private String _description;

		/// <summary>
		/// It´s the name of vendor
		/// </summary>
		private String _author;

		/// <summary>
		/// It´s the name of filter
		/// </summary>
		private String _name;

        /// <summary>
        /// It´s a string to group by
        /// </summary>
        private String _group;

        /// <summary>
        /// It´s a string to group by
        /// </summary>
        public String GetGroup()
        {
            return this._group;
        }

        /// <summary>
        /// Get the description of filter
        /// </summary>
        /// <returns></returns>
        public String GetDescription()
        {
        	return this._description;
		}

		/// <summary>
		/// Get the name of author
		/// </summary>
		/// <returns></returns>
		public String GetAuthor() {
			return this._author;
		}

		/// <summary>
		/// Gets the filter name
		/// </summary>
		/// <returns></returns>
		public String GetName() {
			return this._name;
		}

        /// <summary>
        /// Sets the description
        /// </summary>
        /// <param name="description"></param>
        public void SetDescription(string description)
        {
            #region Entries validation
            
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            #endregion

            this._description = description;
        }

        /// <summary>
        /// Sets the name of filter
        /// </summary>
        /// <param name="v"></param>
        public void SetName(string name)
        {
            #region Entries validation
            
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            #endregion

            this._name = name;
        }

        /// <summary>
        /// Creates an instance of object
        /// </summary>
        /// <param name="name">It´s the name of component</param>
        /// <param name="author">It´s the name of vendor</param>
        /// <param name="description">It´s the description of filter</param>
        /// <param name="group">It´s a information to group by</param>
        public Identification(String name, String author, String description, String group)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (String.IsNullOrEmpty(author))
            {
                throw new ArgumentNullException("author");
            }
            if (String.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }
            if (String.IsNullOrEmpty(group))
            {
                throw new ArgumentNullException("group");
            }

            #endregion

            this._name = name;
            this._author = author;
            this._description = description;
            this._group = group;
		}

        /// <summary>
        /// Sets the group
        /// </summary>
        /// <param name="group"></param>
        public void SetGroup(string group)
        {
            this._group = group;
        }
    }
}