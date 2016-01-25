
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
		/// Represents a meta class of sprites
		/// </summary>
		public MetaSprite() {
		}

		/// <summary>
		/// It´s the game object represented by the meta sprite
		/// </summary>
		public IGameObject RelatedGameObject;

		/// <param name="gameObject"></param>
		public void MetaSprite(IGameObject gameObject ) {
			// TODO implement here
		}

		/// <summary>
		/// Generates a meta file in specified path.
		/// </summary>
		/// <param name="fullPath"></param>
		public void Generate(String fullPath) {
			// TODO implement here
		}

	}
}