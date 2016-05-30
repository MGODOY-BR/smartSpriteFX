
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Unity{
	/**
	 * Represents a meta class of sprites
	 */
	public class MetaSprite {

		/**
		 * Represents a meta class of sprites
		 */
		public MetaSprite() {
		}

		/**
		 * It´s the game object represented by the meta sprite
		 */
		public IGameObject RelatedGameObject;

		/**
		 * @param gameObject
		 */
		public void MetaSprite(IGameObject gameObject ) {
			// TODO implement here
		}

		/**
		 * Generates a meta file in specified path.
		 * @param fullPath
		 */
		public void Generate(String fullPath) {
			// TODO implement here
		}

	}
}