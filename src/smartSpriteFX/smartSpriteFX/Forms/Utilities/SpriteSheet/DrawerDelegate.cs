using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.SpriteSheet
{
    /// <summary>
    /// Represents a actor to handle the object graphic
    /// </summary>
    public class DrawerDelegate
    {
        /// <summary>
        /// It's the underlying graphic object
        /// </summary>
        private Graphics _graphic;
        /// <summary>
        /// It's the image to save
        /// </summary>
        private Image _image;

        /// <summary>
        /// Creates an instance of object
        /// </summary>
        /// <param name="image"></param>
        public DrawerDelegate(Image image)
        {
            #region Entries validation

            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            #endregion

            this._graphic = Graphics.FromImage(image);
            this._image = image;
        }

        /// <summary>
        /// Draw the image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rect"></param>
        public virtual void DrawImage(Image image, RectangleF rect)
        {
            try
            {
                this._graphic.DrawImage(image, rect);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error to draw the image. The original error was: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the image from path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual Image GetImage(string path)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            #endregion

            try
            {
                return Image.FromFile(path);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error to read " + path + ". The original error was: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Saves the picture
        /// </summary>
        public virtual void Save(string fileName)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            #endregion

            try
            {
                this._image.Save(
                    Path.ChangeExtension(
                        fileName,
                        ".smartSpriteFXSpriteSheet.png"),
                    ImageFormat.Png);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error to generate " + fileName + ". The original error was: " + ex.Message, ex);
            }
        }
    }
}
