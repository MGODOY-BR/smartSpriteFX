using smartSuite.smartSpriteFX.PictureEngine.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.BitmapMatters
{
    /// <summary>
    /// Represents an algorithm specified in lock bitmap for loading buffer
    /// </summary>
    /// <see cref="LockBitmap"/>
    public class LockBufferAlgorithmBuffer : ITraditionalAlgorithmBuffer
    {
        /// <summary>
        /// It's the buffer
        /// </summary>
        public IPictureDatabase Buffer { get; private set; }

        /// <summary>
        /// Gets the color count
        /// </summary>
        public long ColorCount { get; private set; }

        /// <summary>
        /// It's the height of image
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// It's the width of image
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// It's the underlying lock bitmap
        /// </summary>
        private LockBitmap _lockBitmap;

        public LockBufferAlgorithmBuffer(IPictureDatabase buffer, Bitmap image) : this(buffer, image, new NoneColorFilter())
        {
        }

        public LockBufferAlgorithmBuffer(IPictureDatabase buffer, Bitmap image, IColorFilter colorFilter)
        {
            this.Buffer = buffer;

            #region Entries validation

            if (image == null)
            {
                throw new ArgumentNullException("image");
            }
            if (this.Buffer != null)
            {
                return;
            }

            #endregion

            this._lockBitmap = new LockBitmap(image);
            this._lockBitmap.LockBits();

            this.Buffer = this.CreatePictureDatabase();
            this.ColorCount = this.Buffer.CountColor();
            this.Height = image.Height;
            this.Width = image.Width;
        }

        public IPictureDatabase CreatePictureDatabase()
        {
            return LockBitmapPictureDatabaseAdapter.Open(this._lockBitmap);
        }

        public void ReleaseBuffer()
        {
            // HACK: Do not clear the buffer!
        }
    }
}
