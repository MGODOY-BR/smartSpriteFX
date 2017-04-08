using smartSuite.smartSpriteFX.Effects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Effects.Infra;
using System.Drawing;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using smartSuite.smartSpriteFX.Effects.Core;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter which enfasizes the blue color
    /// </summary>
    public class BlueFilter : SmartSpriteOriginalFilterBase, IScaleOrientedObject
    {
        /// <summary>
        /// Gets or sets the scale of color
        /// </summary>
        public float Scale
        {
            get;
            set;
        }

        public override bool ApplyFilter(Picture frame, int index)
        {
            var transparentBackgroundFilter =
                EffectEngine.GetTransparentBackgroundFilter();

            var cropFilter = EffectEngine.FindFilter<CropFilter>();

            var pixelList = frame.GetAllPixels();
            foreach (var pixelItem in pixelList)
            {
                #region Transparent background dealing

                if (transparentBackgroundFilter != null)
                {
                    if (transparentBackgroundFilter.TransparentColor.ToArgb() ==
                        pixelItem.Color.ToArgb())
                    {
                        continue;
                    }
                }
                if (cropFilter != null)
                {
                    if (cropFilter.GetTransparentColor().ToArgb() ==
                        pixelItem.Color.ToArgb())
                    {
                        continue;
                    }
                }

                #endregion

                Color newColor = Color.FromArgb(pixelItem.Color.A, pixelItem.Color.R, pixelItem.Color.G, (int)this.Scale);
                frame.ReplacePixel((int)pixelItem.X, (int)pixelItem.Y, newColor);
            }

            return true;
        }

        public override void Reset()
        {
            this.Scale = 255;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ScaleConfigurationPanelControl(0, 255);
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Blue");
            identification.SetDescription("A filter used to enfisize the blue color");
            identification.SetGroup("Color");

            return identification;
        }

    }
}
