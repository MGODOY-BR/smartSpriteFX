using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System.Drawing;
using System.IO;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Core;
using System.Drawing.Imaging;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// It´s a filter used to reduce the too big image to fit in another filters
    /// </summary>
    public class ResizeToFitFilter : SmartSpriteOriginalFilterBase
    {
        /// <summary>
        /// It´s the width
        /// </summary>
        private int _screenWidth;
        /// <summary>
        /// It´s the height
        /// </summary>
        private int _screenHeight;

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Resize to fit");
            identification.SetDescription("A filter which resize image to lower dimensions, improving another filters, such the Old School Video Game filters.");
            identification.SetGroup("Work around");

            return identification;
        }

        public override bool ApplyFilter(Picture frame, int index)
        {
            String fullPath = frame.FullPath;

            var path = Path.GetDirectoryName(fullPath);
            var newPath = Path.Combine(path, "filtered");

            lock (typeof(ResizeToFitFilter))
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
            }

            var newFile = Path.Combine(newPath, Path.GetFileName(fullPath));

            Bitmap bitmap = new Bitmap(this._screenWidth, this._screenHeight, PixelFormat.Format32bppArgb);

            var hypotenuseHigher =
                Math.Sqrt(
                    Math.Pow(frame.Width, 2L) +
                    Math.Pow(frame.Height, 2L));

            var hypotenuseLower =
                Math.Sqrt(
                    Math.Pow(this._screenWidth, 2L) +
                    Math.Pow(this._screenHeight, 2L));

            var step = hypotenuseHigher / hypotenuseLower;

            float lastX = 0;
            float lastY = 0;
            int xx = 0, yy = 0;

            Dictionary<String, PointInfo> cacheList = new Dictionary<string, PointInfo>(); 
            var sourceList = frame.GetAllPixels();
            foreach (var sourceItem in sourceList)
            {
                String key = frame.FormatKey((int)sourceItem.X, (int)sourceItem.Y);
                cacheList.Add(key, sourceItem);
            }

            for (double y = 0; y < frame.Height; y+= step)
            {
                xx = 0;
                for (double x = 0; x < frame.Width; x+= step)
                {
                    String key = frame.FormatKey((int)x, (int)y);

                    var color = cacheList[key].Color;
                    if (xx < bitmap.Width && yy < bitmap.Height)
                    {
                        bitmap.SetPixel(
                            xx,
                            yy,
                            color);

                        lastX = xx;
                        lastY = yy;
                    }
                    xx++;
                }
                yy++;
            }
            bitmap.Save(newFile);

            frame.ReleaseBuffer();
            frame.LoadBuffer(bitmap);
            EffectEngine.ReloadPreviewBoard(newFile);
            return true;
        }

        public override void Reset()
        {
            this._screenWidth = 1366;
            this._screenHeight = 768;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new NoneConfigurationPanelControl();
        }
    }
}
