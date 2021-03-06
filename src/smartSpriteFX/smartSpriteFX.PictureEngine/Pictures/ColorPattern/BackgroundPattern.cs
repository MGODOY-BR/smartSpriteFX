﻿
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.Pictures.PixelPatterns;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace smartSuite.smartSpriteFX.Pictures.ColorPattern
{
    /// <summary>
    /// It´s an object which studies the pixels to detect background pattern
    /// </summary>
    public class BackgroundPattern
    {
        /// <summary>
        /// It´s a comparator for colors
        /// </summary>
        private ColorEqualityComparer _colorComparer = new ColorEqualityComparer();

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
        /// Gets the replacement colors
        /// </summary>
        /// <returns></returns>
        internal Color GetReplacementColor(Piece piece, IAskingForColorDelegate askingForColorDelegate)
        {
            return this.GetReplacementColor(
                    piece.PointA,
                    piece.PointC,
                    piece.PointD,
                    piece.PointB,
                    piece,
                    askingForColorDelegate);
        }

        /// <summary>
        /// It´s a color to replace
        /// </summary>
        private List<Color> _replacementColorList = new List<Color>();

        /// <summary>
        /// Gets the transparentColor got after <see cref="DoTransparentBorder"/> method.
        /// </summary>
        public Color TransparentColor
        {
            get;
            private set;
        }

        /// <summary>
        /// Includes the coordinates to study the pattern
        /// </summary>
        /// <param name="x">It´s the x coordinate</param>
        /// <param name="y">It´s the y coordinate</param>
        /// <param name="color">It´s the color of pixel</param>
        public void Learn(int x, int y, Color color)
        {
            #region Entries validation

            #endregion

            PixelInfo pixelInfo = new PixelInfo()
            {
                X = x,
                Y = y,
                Color = color
            };

            var refPoint = new Point(x, y);

            #region TopLeft

            if (this._topLeft == null)
            {
                this._topLeft = refPoint;
            }
            else
            {
                if (refPoint.X <= this._topLeft.X &&
                    refPoint.Y <= this._topLeft.Y)
                {
                    this._topLeft = refPoint;
                }
            }

            #endregion

            #region TopRight

            if (this._topRight == null)
            {
                this._topRight = refPoint;
            }
            else
            {
                if (refPoint.Y == this._topLeft.Y &&
                    refPoint.X >= this._topRight.X &&
                    refPoint.X >= this._topLeft.X)
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
                if (refPoint.Y >= this._topLeft.Y &&
                    refPoint.X == this._topLeft.X)
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
                if (refPoint.Y == this._lowerLeft.Y &&
                    refPoint.X >= this._lowerLeft.X &&
                    refPoint.X >= this._lowerRight.X)
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
        /// Gets the X component from a key gotten from learntCahce
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetFromKeyX(String key)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            #endregion

            return int.Parse(key.Split('_')[0]);
        }

        /// <summary>
        /// Gets the Y component from a key gotten from learntCahce
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetFromKeyY(String key)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            #endregion

            return int.Parse(key.Split('_')[1]);
        }

        /// <summary>
        /// Does the transparent border
        /// </summary>
        /// <param name="piece">It´s a piece to deal.</param>
        /// <param name="askingForColorDelegate">An instance used to support the algorithm after an indefinition of a color.</param>
        public void DoTransparentBorder(Piece piece, IAskingForColorDelegate askingForColorDelegate)
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
            Picture pieceTakenPicture = Picture.GetInstance(piece.GetTakenPictureFullFileName());

            // Getting the replacement colors
            var transparentReplacementColor =
                this.GetReplacementColor(
                    piece.PointA,
                    piece.PointC,
                    piece.PointD,
                    piece.PointB,
                    piece,
                    askingForColorDelegate);

            this.TransparentColor = transparentReplacementColor;

            // Refreshing picture
            pieceTakenPicture.Overwrite(transparentReplacementColor);   // <-- TODO: Change replacementColor to put a pattern
        }

        /// <summary>
        /// Gets the replacement colors
        /// </summary>
        /// <returns></returns>
        private Color GetReplacementColor(
            Point topLeftPiece, 
            Point topRightPiece, 
            Point lowerLeftPiece, 
            Point lowerRightPiece,
            Piece piece,
            IAskingForColorDelegate askingForColorDelegate
            )
        {
            #region Entries validation

            if (askingForColorDelegate == null)
            {
                throw new ArgumentNullException("askingForColorDelegate");
            }
            if (piece == null)
            {
                throw new ArgumentNullException("piece");
            }
            var picture = piece.GetReferencePicture();
            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }

            #endregion

            List<Color> invalidColorList = new List<Color>();
            ColorFrequency horizontalColor = new ColorFrequency
            {
                Color = Color.Transparent,
                Frequency = 0
            };

            ColorFrequency verticalColor = new ColorFrequency
            {
                Color = Color.Transparent,
                Frequency = 0
            };

            var colorList = picture.GetAllColors();
            colorList.Sort(this._colorComparer);

            int counter = 0;
            while (counter < colorList.Count)
            {
                horizontalColor = this.GetHorizontalReplacementColor(picture, topLeftPiece, topRightPiece, invalidColorList);
                verticalColor = this.GetVerticalReplacementColor(picture, topLeftPiece, topRightPiece, invalidColorList);

                invalidColorList.Add(horizontalColor.Color);
                invalidColorList.Add(verticalColor.Color);

                counter++;

                #region New color validation

                if (horizontalColor.Color == Color.Transparent || verticalColor.Color == Color.Transparent)
                {
                    return Color.Transparent;
                }
                else
                {
                    if (horizontalColor.GetPercentage() > 25 && verticalColor.GetPercentage() > 25)
                    {
                        if (this._colorComparer.LooksLike(horizontalColor.Color, verticalColor.Color))
                        {
                            break;
                        }
                    }
                }

                #endregion
            }

            #region Handling with undefined color

            // TODO: It necessary to improve this condiction to avoid bad interpretation of background mistakes
            if (counter == colorList.Count && counter > 1) // <-- This means that there no common color among the axis. We need help from user
            {
                return
                    askingForColorDelegate.AnswerMe(piece, invalidColorList);
            }

            #endregion

            return horizontalColor.Color;
        }

        /// <summary>
        /// Gets a replacement color for horizontal coordinates
        /// </summary>
        /// <param name="invalidColorList">It´s a list of colors to ignore</param>
        /// <returns></returns>
        private ColorFrequency GetHorizontalReplacementColor(Picture picture, Point topLeftPiece, Point topRightPiece, List<Color> invalidColorList)
        {
            #region Entries validation

            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }
            if (topLeftPiece == null)
            {
                throw new ArgumentNullException("topLeft");
            }
            if (topRightPiece == null)
            {
                throw new ArgumentNullException("topRight");
            }

            if (this._topLeft == null)
            {
                throw new ArgumentNullException("this._topLeft");
            }
            if (this._topRight == null)
            {
                throw new ArgumentNullException("this._topRight");
            }

            #endregion

            Dictionary<Color, int> patternList = new Dictionary<Color, int>();
            ColorFrequency frequentlyColor = null;
            int counter = 0;

            // Getting the horizontal pattern
            // for (int x = (int)this._topLeft.X; x < (int)this._topRight.X; x++)
            for (int x = (int)topLeftPiece.X; x < (int)topRightPiece.X; x++)
            {
                // var color = picture.GetPixel(x, (int)this._topLeft.Y);
                var color = picture.GetPixel(x, (int)topLeftPiece.Y);

                #region Entries validation

                if (color == null)
                {
                    continue;
                }

                #endregion

                #region Applying filter to avoid ignored colors

                if (color == Color.Transparent)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = color.Value,
                        Frequency = 1
                    };
                    break;
                }
                if (invalidColorList.Contains(color.Value))
                {
                    continue;
                }

                #endregion

                #region Counting

                //if (!patternList.ContainsKey(pixelInfo.Color))
                var similarColor = this.GetSimilarColor(patternList.Keys, color.Value);
                if(!patternList.ContainsKey(similarColor))
                {
                    // patternList.Add(pixelInfo.Color, 1);
                    patternList.Add(similarColor, 1);
                }
                else
                {
                    // patternList[pixelInfo.Color]++;
                    patternList[similarColor]++;
                }
                counter++;

                #endregion

                #region Frequency color algoritmn

                if (frequentlyColor == null)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = color.Value,
                        Frequency = 1
                    };
                }

                // Testing the frequency
                //if (patternList[pixelInfo.Color] > frequentlyColor.Frequency)
                if (patternList[similarColor] > frequentlyColor.Frequency)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = color.Value,
                        // Frequency = patternList[pixelInfo.Color]
                        Frequency = patternList[similarColor]
                    };
                }

                #endregion

                #region Piece border validation

                if (x + 1 >= topLeftPiece.X)
                {
                    break;
                }

                #endregion
            }

            #region Entries validation

            if (frequentlyColor == null)
            {
                frequentlyColor = new ColorFrequency
                {
                    Color = Color.Transparent,
                    Frequency = 0,
                    Length = counter
                };
            }
            else
            {
                frequentlyColor.Length = counter;
            }

            #endregion

            return frequentlyColor;
        }

        /// <summary>
        /// Gets a color similar in collection
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="colorList"></param>
        /// <returns></returns>
        private Color GetSimilarColor(Dictionary<Color, int>.KeyCollection colorList, Color color)
        {
            #region Entries validation

            if (color == null)
            {
                throw new ArgumentNullException("color");
            }
            if (colorList == null)
            {
                throw new ArgumentNullException("colorList");
            }

            #endregion

            foreach (var item in colorList)
            {
                if (this._colorComparer.LooksLike(item, color))
                {
                    return item;
                }
            }

            return color;
        }

        /// <summary>
        /// Gets a replacement color for vertical coordinates
        /// </summary>
        /// <param name="invalidColorList">It´s a list of colors to ignore</param>
        /// <returns></returns>
        private ColorFrequency GetVerticalReplacementColor(Picture picture, Point lowerLeftPiece, Point lowerRightPiece, List<Color> invalidColorList)
        {
            #region Entries validation

            if (picture == null)
            {
                throw new ArgumentNullException("picture");
            }
            if (lowerLeftPiece == null)
            {
                throw new ArgumentNullException("lowerLeft");
            }
            if (lowerRightPiece == null)
            {
                throw new ArgumentNullException("lowerRight");
            }

            if (this._lowerLeft == null)
            {
                throw new ArgumentNullException("this._lowerLeft");
            }
            if (this._lowerRight == null)
            {
                throw new ArgumentNullException("this._lowerRight");
            }

            #endregion

            Dictionary<Color, int> patternList = new Dictionary<Color, int>();
            ColorFrequency frequentlyColor = null;
            int counter = 0;

            // Getting the horizontal pattern
            for (int y = (int)this._topLeft.Y - 1; y < (int)this._lowerLeft.Y + 1; y++)
            {
                #region Entries validation

                if (y < 0) continue;

                #endregion

                var color = picture.GetPixel((int)this._lowerLeft.X, y);

                #region Entries validation

                if (color == null)
                {
                    continue;
                }

                #endregion

                #region Applying filter to avoid ignored colors

                if (color.Value == Color.Transparent)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = color.Value,
                        Frequency = 1
                    };
                    break;
                }
                if (invalidColorList.Contains(color.Value))
                {
                    continue;
                }

                #endregion

                #region Counting

                if (!patternList.ContainsKey(color.Value))
                {
                    patternList.Add(color.Value, 1);
                }
                else
                {
                    patternList[color.Value]++;
                }
                counter++;

                #endregion

                #region Frequency color algoritmn

                if (frequentlyColor == null)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = color.Value,
                        Frequency = 1
                    };
                }

                // Testing the frequency
                if (patternList[color.Value] > frequentlyColor.Frequency)
                {
                    frequentlyColor = new ColorFrequency
                    {
                        Color = color.Value,
                        Frequency = patternList[color.Value]
                    };
                }

                #endregion
                
                #region Piece border validation

                if (y + 1 >= lowerLeftPiece.Y)
                {
                    break;
                }

                #endregion
            }

            #region Entries validation

            if (frequentlyColor == null)
            {
                return new ColorFrequency
                {
                    Color = Color.Transparent,
                    Frequency = 0,
                    Length = counter
                };
            }
            else
            {
                frequentlyColor.Length = counter;
            }

            #endregion

            return frequentlyColor;
        }

    }
}