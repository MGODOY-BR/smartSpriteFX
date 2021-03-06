
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters;

namespace smartSuite.smartSpriteFX.Effects.Tools{
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
        private PointRange _lastScannedPoint;

        /// <summary>
        /// It´s a comparer of colors
        /// </summary>
        private ColorEqualityComparer _comparer = new ColorEqualityComparer();

        /// <summary>
        /// It's a dictionary made of colors and collection of pixels
        /// </summary>
        private List<PointRange> _translatedPixel = new List<PointRange>();

        /// <summary>
        /// Indicates if some of the filters are transparentFilter
        /// </summary>
        private bool _hasTransparentFilter = false;

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
        public PointRange LastScannedPoint
        {
            get
            {
                return _lastScannedPoint;
            }
        }

        /// <summary>
        /// It´s a list of color to avoid.
        /// </summary>
        private List<Color> _avoidColorList = new List<Color>();

        /// <summary>
        /// It´s a list of color to avoid.
        /// </summary>
        public List<Color> AvoidColorList
        {
            get
            {
                return _avoidColorList;
            }
            set
            {
                _avoidColorList = value;
                if (this._colorBuffer != null)
                {
                    this._colorBuffer.AvoidedColorList = _avoidColorList;
                }
            }
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
        public ResolutionTranslator(Picture originalPicture, int screenWidth, int screenHeight, int newScreenWidth, int newScreenHeight, int newColorAmount)
        {
            this.Initialize(originalPicture, screenWidth, screenHeight, newScreenWidth, newScreenHeight, newColorAmount, 0);
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
        /// <param name="contrast">It´s how different the colors are</param>
        public ResolutionTranslator(Picture originalPicture, int screenWidth, int screenHeight, int newScreenWidth, int newScreenHeight, int newColorAmount, int contrast)
        {
            this.Initialize(originalPicture, screenWidth, screenHeight, newScreenWidth, newScreenHeight, newColorAmount, contrast);
        }

        public ResolutionTranslator() : base()
        {
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
        private void Initialize(Picture originalPicture, int screenWidth, int screenHeight, int newScreenWidth, int newScreenHeight, int newColorAmount, int contrast)
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

            if (this.AvoidColorList == null)
            {
                this.AvoidColorList = new List<Color>();
            }

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

            // Checking for transparent filter
            var transparentBackgroundFilter =
                EffectEngine.GetTransparentBackgroundFilter();

            // Checking for crop filter
            var cropFilter =
                EffectEngine.FindFilter<CropFilter>();

            if (transparentBackgroundFilter != null || cropFilter != null)
            {
                this._hasTransparentFilter = true;
            }

            this._colorBuffer = new ColorBuffer(newColorAmount, contrast);
            this._colorBuffer.AvoidedColorList = this.AvoidColorList;
            if (originalPicture.TransparentColor != null)
            {
                this._colorBuffer.AvoidedColorList.Add(originalPicture.TransparentColor.Value);
            }
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
                // throw new ArgumentOutOfRangeException("Invalid x coordinate");
                return;
            }
            if (y < 0 || y > this._originalPicture.Height)
            {
                // throw new ArgumentOutOfRangeException("Invalid y coordinate");
                return;
            }
            if (this.PointOcuppied(x, y, this._resolutionTax))
            {
                return;
            }

            #endregion

            Color newColor = this._colorBuffer.GetSimilarColor(color);

            if (this._originalPicture.TransparentColor != null && color == this._originalPicture.TransparentColor)
            {
                newColor = color;
            }

            if (!this._hasTransparentFilter)
            {
                if (this._originalPicture.TransparentColor != null)
                {
                    if (_comparer.EqualsButNoAlpha(newColor, this._originalPicture.TransparentColor.Value))
                    {
                        newColor =
                                ColorBuffer.GetSlightlyDifferentColor(newColor);
                    }
                }
            }

            // Set the measurements of destination pixel
            int initialX = x;
            int initialY = y;
            int finalX = x + this._resolutionTax;
            int finalY = y + this._resolutionTax;

            PointRange pointRange = null;
            if (this._lastScannedPoint == null)
            {
                pointRange = new PointRange();
                pointRange.Color = newColor;
                this._translatedPixel.Add(pointRange);
            }
            else
            {
                if (this._lastScannedPoint.Color != newColor)
                {
                    pointRange = new PointRange();
                    pointRange.Color = newColor;
                    this._translatedPixel.Add(pointRange);
                }
                else
                {
                    pointRange = this._lastScannedPoint;
                }
            }
            pointRange.UpdatePoint(initialX, initialY, finalX, finalY);
            this._lastScannedPoint = pointRange;
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
		public Picture CreateTranslatedPicture()
        {
            // Copying picture
            Picture clonePicture = this._originalPicture.Clone(Picture.CloneMode.StructureOnly);

            try
            {
                clonePicture.BeginBatchUpdate();

                List<PointInfo> replacePixelList = new List<PointInfo>();

                // Creating point info
                foreach (var translatedPixelItem in this._translatedPixel)
                {
                    replacePixelList.AddRange(translatedPixelItem.ToPointInfoList());
                }

                clonePicture.SetPixel(replacePixelList);

                #region Fullfiled the lacks

                var lackPointList = from item in _originalPicture.GetAllPixels()
                                    where !clonePicture.Contains(item)
                                    select item;

                clonePicture.SetPixel(lackPointList.ToList());

                #endregion

                clonePicture.EndBatchUpdate();
            }
            catch (NullReferenceException)
            {
                // Errors this type can't interrupt the normal flow of works
            }
            catch (ArgumentException)
            {
                // Errors this type can't interrupt the normal flow of works
            }
            catch (Exception ex)
            {
                clonePicture.CancelBatchUpdate();
                throw ex;
            }

            return clonePicture;
        }

        /// <summary>
        /// Gets a indicator informing if the point is occupied.
        /// </summary>
        /// <returns></returns>
        private bool PointOcuppied(int x, int y, int resolutionTax)
        {
            if (this._lastScannedPoint == null)
            {
                return false;
            }

            // Inferring the line
            int finalLineRange = (int)this._lastScannedPoint.EndPoint.Y;
            // inferring the pixel
            int finalPixel = (int)this._lastScannedPoint.EndPoint.X;

            if (y < finalLineRange)
            {
                if (x < finalPixel)
                {
                    return true;
                }
            }

            return false;
        }

    }
}