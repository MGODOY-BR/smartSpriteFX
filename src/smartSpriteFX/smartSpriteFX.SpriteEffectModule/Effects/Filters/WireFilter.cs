﻿using smartSuite.smartSpriteFX.Effects.Filters;
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
using System.Drawing;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Represents a filter used to make images seems like made by wires, like Tron movie
    /// </summary>
    public class WireFilter : SmartSpriteOriginalFilterBase
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

            Color? lastColor = null;

            var sourceList = frame.GetAllPixels();
            try
            {
                frame.BeginBatchUpdate();

                foreach (var sourceItem in sourceList)
                {
                    int x = (int)sourceItem.X;
                    int y = (int)sourceItem.Y;
                    Color pixel = sourceItem.Color;

                    var newColor =
                        _colorBuffer.GetSimilarColor(pixel);

                    if (transparentBackgroundFilter != null)
                    {
                        if (_colorComparer.EqualsButNoAlpha(newColor, transparentBackgroundFilter.TransparentColor))
                        {
                            newColor = ColorBuffer.GetSlightlyDifferentColor(newColor);
                        }
                    }
                    else if (frame.TransparentColor.HasValue && _colorComparer.EqualsButNoAlpha(newColor, frame.TransparentColor.Value))
                    {
                        newColor = ColorBuffer.GetSlightlyDifferentColor(newColor);
                    }

                    if (lastColor.HasValue && _colorComparer.LooksLikeBySensibility2(newColor, lastColor.Value))
                    {
                        frame.ReplacePixel(x, y, Color.Black);
                    }
                    else
                    {
                        frame.ReplacePixel(x, y, newColor);
                    }
                    lastColor = newColor;
                }

                frame.EndBatchUpdate();
            }
            catch
            {
                frame.CancelBatchUpdate();
                throw;
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
            identification.SetName("Made by Wire (Like \"Tron movie\")");
            identification.SetDescription("Turns the image similar to Tron movie, made by wire.");
            identification.SetGroup("Art");

            return identification;
        }
    }
}
