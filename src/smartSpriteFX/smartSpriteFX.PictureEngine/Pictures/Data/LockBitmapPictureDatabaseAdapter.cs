﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.PictureEngine.Pictures.BitmapMatters;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using static smartSuite.smartSpriteFX.PictureEngine.Pictures.BitmapMatters.LockBitmap;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.Data
{
    /// <summary>
    /// Represents an instance of PictureDatabase that is independent of intermidate cache
    /// </summary>
    public class LockBitmapPictureDatabaseAdapter : IPictureDatabase
    {
        /// <summary>
        /// It's a data source
        /// </summary>
        public LockBitmap DataSource { get; private set; }

        public void BeginTransaction()
        {
        }

        public void CLEAR()
        {
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)this.DataSource.Depth, this.DataSource);
            while (enumerator.MoveNext())
            {
                this.DataSource.SetPixel(
                    (int)enumerator.Current.X,
                    (int)enumerator.Current.Y,
                    Color.Empty);
            }
        }

        public IPictureDatabase Clone()
        {
            LockBitmapPictureDatabaseAdapter clone = new LockBitmapPictureDatabaseAdapter();
            clone.DataSource = this.DataSource;
            return clone;
        }

        public void Close()
        {
            this.DataSource.UnlockBits();
        }

        public void CommitTransaction()
        {
        }

        public long COUNT()
        {
            long returnValue = 0;
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)this.DataSource.Depth, this.DataSource);
            while (enumerator.MoveNext())
            {
                returnValue++;
            }
            return returnValue;
        }

        public long COUNT(Color color)
        {
            long returnValue = 0;
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)this.DataSource.Depth, this.DataSource);
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Color.ToArgb() != color.ToArgb()) continue;

                returnValue++;
            }
            return returnValue;
        }

        public long CountColor()
        {
            long returnValue = 0;
            Color lastColor = Color.Empty;
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)this.DataSource.Depth, this.DataSource);
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Color.ToArgb() == lastColor.ToArgb()) continue;

                lastColor = enumerator.Current.Color;
                returnValue++;
            }
            return returnValue;
        }

        public bool DELETE(int x, int y)
        {
            this.DataSource.SetPixel(x, y, Color.Empty);
            return true;
        }

        public bool EXISTS(smartSpriteFX.Pictures.Point point)
        {
            var pixel = this.DataSource.GetPixel((int)point.X, (int)point.Y);

            return pixel.ToArgb() != Color.Empty.ToArgb();
        }

        public bool EXISTS(smartSpriteFX.Pictures.Point point, Color color)
        {
            var pixel = this.DataSource.GetPixel((int)point.X, (int)point.Y);

            return pixel.ToArgb() != color.ToArgb();
        }

        public List<Color> GetAllColors()
        {
            Color lastColor = Color.Empty;
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)this.DataSource.Depth, this.DataSource);
            List<Color> returnValue = new List<Color>();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Color.ToArgb() == lastColor.ToArgb()) continue;

                lastColor = enumerator.Current.Color;
                returnValue.Add(lastColor);
            }
            return returnValue;
        }

        public void INSERT(int x, int y, Color color)
        {
            this.DataSource.SetPixel(x, y, color);
        }

        public void INSERT(List<PointInfo> pointInfoList)
        {
            pointInfoList.ForEach(p => this.DataSource.SetPixel((int)p.X, (int)p.Y, p.Color));
        }

        public void Merge(IPictureDatabase other)
        {
            var otherLockBitmap = (LockBitmap)other;
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)otherLockBitmap.Depth, otherLockBitmap);
            while (enumerator.MoveNext())
            {
                this.DataSource.SetPixel(
                    (int)enumerator.Current.X,
                    (int)enumerator.Current.Y,
                    enumerator.Current.Color);
            }
        }

        public void RollbackTransaction()
        {
        }

        public List<PointInfo> SELECT(Color color)
        {
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)this.DataSource.Depth, this.DataSource);
            List<PointInfo> returnValue = new List<PointInfo>();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Color.ToArgb() != color.ToArgb()) continue;

                returnValue.Add(
                    new PointInfo(
                        enumerator.Current.X,
                        enumerator.Current.Y,
                        enumerator.Current.Color));
            }
            return returnValue;
        }

        public ColorInfo SELECT(int x, int y)
        {
            var pixel = this.DataSource.GetPixel(x, y);
            return new ColorInfo(pixel);
        }

        public PointInfo SELECT(smartSpriteFX.Pictures.Point point)
        {
            return new PointInfo(
                point,
                this.DataSource.GetPixel((int)point.X, (int)point.Y));
        }

        public List<PointInfo> SELECTALL()
        {
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)this.DataSource.Depth, this.DataSource);
            List<PointInfo> returnValue = new List<PointInfo>();
            while (enumerator.MoveNext())
            {
                returnValue.Add(
                    new PointInfo(
                        enumerator.Current.X,
                        enumerator.Current.Y,
                        enumerator.Current.Color));
            }
            return returnValue;
        }

        public int UPDATE(Color color, Color replaceColor)
        {
            int numAffect = 0;
            LockBitmap.LockBitmapEnumerator enumerator = new LockBitmap.LockBitmapEnumerator((DepthEnum)this.DataSource.Depth, this.DataSource);
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Color.ToArgb() != color.ToArgb()) continue;

                this.DataSource.SetPixel(
                    (int)enumerator.Current.X,
                    (int)enumerator.Current.Y,
                    replaceColor);

                numAffect++;
            }
            return numAffect;
        }

        public int UPDATE(int x, int y, Color color)
        {
            this.DataSource.SetPixel(x, y, color);
            return 1;
        }

        /// <summary>
        /// Creates and returns an instance of picture data base based on lock bitmap
        /// </summary>
        /// <returns></returns>
        internal static IPictureDatabase Open(LockBitmap dataSource)
        {
            var resultValue = new LockBitmapPictureDatabaseAdapter();
            resultValue.DataSource = dataSource;
            return resultValue;
        }
    }
}
