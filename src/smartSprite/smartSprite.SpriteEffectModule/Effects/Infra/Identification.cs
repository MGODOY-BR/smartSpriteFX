
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
		/// Creates an instance of object
		/// </summary>
		/// <param name="name">It´s the name of component</param>
		/// <param name="author">It´s the name of vendor</param>
		/// <param name="description">It´s the description of filter</param>
		public Identification(String name, String author, String description)
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

            #endregion

            this._name = name;
            this._author = author;
            this._description = description;
		}
	}
}