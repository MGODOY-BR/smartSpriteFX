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
    /// Represents a filter capable to invert the frame
    /// </summary>
    public class InversionFilter : SmartSpriteOriginalFilterBase
    {
        public override bool ApplyFilter(Picture frame, int index)
        {
            var originalWidth = frame.Width;

            // Filling the rest of empty spaces
            int originalX = originalWidth;
            List<PointInfo> pointInfoList = new List<PointInfo>();
            for (int x = 0; x < originalWidth; x++)
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
                        var newX = originalWidth - originalX;
                        pointInfoList.Add(new PointInfo(newX, y, mirrorPixel.Value));
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
            identification.SetName("Inversion Filter");
            identification.SetDescription("Represents a filter capable to invert the frame");
            identification.SetGroup("Picture");

            return identification;
        }
    }
}
