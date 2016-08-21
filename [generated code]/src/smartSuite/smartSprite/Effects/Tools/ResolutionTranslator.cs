
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Tools{
	/// <summary>
	/// Represents a translator to lower image resolution
	/// </summary>
	public class ResolutionTranslator {

		/// <summary>
		/// Represents a translator to lower image resolution
		/// </summary>
		public ResolutionTranslator() {
		}

		/// <summary>
		/// It´s a calculated resolution tax.
		/// </summary>
		private float _resolutionTax;

		/// <summary>
		/// It´s the original picture
		/// </summary>
		private Picture _originalPicture;

		/// <summary>
		/// 
		/// </summary>
		private ColorBuffer _colorBuffer;

		/// <summary>
		/// 
		/// </summary>
		private Dictionary _translatedPixel;

		/// <summary>
		/// Creates an instance of the object
		/// </summary>
		/// <param name="originalPicture">It´s the original picture</param>
		/// <param name="newWidth">It´s the lenght of new image</param>
		/// <param name="newHeight">It´s the height of destination image</param>
		/// <param name="newColorAmount">It´s a number of simultaneous color</param>
		public void ResolutionTranslator(Picture originalPicture, int newWidth, int newHeight, int newColorAmount) {
			// TODO implement here
		}

		/// <summary>
		/// Translate a pixel for new new resolution
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color">		public void Translate(int x, int y, Color color) {</param>
			// TODO implement here
		}

		/// <summary>
		/// Gets the color buffer
		/// </summary>
		/// <returns></returns>
		public ColorBuffer GetColorBuffer() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// Creates a picture from translated pixel cache
		/// </summary>
		/// <returns></returns>
		public Picture CreatedTranslatedPicture() {
			// TODO implement here
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		public class Dictionary {

			/// <summary>
			/// 
			/// </summary>
			public Dictionary() {
			}

			/// <summary>
			/// 
			/// </summary>
			public Color TKey;

			/// <summary>
			/// 
			/// </summary>
			public PointCollection TValue;


		}

	}
}