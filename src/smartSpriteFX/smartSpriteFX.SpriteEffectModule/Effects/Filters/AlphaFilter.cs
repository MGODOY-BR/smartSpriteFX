﻿using smartSuite.smartSpriteFX.Effects.Filters;
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
    /// Represents a filter which enfasizes the alpha color
    /// </summary>
    public class AlphaFilter : SmartSpriteOriginalFilterBase, IScaleOrientedObject
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
                Color newColor = Color.FromArgb((int)this.Scale, pixelItem.Color.R, pixelItem.Color.G, pixelItem.Color.A);
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
            identification.SetName("Alpha");
            identification.SetDescription("A filter used to enfisize the transparent color");
            identification.SetGroup("Color");

            return identification;
        }

    }
}