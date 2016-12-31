using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using smartSuite.smartSpriteFX.Effects.Core;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter used to convert real images to drawn by hand like.
    /// </summary>
    public class DrawnByHandFilter : SmartSpriteOriginalFilterBase
    {
        /// <summary>
        /// It´s the color buffer used to simply the colors
        /// </summary>
        private ColorBuffer _colorBuffer = new ColorBuffer(10, 0);

        /// <summary>
        /// A comparer used to compare colors.
        /// </summary>
        private ColorEqualityComparer _colorComparer = new ColorEqualityComparer();

        public override bool ApplyFilter(Picture frame, int index)
        {
            var transparentBackgroundFilter =
                EffectEngine.GetTransparentBackgroundFilter();

            for (int y = 0; y < frame.Height; y++)
            {
                for (int x = 0; x < frame.Width; x++)
                {
                    var pixel = frame.GetPixel(x, y);

                    if (pixel.HasValue)
                    {
                        var newColor =
                            _colorBuffer.GetSimilarColor(pixel.Value);

                        if (transparentBackgroundFilter != null)
                        {
                            if (!_colorComparer.EqualsButNoAlpha(newColor, transparentBackgroundFilter.TransparentColor))
                            {
                                newColor = _colorBuffer.GetSlightlyDifferentColor(newColor);
                            }
                        }

                        frame.ReplacePixel(x, y, newColor);
                    }
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
            identification.SetName("Drawn by Hand");
            identification.SetDescription("A filter which transforms photos taken from reality in drawn by hand one.");
            identification.SetGroup("Art");

            return identification;
        }
    }
}
