
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Unity{
	/// <summary>
	/// Represents a meta class of sprites
	/// </summary>
	public class MetaSprite {

		/// <summary>
		/// It´s the game object represented by the meta sprite
		/// </summary>
		public GameObject RelatedGameObject
        {
            get;
            set;
        }

		public MetaSprite(GameObject gameObject)
        {
            this.RelatedGameObject = gameObject;
		}

		/// <summary>
		/// Generates a meta file in specified path.
		/// </summary>
		/// <param name="fullPath"></param>
		public void Generate(String fullPath) {
            throw new NotImplementedException();
		}

	}
}