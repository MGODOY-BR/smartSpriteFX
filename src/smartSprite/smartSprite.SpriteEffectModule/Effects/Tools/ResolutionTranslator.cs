
using smartSuite.smartSprite.Effects.Infra;
using smartSuite.smartSprite.Pictures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace smartSuite.smartSprite.Effects.Tools{
	/// <summary>
	/// Represents a translator to lower image resolution
	/// </summary>
	public class ResolutionTranslator {

		/// <summary>
		/// It´s a calculated resolution tax.
		/// </summary>
		private float _resolutionTax;

		/// <summary>
		/// It´s the original picture
		/// </summary>
		private Picture _originalPicture;

		/// <summary>
		/// It´s the color buffer
		/// </summary>
		private ColorBuffer _colorBuffer;

		/// <summary>
		/// Is a dictionary made of colors and collection of pixels
		/// </summary>
		private Dictionary<Color, PointCollection> _translatedPixel = new Dictionary<Color, PointCollection>();

		/// <summary>
		/// Creates an instance of the object
		/// </summary>
		/// <param name="originalPicture">It´s the original picture</param>
		/// <param name="newWidth">It´s the lenght of new image</param>
		/// <param name="newHeight">It´s the height of destination image</param>
		/// <param name="newColorAmount">It´s a number of simultaneous color</param>
		public ResolutionTranslator(Picture originalPicture, int newWidth, int newHeight, int newColorAmount)
        {
            #region Entries validation

            if (originalPicture == null)
            {
                throw new ArgumentNullException("originalPicture");
            }
            if (newWidth < 80)
            {
                throw new ArgumentException("Invalid newWidth argument");
            }
            if (newHeight < 40)
            {
                throw new ArgumentException("Invalid newHeight argument");
            }

            #endregion

            this._originalPicture = originalPicture;
            this._colorBuffer = new ColorBuffer(newColorAmount);

            // Getting the picture boundaries
            int width = originalPicture.Width;
            int height = originalPicture.Height;

            // Calculating the hipotenuse of originalPicture
            double hipotenuseOriginalPicture =
                Math.Sqrt((width * width) + (height * height));

            // Calculating the hipotenuse of new picture
            double hipotenuseNewPicture =
                Math.Sqrt((newWidth * newWidth) + (newHeight * newHeight));

            // Calculating the tax of resolution
            this._resolutionTax =
                (float)hipotenuseNewPicture / (float)hipotenuseOriginalPicture;
        }

        /// <summary>
        /// Translate a pixel for a new resolution
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color" />
        public void Translate(int x, int y, Color color)
        {
            #region Entries validation

            if (x < 0 || x > this._originalPicture.Width)
            {
                throw new ArgumentOutOfRangeException("Invalid x coordinate");
            }
            if (y < 0 || y > this._originalPicture.Height)
            {
                throw new ArgumentOutOfRangeException("Invalid y coordinate");
            }

            #endregion

            // Feeding the color buffer
            this._colorBuffer.Register(color);
            // Getting the color compatible with the destination resolution
            Color newColor = this._colorBuffer.GetSimilarColor(color);

            // Set the measurements of destination pixel
            int initialX = x;
            int initialY = y;
            int finalX = x + (int)this._resolutionTax;
            int finalY = y + (int)this._resolutionTax;

            // Round whatever the decimal to next integer number
            if (this._resolutionTax != (int)this._resolutionTax)
            {
                finalX++;
                finalY++;
            }

            // Getting the points to translate
            if (!this._translatedPixel.ContainsKey(newColor))
            {
                this._translatedPixel.Add(newColor, new PointCollection());
            }
            var points = this._translatedPixel[newColor];

            // Translating...
            for (int yy = 0; yy < finalY; yy++)
            {
                for (int xx = 0; xx < finalX; xx++)
                {
                    #region Entries validation

                    if (!this.PointNotOcuppied(xx, yy, this._translatedPixel))
                    {
                        break;
                    }

                    #endregion

                    points.SetPoint(xx, yy);
                }
            }
        }

        /// <summary>
        /// Gets the color buffer
        /// </summary>
        /// <returns></returns>
        public ColorBuffer GetColorBuffer()
        {
			return this._colorBuffer;
		}

		/// <summary>
		/// Creates a picture from translated pixel cache
		/// </summary>
		/// <returns></returns>
		public Picture CreatedTranslatedPicture() {
            throw new NotImplementedException();
		}

        /// <summary>
        /// Gets a indicator informing if the point is occupied.
        /// </summary>
        /// <returns></returns>
        private bool PointNotOcuppied(int x, int y, Dictionary<Color, PointCollection> translatedPointList)
        {
            #region Entries validation

            if (translatedPointList == null)
            {
                throw new ArgumentNullException("translatedPointList");
            }

            #endregion

            bool found = false;
            foreach (var values in translatedPointList.Values)
            {
                found = values.InnerPointList.Find(item => item.X == x && item.Y == y) != null;
                if (found)
                {
                    return true;
                }
            }

            return false;
        }
    }
}