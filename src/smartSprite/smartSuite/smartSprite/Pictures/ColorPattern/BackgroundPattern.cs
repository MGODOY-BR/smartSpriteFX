
using smartSprite.Pictures.ColorPattern;
using smartSuite.smartSprite.Pictures.PixelPatterns;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace smartSuite.smartSprite.Pictures.ColorPattern
{
    /// <summary>
    /// It´s an object which studies the pixels to detect background pattern
    /// </summary>
    public class BackgroundPattern
    {
        /// <summary>
        /// It´s the cache of pixel information
        /// </summary>
        private Dictionary<String, PixelInfo> _learntCache = new Dictionary<string, PixelInfo>();

        /// <summary>
        /// A comparator for colors
        /// </summary>
        private ColorEqualityComparer _colorComparer = new ColorEqualityComparer();

        /// <summary>
        /// It´s a list of learnt colors
        /// </summary>
        private HashSet<Color> _learntColors = new HashSet<Color>();

        /// <summary>
        /// It´s the point of left coordinate
        /// </summary>
        private Point _topLeft;

        /// <summary>
        /// It´s the top of right coordinate
        /// </summary>
        private Point _topRight;

        /// <summary>
        /// It´s the lower left coordinate
        /// </summary>
        private Point _lowerLeft;

        /// <summary>
        /// It´s the lower right coordinate
        /// </summary>
        private Point _lowerRight;

        /// <summary>
        /// It´s a color to replace
        /// </summary>
        private List<Color> _replacementColorList = new List<Color>();

        /// <summary>
        /// Includes the coordinates to study the pattern
        /// </summary>
        /// <param name="x">It´s the x coordinate</param>
        /// <param name="y">It´s the y coordinate</param>
        /// <param name="color">It´s the color of pixel</param>
        public void Learn(int x, int y, Color color)
        {
            #region Entries validation

            if (
                this._lowerLeft != null &&
                this._lowerRight != null &&
                this._topLeft != null &&
                this._topRight != null)
            {
                // Stop learning, boarder are detected already
                return;
            }

            #endregion

            PixelInfo pixelInfo = new PixelInfo()
            {
                X = x,
                Y = y,
                Color = color
            };

            // Updating the cash of pixels
            this._learntCache.Add(this.FormatKey(x, y), pixelInfo);

            // Updating the cash of colors
            if (!this._learntColors.Contains(color, this._colorComparer))
            {
                this._learntColors.Add(color);
            }

            var refPoint = new Point(x, y);

            #region TopLeft

            if (this._topLeft == null)
            {
                this._topLeft = refPoint;
                this._lowerLeft = refPoint;
            }
            else
            {
                if (this._topLeft.CompareTo(refPoint) == 1)
                {
                    this._topLeft = refPoint;
                }
            }

            #endregion

            #region TopRight

            if (this._topRight == null)
            {
                this._topRight = refPoint;
                this._lowerRight = refPoint;
            }
            else
            {
                if (this._topRight.CompareTo(refPoint) == -1)
                {
                    this._topRight = refPoint;
                }
            }

            #endregion

            #region LowerLeft

            if (this._lowerLeft == null)
            {
                this._lowerLeft = refPoint;
            }
            else
            {
                if (this._lowerLeft.CompareTo(refPoint) == -1)
                {
                    this._lowerLeft = refPoint;
                }
            }

            #endregion

            #region LowerRight

            if (this._lowerRight == null)
            {
                this._lowerRight = refPoint;
            }
            else
            {
                if (this._lowerRight.CompareTo(refPoint) == -1)
                {
                    this._lowerRight = refPoint;
                }
            }

            #endregion
        }

        /// <summary>
        /// Returns a key ready for be included on learntCache
        /// </summary>
        /// <param name="x">It´s the x coordinate</param>
        /// <param name="y">It´s the y coordinate</param>
        /// <returns></returns>
        private string FormatKey(int x, int y)
        {
            return x + "_" + y;
        }

        /// <summary>
        /// Does the transparent border
        /// </summary>
        /// <param name="piece">It´s a piece to deal.</param>
        public void DoTransparentBorder(Piece piece)
        {
            #region Entries validation

            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }
            if (this._topLeft == null && this._topRight == null && this._lowerLeft == null && this._lowerRight == null)
            {
                return;
            }

            #endregion

            // Getting the taken picture
            Picture takenPicture = new Picture(piece.GetTakenPictureFullFileName());

            // Getting the replacement colors
            var replacementColor =
                this.GetReplacementColor(
                    this._learntCache,
                    piece.PointA,
                    piece.PointC,
                    piece.PointD,
                    piece.PointB);

            for (int y = (int)piece.PointA.Y; y < piece.PointB.Y; y++)
            {
                // Scanning the taken picture
                for (int x = (int)piece.PointA.X; x < piece.PointB.X; x++)
                {
                    try
                    {
                        // Getting pixel
                        var pixel = takenPicture.GetPixel(x, y);

                        // Checking if it is replacement color
                        if (!this._colorComparer.Equals(replacementColor, pixel))
                        {
                            break;
                        }

                        // Replacing picture
                        takenPicture.ReplacePixel(x, y, pixel);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                }
            }

            // Refreshing picture
            takenPicture.Overwrite(replacementColor);
        }

        /// <summary>
        /// Gets the replacement colors
        /// </summary>
        /// <returns></returns>
        private Color GetReplacementColor(Dictionary<String, PixelInfo> learntCache, Point topLeft, Point topRight, Point lowerLeft, Point lowerRight)
        {
            List<Color> invalidColorList = new List<Color>();
            Color horizontalColor = Color.Transparent;
            Color verticalColor = Color.Transparent;

            int counter = 0;

            while (
                !this._colorComparer.Equals(horizontalColor, verticalColor) && 
                counter < this._learntColors.Count)
            {
                horizontalColor = this.GetHorizontalReplacementColor(learntCache, topLeft, topRight, invalidColorList);
                verticalColor = this.GetVerticalReplacementColor(learntCache, topLeft, topRight, invalidColorList);

                invalidColorList.Add(horizontalColor);
                invalidColorList.Add(verticalColor);

                counter++;
            }

            return horizontalColor;
        }

        /// <summary>
        /// Gets a replacement color for horizontal coordinates
        /// </summary>
        /// <param name="invalidColorList">It´s a list of colors to ignore</param>
        /// <returns></returns>
        private Color GetHorizontalReplacementColor(Dictionary<String, PixelInfo> learntCache, Point topLeft, Point topRight, List<Color> invalidColorList)
        {
            #region Entries validation

            if (learntCache == null)
            {
                throw new ArgumentNullException("learntCache");
            }
            if (topLeft == null)
            {
                throw new ArgumentNullException("topLeft");
            }
            if (topRight == null)
            {
                throw new ArgumentNullException("topRight");
            }

            #endregion

            Dictionary<Color, int> patternList = new Dictionary<Color, int>();
            ColorFrequency frequentlyColor = null;

            // Getting the horizontal pattern
            for (int x = (int)topLeft.X; x < (int)topRight.X; x++)
            {
                var key = this.FormatKey(x, (int)topLeft.Y);

                #region Entries validation

                if (!learntCache.ContainsKey(key))
                {
                    continue;
                }

                #endregion

                var pixelInfo = learntCache[key];

                #region Applying filter to avoid ignored colors

                if (invalidColorList.Contains(pixelInfo.Color))
                {
                    continue;
                }

                #endregion

                #region Counting

                if (!patternList.ContainsKey(pixelInfo.Color))
                {
                    patternList.Add(pixelInfo.Color, 1);
                }
                else
                {
                    patternList[pixelInfo.Color]++;
                }

                #endregion

                if (frequentlyColor == null)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = pixelInfo.Color,
                        Frequency = 1
                    };
                }

                // Testing the frequency
                if (patternList[pixelInfo.Color] > frequentlyColor.Frequency)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = pixelInfo.Color,
                        Frequency = patternList[pixelInfo.Color]
                    };
                }
            }

            #region Entries validation

            if (frequentlyColor == null)
            {
                // throw new ArgumentNullException("frequentlyColor");
                frequentlyColor = new ColorFrequency
                {
                    Color = Color.Black,
                    Frequency = 1
                };
            }

            #endregion

            return frequentlyColor.Color;
        }

        /// <summary>
        /// Gets a replacement color for vertical coordinates
        /// </summary>
        /// <param name="invalidColorList">It´s a list of colors to ignore</param>
        /// <returns></returns>
        private Color GetVerticalReplacementColor(Dictionary<String, PixelInfo> learntCache, Point lowerLeft, Point lowerRight, List<Color> invalidColorList)
        {
            #region Entries validation

            if (learntCache == null)
            {
                throw new ArgumentNullException("learntCache");
            }
            if (lowerLeft == null)
            {
                throw new ArgumentNullException("lowerLeft");
            }
            if (lowerRight == null)
            {
                throw new ArgumentNullException("lowerRight");
            }

            #endregion

            Dictionary<Color, int> patternList = new Dictionary<Color, int>();
            ColorFrequency frequentlyColor = null;

            // Getting the horizontal pattern
            for (int i = (int)lowerLeft.Y - 1; i < (int)lowerRight.Y + 1; i++)
            {
                var key = this.FormatKey((int)lowerLeft.X, i);

                #region Entries validation

                if (!learntCache.ContainsKey(key))
                {
                    continue;
                }

                #endregion

                var pixelInfo = learntCache[key];

                #region Applying filter to avoid ignored colors

                if (invalidColorList.Contains(pixelInfo.Color))
                {
                    continue;
                }

                #endregion

                #region Counting

                if (!patternList.ContainsKey(pixelInfo.Color))
                {
                    patternList.Add(pixelInfo.Color, 1);
                }
                else
                {
                    patternList[pixelInfo.Color]++;
                }

                #endregion

                if (frequentlyColor == null)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = pixelInfo.Color,
                        Frequency = 1
                    };
                }

                // Testing the frequency
                if (patternList[pixelInfo.Color] > frequentlyColor.Frequency)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = pixelInfo.Color,
                        Frequency = patternList[pixelInfo.Color]
                    };
                }
            }

            #region Entries validation

            if (frequentlyColor == null)
            {
                // throw new ArgumentNullException("frequentlyColor");
                return Color.Transparent;
            }

            #endregion

            return frequentlyColor.Color;
        }

    }
}