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

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter which enfasizes the bright
    /// </summary>
    public class BrightFilter : SmartSpriteOriginalFilterBase, IScaleOrientedObject
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
            var pixelList = frame.GetAllPixels();
            foreach (var pixelItem in pixelList)
            {
                var bright = (int)this.Scale;

                Color newColor = 
                    Color.FromArgb(
                        pixelItem.Color.A,
                        this.ApplyBright(pixelItem.Color.R, bright),
                        this.ApplyBright(pixelItem.Color.G, bright),
                        this.ApplyBright(pixelItem.Color.B, bright));

                frame.ReplacePixel((int)pixelItem.X, (int)pixelItem.Y, newColor);
            }

            return true;
        }

        /// <summary>
        /// Applies the bright to color
        /// </summary>
        /// <param name="color"></param>
        /// <param name="bright"></param>
        /// <returns></returns>
        private int ApplyBright(int color, int bright)
        {
            var newColor = color + bright;

            if (newColor < 0) newColor = 0;
            if (newColor > 254) newColor = 254;

            return newColor;
        }

        public override void Reset()
        {
            this.Scale = 0;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ScaleConfigurationPanelControl(-255, 255);
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Bright");
            identification.SetDescription("A filter used to enfisize the bright");
            identification.SetGroup("Color");

            return identification;
        }

    }
}
