using smartSuite.smartSpriteFX.Effects.Filters;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter capable to expand the frame, adding a mirror image from original
    /// </summary>
    public class MirrorFilter : SmartSpriteOriginalFilterBase
    {
        public override bool ApplyFilter(Picture frame, int index)
        {
            var originalWidth = frame.Width;
            var newWidth = originalWidth * 2;
            frame.Width = newWidth;

            // Filling the rest of empty spaces
            int originalX = originalWidth;
            List<PointInfo> pointInfoList = new List<PointInfo>();
            for (int x = originalWidth; x < newWidth; x++)
            {
                originalX--; if (originalX == -1) originalX = originalWidth;

                for (int y = 0; y < frame.Height; y++)
                {
                    var mirrorPixel =
                        frame.GetPixel(
                            originalX,
                            y);

                    if (mirrorPixel != null && mirrorPixel.Value != Color.Transparent)
                    {
                        int mirrorX = x;

                        pointInfoList.Add(new PointInfo(x, y, mirrorPixel.Value));
                    }
                }
            }

            frame.SetPixel(pointInfoList);
            return true;
        }

        public override void Reset()
        {
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new NoneConfigurationPanelControl();
        }

        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Mirror Filter");
            identification.SetDescription("Represents a filter capable to expand the frame, adding a mirror image from original");
            identification.SetGroup("SFX");

            return identification;
        }
    }
}
