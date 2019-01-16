using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.SpriteSheet
{
    /// <summary>
    /// Represents a maker of sprite sheet
    /// </summary>
    public class SpriteSheetMaker
    {
        /// <summary>
        /// It's the organizer
        /// </summary>
        private SpriteFileListOrganizer _organizer;

        /// <summary>
        /// Creates an instance of object
        /// </summary>
        /// <param name="organizer"></param>
        public SpriteSheetMaker(SpriteFileListOrganizer organizer)
        {
            #region Entries validation

            if (organizer == null)
            {
                throw new ArgumentNullException("organizer");
            }

            #endregion

            this._organizer = organizer;
        }

        /// <summary>
        /// Generates the SpriteMaker
        /// </summary>
        public void Generate(string spriteSheetFileName)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(spriteSheetFileName))
            {
                throw new ArgumentNullException("spriteSheetFileName");
            }

            #endregion

            this._organizer.Load();

            var fileCount = this._organizer.FileCount;
            var matrixInfo = this._organizer.CalculateSpriteSheet();
            var largerSprite = this._organizer.LargerSprite;
            Dimension spriteSheetDimension = new Dimension
            {
                Width = largerSprite.Width * matrixInfo.ColumnCount,
                Height = largerSprite.Height * matrixInfo.RowCount
            };
            float uvHorizontal = 1f / (float)matrixInfo.ColumnCount;
            float uvVertical = 1f / (float)matrixInfo.RowCount;

            using (Image spriteSheet = new Bitmap((int)spriteSheetDimension.Width, (int)spriteSheetDimension.Height, PixelFormat.Format32bppArgb))
            {
                using (var streamUVInfo = this.GetStreamUVInfo(spriteSheetFileName))
                {
                    streamUVInfo.WriteLine("INDEX;UV(X);UV(Y)");

                    int generalIndex = 0;
                    int currentColumn = 0;
                    int currentRow = 1;
                    Point cursor = new Point { X = 0, Y = 0 };
                    var drawer = this.GetDrawerDelegate(spriteSheet);

                    foreach (var imagePathItem in this._organizer.ListImages())
                    {
                        #region Matrix management

                        #region Column managing

                        if (currentColumn < matrixInfo.ColumnCount)
                        {
                            currentColumn++;
                        }
                        else
                        {
                            #region Row managing

                            if (currentRow < matrixInfo.RowCount)
                            {
                                currentRow++;
                                currentColumn = 1;
                            }
                            else
                            {
                                break;
                            }

                            #endregion
                        }

                        #endregion

                        #endregion

                        #region Position management

                        cursor.X = (currentColumn - 1) * (int)largerSprite.Width;
                        cursor.Y = (currentRow - 1) * (int)largerSprite.Height;

                        RectangleF areaToDraw = new RectangleF(cursor.X, cursor.Y, largerSprite.Width, largerSprite.Height);

                        #endregion

                        #region Copying and pasting

                        using (Image frame = drawer.GetImage(imagePathItem))
                        {
                            this.CopyFrameToSpriteSheet(frame, drawer, areaToDraw);
                        }

                        #endregion

                        streamUVInfo.WriteLine(
                            "{0};{1};{2}",
                            generalIndex,
                            (currentColumn * uvHorizontal).ToString(CultureInfo.InvariantCulture),
                            (currentRow * uvVertical).ToString(CultureInfo.InvariantCulture));

                        generalIndex++;
                    }

                    drawer.Save(spriteSheetFileName);
                }
            }

            // Generating info file
            using (var streamText = this.GetStreamText(spriteSheetFileName))
            {
                streamText.WriteLine("".PadRight(20, '-'));
                streamText.WriteLine("Matrix info");
                streamText.WriteLine("".PadRight(20, '-'));
                streamText.WriteLine("Number of columns: {0}", matrixInfo.ColumnCount);
                streamText.WriteLine("Number of rows: {0}", matrixInfo.RowCount);
                streamText.WriteLine("".PadRight(20, '-'));

                streamText.WriteLine();
                streamText.WriteLine("".PadRight(20, '-'));
                streamText.WriteLine("Larger sprite");
                streamText.WriteLine("".PadRight(20, '-'));
                streamText.WriteLine("width: {0}", largerSprite.Width.ToString(CultureInfo.InvariantCulture));
                streamText.WriteLine("height: {0}", largerSprite.Height.ToString(CultureInfo.InvariantCulture));
                streamText.WriteLine("".PadRight(20, '-'));

                streamText.WriteLine();
                streamText.WriteLine("".PadRight(20, '-'));
                streamText.WriteLine("UV");
                streamText.WriteLine("".PadRight(20, '-'));
                streamText.WriteLine("width: {0}", uvHorizontal.ToString(CultureInfo.InvariantCulture));
                streamText.WriteLine("height: {0}", uvVertical.ToString(CultureInfo.InvariantCulture));
                streamText.WriteLine("".PadRight(20, '-'));
                streamText.WriteLine();
                streamText.WriteLine("".PadRight(20, '-'));
                streamText.WriteLine("Total: {0} sprites", this._organizer.FileCount);
            }
        }

        /// <summary>
        /// Gets a stream for info txt file
        /// </summary>
        /// <param name="spriteSheetFileName"></param>
        /// <returns></returns>
        public virtual StreamWriter GetStreamText(string spriteSheetFileName)
        {
            return File.CreateText(spriteSheetFileName + ".info.txt");
        }

        /// <summary>
        /// Gets a stream for info csv file
        /// </summary>
        /// <param name="spriteSheetFileName"></param>
        /// <returns></returns>
        public virtual StreamWriter GetStreamUVInfo(string spriteSheetFileName)
        {
            return File.CreateText(spriteSheetFileName + ".uv.info.csv");
        }

        /// <summary>
        /// Copies frame to position in frame
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="drawer"></param>
        /// <param name="areaToDraw"></param>
        private void CopyFrameToSpriteSheet(Image frame, DrawerDelegate drawer, RectangleF areaToDraw)
        {
            drawer.DrawImage(frame, areaToDraw);
        }

        /// <summary>
        /// Gets the drawer delegate
        /// </summary>
        /// <returns></returns>
        public virtual DrawerDelegate GetDrawerDelegate(Image spriteSheet)
        {
            return new DrawerDelegate(spriteSheet);
        }

    }
}
