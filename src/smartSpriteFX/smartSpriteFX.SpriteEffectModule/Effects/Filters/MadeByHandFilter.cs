using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Pictures.PixelPatterns;
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using System.Drawing;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using smartSuite.smartSpriteFX.Effects.Infra;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter used to emulate draw made by hand
    /// </summary>
    public class MadeByHandFilter : SmartSpriteOriginalFilterBase
    {
        public override bool ApplyFilter(Picture frame, int index)
        {
            List<PointInfo>[] columnList =
                frame.GetColumns();

            List<PointInfo>[] lineList =
                frame.GetLines();

            ColorEqualityComparer comparer = new ColorEqualityComparer();

            // Column handling
            Color comparing = Color.Transparent;
            foreach (var columnItem in columnList)
            {
                foreach (var pointItem in columnItem)
                {
                    Color currentColor = pointItem.Color;
                    if (!comparer.LooksLikeBySensibility3(comparing, currentColor))
                    {
                        pointItem.Color = Color.Black;
                    }
                    comparing = currentColor;
                }
            }

            // Line handling
            comparing = Color.Transparent;
            foreach (var lineItem in lineList)
            {
                foreach (var pointItem in lineItem)
                {
                    Color currentColor = pointItem.Color;
                    if (!comparer.LooksLikeBySensibility3(comparing, currentColor))
                    {
                        pointItem.Color = Color.Black;
                    }
                    comparing = currentColor;
                }
            }

            return true;
        }

        public override void Reset()
        {
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new NoneConfigurationPanelControl();
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Made By Hand");
            identification.SetDescription("Represents a filter used to emulate draw made by hand");
            identification.SetGroup("Art");

            return identification;
        }
    }
}
