using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter capable to scale images
    /// </summary>
    public class ScaleFilter : SmartSpriteOriginalFilterBase, IScaleOrientedObject
    {
        /// <summary>
        /// Sets or gets the scale
        /// </summary>
        public float Scale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Scale");
            identification.SetDescription("A filter which scales the size of image.");
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

            int widthScaled =
                (int)Math.Round(
                    frame.Width * this.Scale, 0, MidpointRounding.AwayFromZero);
            int heightScaled =
                (int)Math.Round(
                    frame.Height * this.Scale, 0, MidpointRounding.AwayFromZero);

            Bitmap bitmap = new Bitmap(widthScaled, heightScaled, PixelFormat.Format32bppArgb);

            Dictionary<String, PointInfo> cacheList = new Dictionary<string, PointInfo>();
            var sourceList = frame.GetAllPixels();
            foreach (var sourceItem in sourceList)
            {
                String key = frame.FormatKey((int)sourceItem.X, (int)sourceItem.Y);
                cacheList.Add(key, sourceItem);
            }

            var step = Math.Pow(this.Scale, -1);
            int xx = 0, yy = 0;

            for (double y = 0; y < frame.Height; y += step)
            {
                xx = 0;
                for (double x = 0; x < frame.Width; x += step)
                {
                    String key = frame.FormatKey((int)x, (int)y);

                    var color = cacheList[key].Color;
                    if (xx < bitmap.Width && yy < bitmap.Height)
                    {
                        bitmap.SetPixel(
                            xx,
                            yy,
                            color);
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
            this.Scale = 1;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ScaleConfigurationPanelControl(0.01F, 2F);
        }
    }
}
