
using smartSprite.Pictures.ColorPattern;
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
		private int _resolutionTax;

		/// <summary>
		/// It´s the original picture
		/// </summary>
		private Picture _originalPicture;

		/// <summary>
		/// It´s the color buffer
		/// </summary>
		private ColorBuffer _colorBuffer;

        /// <summary>
        /// It´s the last scan that has scanned in Translated Method
        /// </summary>
        private Pictures.Point _lastScannedPoint;

		/// <summary>
		/// Is a dictionary made of colors and collection of pixels
		/// </summary>
		private Dictionary<Color, PointRange> _translatedPixel = new Dictionary<Color, PointRange>();
        
        /// <summary>
        /// Gets the resolution tax
        /// </summary>
        public int ResolutionTax
        {
            get
            {
                return _resolutionTax;
            }
        }

        /// <summary>
        /// Gets the last scanned point in Translate method
        /// </summary>
        public Pictures.Point LastScannedPoint
        {
            get
            {
                return _lastScannedPoint;
            }
        }

        /// <summary>
        /// It´s a list of color to avoid.
        /// </summary>
        public List<Color> AvoidColorList { get; set; }

        /// <summary>
        /// Creates an instance of the object
        /// </summary>
        /// <param name="originalPicture">It´s the original picture</param>
        /// <param name="screenWidth">It´s the lenght of new image</param>
        /// <param name="screenHeight">It´s a height of screen</param>
        /// <param name="newScreenWidth">It´s the length of the screen of  new resolution</param>
        /// <param name="newScreenHeight">It´s the height of screen of new resolution</param>
        /// <param name="newColorAmount">It´s a number of simultaneous color</param>
        public ResolutionTranslator(Picture originalPicture, int screenWidth, int screenHeight, int newScreenWidth, int newScreenHeight, int newColorAmount)
        {
            this.Initialize(originalPicture, screenWidth, screenHeight, newScreenWidth, newScreenHeight, newColorAmount, 0f);
        }

        /// <summary>
        /// Creates an instance of the object
        /// </summary>
        /// <param name="originalPicture">It´s the original picture</param>
        /// <param name="screenWidth">It´s the lenght of new image</param>
        /// <param name="screenHeight">It´s a height of screen</param>
        /// <param name="newScreenWidth">It´s the length of the screen of  new resolution</param>
        /// <param name="newScreenHeight">It´s the height of screen of new resolution</param>
        /// <param name="newColorAmount">It´s a number of simultaneous color</param>
        /// <param name="diluteColorFactor">It´s a factor used to dilute color</param>
        public ResolutionTranslator(Picture originalPicture, int screenWidth, int screenHeight, int newScreenWidth, int newScreenHeight, int newColorAmount, float diluteColorFactor)
        {
            this.Initialize(originalPicture, screenWidth, screenHeight, newScreenWidth, newScreenHeight, newColorAmount, diluteColorFactor);
        }

        /// <summary>
        /// Initializes the object
        /// </summary>
        /// <param name="originalPicture">It´s the original picture</param>
        /// <param name="screenWidth">It´s the lenght of new image</param>
        /// <param name="screenHeight">It´s a height of screen</param>
        /// <param name="newScreenWidth">It´s the length of the screen of  new resolution</param>
        /// <param name="newScreenHeight">It´s the height of screen of new resolution</param>
        /// <param name="newColorAmount">It´s a number of simultaneous color</param>
        /// <param name="contrast">It´s a factor used to contrast color</param>
        private void Initialize(Picture originalPicture, int screenWidth, int screenHeight, int newScreenWidth, int newScreenHeight, int newColorAmount, float contrast)
        {
            #region Entries validation

            if (originalPicture == null)
            {
                throw new ArgumentNullException("originalPicture");
            }
            if (newScreenWidth < 80)
            {
                throw new ArgumentException("Invalid newScreenWidth argument");
            }
            if (newColorAmount < 2)
            {
                throw new ArgumentException("Invalid newColorAmount argument");
            }

            #endregion

            this.AvoidColorList = new List<Color>();

            this._originalPicture = originalPicture;

            // Getting the picture boundaries
            int width = originalPicture.Width;
            int height = originalPicture.Height;

            // Calculating the hipotenuse of originalPicture
            double hipotenuseOriginalPicture =
                Math.Sqrt((screenWidth * screenWidth) + (screenHeight * screenHeight));

            // Calculating the hipotenuse of new picture
            double hipotenuseNewPicture =
                Math.Sqrt((newScreenWidth * newScreenWidth) + (newScreenHeight * newScreenHeight));

            // Calculating the tax of resolution
            this._resolutionTax =
                 (int)hipotenuseOriginalPicture / (int)hipotenuseNewPicture;

            float bandWidth = 
                 (float)hipotenuseNewPicture / (float)hipotenuseOriginalPicture *
                 2.15f + contrast;

            float sensibility;

            if (this._originalPicture.ColorCount <= newColorAmount)
            {
                sensibility = 0;
            }
            else
            {
                sensibility =
                    ((float)newColorAmount / (float)this._originalPicture.ColorCount) +
                    bandWidth;
            }

            this._colorBuffer = new ColorBuffer(newColorAmount, sensibility);
        }

        /// <summary>
        /// Translate a pixel for a new resolution
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color" />
        /// <returns>The last point read</returns>
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
            if (this.PointOcuppied(x, y, color, this._translatedPixel))
            {
                return;
            }

            #endregion

            bool ignoreCache = false;
            if (this._originalPicture.TransparentColor != null)
            {
                ignoreCache =
                    color.ToArgb().Equals(this._originalPicture.TransparentColor.ToArgb());
            }

            Color newColor;

            if (!ignoreCache)
            {
                // Feeding the color buffer
                this._colorBuffer.Register(color, this.AvoidColorList.ToArray());
                // Getting the color compatible with the destination resolution
                newColor = this._colorBuffer.GetSimilarColor(color);
            }
            else
            {
                newColor = color;
            }

            // Set the measurements of destination pixel
            int initialX = x;
            int initialY = y;
            int finalX = x + this._resolutionTax;
            int finalY = y + this._resolutionTax;
            
            // Getting the points to translate
            if (!this._translatedPixel.ContainsKey(newColor))
            {
                this._translatedPixel.Add(newColor, new PointRange());
            }
            var points = this._translatedPixel[newColor];
            points.Prepare();

            // Translating...
            for (int yy = y; yy < finalY; yy++)
            {
                for (int xx = x; xx < finalX; xx++)
                {
                    points.SetPoint(xx, yy);
                    this._lastScannedPoint = new Pictures.Point(xx, yy);
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
		public Picture CreatedTranslatedPicture()
        {
            // Copying picture
            Picture clonePicture = this._originalPicture.Clone();

            // Changing the internal buffer
            foreach (var translatedPixelItem in this._translatedPixel)
            {
                foreach (var pointItem in translatedPixelItem.Value.ToPointList())
                {
                    clonePicture.ReplacePixel(
                        (int)pointItem.X,
                        (int)pointItem.Y,
                        translatedPixelItem.Key);
                }
            }

            return clonePicture;
        }

        /// <summary>
        /// Gets a indicator informing if the point is occupied.
        /// </summary>
        /// <returns></returns>
        private bool PointOcuppied(int x, int y, Color originColor, Dictionary<Color, PointRange> translatedPointList)
        {
            #region Entries validation

            if (translatedPointList == null)
            {
                throw new ArgumentNullException("translatedPointList");
            }
            if(this._originalPicture == null)
            {
                throw new ArgumentNullException("this._originalPicture");
            }

            #endregion

            foreach (var values in translatedPointList.Values)
            {
                if (values.IsContained(x, y))
                {
                    return true;
                }
            }

            return false;
        }
    }
}