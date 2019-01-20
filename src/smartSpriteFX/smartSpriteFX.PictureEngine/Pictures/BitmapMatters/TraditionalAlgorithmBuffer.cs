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
    /// Represents a traditional algorithm for loading buffer
    /// </summary>
    [Obsolete("This class is considered deprecated. Please use LockBufferAlgorithmBuffer set.")]
    public class TraditionalAlgorithmBuffer : ITraditionalAlgorithmBuffer
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

        public TraditionalAlgorithmBuffer(IPictureDatabase buffer, Bitmap image) : this(buffer, image, new NoneColorFilter())
        {
        }

        public TraditionalAlgorithmBuffer(IPictureDatabase buffer, Bitmap image, IColorFilter colorFilter)
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

            List<AutoResetEvent> semaphoreList = new List<AutoResetEvent>();
            ThreadPool.SetMinThreads(1, 500);
            ThreadPool.SetMaxThreads(2000, 20000);

            try
            {
                this.Buffer.BeginTransaction();

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        var color = image.GetPixel(x, y);

                        #region Filtering colors

                        if (!colorFilter.IsValid(x, y, color))
                        {
                            continue;
                        }

                        #endregion

                        AutoResetEvent sign = new AutoResetEvent(false);
                        semaphoreList.Add(sign);
                        object[] stateArgs = new object[4] { x, y, color, sign };

                        ThreadPool.QueueUserWorkItem(
                            new WaitCallback(delegate (object state)
                            {
                                object[] args = (object[])state;

                                int xx = (int)args[0];
                                int yy = (int)args[1];
                                Color myColor = (Color)args[2];
                                AutoResetEvent mySign = (AutoResetEvent)args[3];

                                try
                                {
                                    this.Buffer.INSERT(xx, yy, color);
                                }
                                finally
                                {
                                    mySign.Set();
                                }
                            }), 
                            stateArgs);
                    }
                }

                foreach (AutoResetEvent signItem in semaphoreList)
                {
                    signItem.WaitOne();
                }

                this.Buffer.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.Buffer.RollbackTransaction();
                throw ex;
            }

            this.ColorCount = this.Buffer.CountColor();
            this.Height = image.Height;
            this.Width = image.Width;
        }

        public IPictureDatabase CreatePictureDatabase()
        {
            var resultValue = PictureDatabase.Open();
            resultValue.CLEAR();
            return resultValue;
        }

        public void ReleaseBuffer()
        {
            this.Buffer.CLEAR();
        }
    }
}
