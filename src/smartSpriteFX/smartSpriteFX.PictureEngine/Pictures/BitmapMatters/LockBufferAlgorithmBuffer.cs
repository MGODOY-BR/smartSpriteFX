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
            if (this.Buffer != null && this.Buffer.COUNT() > 0)
            {
                return;
            }

            #endregion

            this.Buffer = this.CreatePictureDatabase();

            //List<AutoResetEvent> semaphoreList = new List<AutoResetEvent>();
            //ThreadPool.SetMinThreads(1, 500);
            //ThreadPool.SetMaxThreads(2000, 20000);

            this._lockBitmap = new LockBitmap(image);
            this._lockBitmap.LockBits();

            try
            {
                this.Buffer.BeginTransaction();

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        // var color = image.GetPixel(x, y);
                        var color = this._lockBitmap.GetPixel(x, y);

                        #region Filtering colors

                        if (!colorFilter.IsValid(x, y, color))
                        {
                            continue;
                        }

                        #endregion

                        AutoResetEvent sign = new AutoResetEvent(false);
                        //semaphoreList.Add(sign);
                        object[] stateArgs = new object[4] { x, y, color, sign };

                        //ThreadPool.QueueUserWorkItem(
                        var waitCallbackDelegate = 
                            new WaitCallback(delegate (object state)
                            {
                                object[] args = (object[])state;

                                int xx = (int)args[0];
                                int yy = (int)args[1];
                                Color myColor = (Color)args[2];
                                // AutoResetEvent mySign = (AutoResetEvent)args[3];

                                try
                                {
                                    this.Buffer.INSERT(xx, yy, color);
                                }
                                finally
                                {
                                    // mySign.Set();
                                }
                            });
                        // stateArgs);

                        waitCallbackDelegate(stateArgs);
                    }
                }

                //foreach (AutoResetEvent signItem in semaphoreList)
                //{
                //    signItem.WaitOne();
                //}

                this.Buffer.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.Buffer.RollbackTransaction();
                throw ex;
            }
            finally
            {
                this._lockBitmap.UnlockBits();
            }

            this.ColorCount = this.Buffer.CountColor();
            this.Height = image.Height;
            this.Width = image.Width;
        }

        public IPictureDatabase CreatePictureDatabase()
        {
            return LockBitmapPictureDatabaseAdapter.Open(this._lockBitmap);
        }
    }
}
