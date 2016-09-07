
using smartSuite.smartSprite.Effects.Infra;
using smartSuite.smartSprite.Effects.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Pictures;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// Represents a filter to convert the frame to 16 bit appearence.
	/// </summary>
	public class _16BitFilter : SmartSpriteOriginalFilterBase {

        /// <summary>
        /// It´s the color buffer amount
        /// </summary>
        private int _colorBufferAmount;
        /// <summary>
        /// It´s the destination width
        /// </summary>
        private int _destinationWidth;
        /// <summary>
        /// It´s the destination height
        /// </summary>
        private int _destinationHeight;

        /// <summary>
        /// Applies the filter
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool ApplyFilter(Picture frame, int index)
        {
            ResolutionTranslator translator =
                new ResolutionTranslator(
                    frame,
                    this._destinationWidth,
                    this._destinationHeight,
                    this._colorBufferAmount);

            for (int y = 0; y < frame.Height; y++)
            {
                for (int x = 0; x < frame.Width; x++)
                {
                    translator.Translate(x, y, frame.GetPixel(x, y));
                }
            }

            Picture filteredFrame = translator.CreatedTranslatedPicture();
            // Releasing the buffer
            frame.ReleaseBuffer();
            // Importing the pixeled buffer of translated picture
            frame.Merge(filteredFrame);
            // Release memory
            filteredFrame.ReleaseBuffer();
            filteredFrame.Dispose();

            return true;
        }

        public override void Reset()
        {
            // This specification are initially based on NEO-GEO consoles
            this._colorBufferAmount = 4096;
            this._destinationWidth = 320;
            this._destinationHeight = 224;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            throw new NotImplementedException();
        }
    }
}