using smartSuite.smartSpriteFX.SpriteEffectModule.Animations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.SpriteSheet
{
    /// <summary>
    /// Represents an analysis of a lsit of sprite
    /// </summary>
    public class SpriteFileListOrganizer
    {
        /// <summary>
        /// It's the folder path
        /// </summary>
        private string _folderPath;

        /// <summary>
        /// Gets the larger dimensions of sprite
        /// </summary>
        private Dimension _largerSprite = new Dimension();

        /// <summary>
        /// Creates an instance of object
        /// </summary>
        /// <param name="folderPath"></param>
        public SpriteFileListOrganizer(string folderPath)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(folderPath))
            {
                throw new ArgumentNullException("folderPath");
            }

            #endregion

            this._folderPath = folderPath;
        }

        /// <summary>
        /// Gets the dimension of image
        /// </summary>
        public virtual Dimension LargerSprite { get => _largerSprite; }

        /// <summary>
        /// Gets the amount of files
        /// </summary>
        public virtual int FileCount { get; private set; }

        /// <summary>
        /// Loads the folderPath 
        /// </summary>
        public virtual void Load()
        {
            try
            {
                IEnumerable<string> fileList = this.ListImages();
                foreach (var fileItem in fileList)
                {
                    Dimension image = this.GetImageDimension(fileItem);

                    #region Summarizes the images

                    if (image.Width > this._largerSprite.Width)
                    {
                        this._largerSprite.Width = image.Width;
                    }
                    if (image.Height > this._largerSprite.Height)
                    {
                        this._largerSprite.Height = image.Height;
                    }

                    #endregion

                    this.FileCount++;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error to loads organizer. The original error was " + ex.Message, ex);
            }
        }

        /// <summary>
        /// List the images of folder path
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> ListImages()
        {
            List<string> returnValue = new List<string>();
            returnValue.AddRange(Directory.EnumerateFiles(this._folderPath, "*.png"));
            returnValue.AddRange(Directory.EnumerateFiles(this._folderPath, "*.bmp"));
            returnValue.AddRange(Directory.EnumerateFiles(this._folderPath, "*.jpg"));
            returnValue.AddRange(Directory.EnumerateFiles(this._folderPath, "*.jpeg"));

            returnValue.RemoveAll(x => x.EndsWith("smartSpriteFXSpriteSheet.png"));

            returnValue.Sort(new AnimationComparer());

            return returnValue;
        }

        /// <summary>
        /// Gets the image dimension
        /// </summary>
        /// <param name="fileItem"></param>
        /// <returns></returns>
        public virtual Dimension GetImageDimension(string fileItem)
        {
            try
            {
                using (var image = Image.FromFile(fileItem))
                {
                    return new Dimension
                    {
                        Width = image.Width,
                        Height = image.Height
                    };
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error to get image dimention of " + fileItem + ". The original error was " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Calculates the best sprite sheet possible
        /// </summary>
        public virtual SpriteMatrixInfo CalculateSpriteSheet()
        {
            #region Entries validation

            if(this.FileCount == 0)
            {
                throw new ApplicationException("Invalid file length. Check if load has been called earlier");
            }

            #endregion
            SpriteMatrixInfo returnValue = new SpriteMatrixInfo();

            if (this.FileCount <= 16)
            {
                returnValue.ColumnCount = this.FileCount / 4;
                if (this.FileCount % 4 > 0)
                {
                    returnValue.ColumnCount++;
                }
                returnValue.RowCount = this.FileCount / returnValue.ColumnCount;
                if(this.FileCount % returnValue.ColumnCount > 0)
                {
                    returnValue.RowCount++;
                }
            }
            else
            {
                if (this.FileCount > 50)    // <-- The .NET engine of creation of PNG doesn't suport images with too short width against too long height. We need balance that.
                {
                    returnValue.ColumnCount = this.FileCount / 100;
                    if (this.FileCount % 100 > 0)
                    {
                        returnValue.ColumnCount++;
                    }
                }
                else
                {
                    returnValue.ColumnCount = 10;
                }

                returnValue.RowCount = this.FileCount / returnValue.ColumnCount;
                if(this.FileCount % returnValue.ColumnCount > 0)
                {
                    returnValue.RowCount++;
                }
            }

            return returnValue;
        }
    }
}
