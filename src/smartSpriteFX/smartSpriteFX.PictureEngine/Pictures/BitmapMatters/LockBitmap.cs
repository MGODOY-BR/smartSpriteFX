﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.BitmapMatters
{
    /// <summary>
    /// Offers an alternative way to work with bitmaps faster
    /// </summary>
    /// <author>Vano Maisuradze</author>
    /// <remarks>
    /// This code was caught from https://www.codeproject.com/Tips/240428/Work-with-Bitmaps-Faster-in-Csharp-3
    /// 
    /// This code is under license of CPOL - https://www.codeproject.com/info/cpol10.aspx
    /// <para>
    /// Revisions
    /// <note type="note">
    /// 2019-10-16 -> [mgodoy-br] It was included ColorComponentEnum, DepthEnum and LockBitmapEnumerator
    /// </note>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// <![CDATA[Bitmap bmp = (Bitmap)Image.FromFile("d:\\source.png");
    /// LockBitmap lockBitmap = new LockBitmap(bmp);
    /// lockBitmap.LockBits();
    /// 
    /// Color compareClr = Color.FromArgb(255, 255, 255, 255);
    /// for (int y = 0; y < lockBitmap.Height; y++)
    /// {
    ///     for (int x = 0; x < lockBitmap.Width; x++)
    ///     {
    ///         if (lockBitmap.GetPixel(x, y) == compareClr)
    ///         {
    ///             lockBitmap.SetPixel(x, y, Color.Red);
    ///         }
    ///     }
    /// }
    /// lockBitmap.UnlockBits();
    /// bmp.Save("d:\\result.png");]]>
    /// </code>
    /// </example>
    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        /// Creates an instance of the object
        /// </summary>
        /// <param name="source"></param>
        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                int PixelCount = Width * Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat);

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }

        /// <summary>
        /// Relates the color component, according the Pixels array
        /// </summary>
        /// <author>mgodoy-br</author>
        /// <remarks>
        /// </remarks>
        public enum ColorComponentEnum
        {
            B = 0,
            G = 1,
            R = 2,
            A = 3,
        }

        /// <summary>
        /// Relates the depth options, according to file
        /// </summary>
        /// <author>mgodoy-br</author>
        /// <remarks>
        /// </remarks>
        public enum DepthEnum
        {
            _8 = 8,
            _24 = 24,
            _32 = 32,
        }

        /// <summary>
        /// Offers an enumerator to interpretate the Pixels array
        /// </summary>
        /// <author>mgodoy-br</author>
        /// <remarks>
        /// </remarks>
        public class LockBitmapEnumerator : IEnumerator<PointInfo>
        {
            /// <summary>
            /// It's the current cursor
            /// </summary>
            private int _cursor;

            /// <summary>
            /// It's the original data source
            /// </summary>
            public LockBitmap DataSource { get; private set; }

            /// <summary>
            /// It's the depth of bitmap
            /// </summary>
            public DepthEnum DepthEnum { get; private set; }

            /// <summary>
            /// Gets the current item
            /// </summary>
            public PointInfo Current { get; private set; }

            object IEnumerator.Current => (PointInfo)this.Current;

            /// <summary>
            /// Creates an instance of object
            /// </summary>
            /// <param name="depthEnum"></param>
            /// <param name="dataSource"></param>
            public LockBitmapEnumerator(DepthEnum depthEnum, LockBitmap dataSource)
            {
                this.DepthEnum = depthEnum;
                this.DataSource = dataSource;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                switch (this.DepthEnum)
                {
                    case DepthEnum._8:
                        this._cursor++;
                        break;
                    case DepthEnum._24:
                        this._cursor += 3;
                        break;
                    case DepthEnum._32:
                        this._cursor += 4;
                        break;
                    default:
                        throw new NotSupportedException("The value for depth is not supported: " + this.DepthEnum);
                }

                if (this._cursor > this.DataSource.Pixels.Length || this._cursor < 0)
                {
                    this.Current = null;
                    return false;
                }
                else
                {
                    int c = this.DataSource.Depth / 8;
                    int i = this._cursor;
                    int w = this.DataSource.Width;
                    int y = (i / c) / w;
                    int x = (i / c) - (y * w);

                    this.Current = new PointInfo(x, y, this.DataSource.GetPixel(x, y));
                }

                return true;
            }

            public void Reset()
            {
                this._cursor = 0;
                this.Current = null;
            }
        }
    }
}
