
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Pictures;
using smartSuite.smartSprite.Pictures.ColorPattern;
using smartSprite.SpriteEffectModule.Forms;
using System.Drawing;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// Represents a filter to infers transparent background
	/// </summary>
	public class TransparentBackgroundFilter : SmartSpriteOriginalFilterBase , IEffectFilter
    {
        /// <summary>
        /// Gets the transparentColor got after <see cref="DoTransparentBorder"/> method.
        /// </summary>
        public Color TransparentColor
        {
            get;
            private set;
        }

        /// <summary>
        /// Applies the filter
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool ApplyFilter(Picture frame, int index)
        {
            BackgroundPattern backgroundPattern = new BackgroundPattern();

            for (int y = 0; y < frame.Height; y++)
            {
                for (int x = 0; x < frame.Width; x++)
                {
                    backgroundPattern.Learn(x, y, frame.GetPixel(x, y));
                }
            }

            this.TransparentColor = 
                backgroundPattern.GetReplacementColor(
                    new Piece(frame, 
                        new Pictures.Point(0f, 0f), 
                        new Pictures.Point((float)frame.Width, (float)frame.Height)), 
                    new ColorSupportFormForAnimation());

            // Changing transparent color
            frame.TransparentColor = this.TransparentColor;

            return true;
        }

        public override void Reset()
        {
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            throw new NotImplementedException();
        }
    }
}