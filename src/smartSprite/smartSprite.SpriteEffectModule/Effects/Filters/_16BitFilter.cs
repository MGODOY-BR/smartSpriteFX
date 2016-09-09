
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
        /// It큦 the color buffer amount
        /// </summary>
        private int _colorBufferAmount;
        /// <summary>
        /// It큦 the destination width
        /// </summary>
        private int _destinationScreenWidth;
        /// <summary>
        /// It큦 the destination height
        /// </summary>
        private int _destinationScreenHeight;

        /// <summary>
        /// It큦 the origin width
        /// </summary>
        private int _screenWidth;
        /// <summary>
        /// It큦 the origin height
        /// </summary>
        private int _screenHeight;

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
                    this._screenWidth,
                    this._screenHeight,
                    this._destinationScreenWidth,
                    this._destinationScreenHeight,
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
            // This specification are initially based on Sega-Genesis Console
            this._colorBufferAmount = 512;
            this._destinationScreenWidth = 320;
            this._destinationScreenHeight = 240;

            this._screenWidth = 1366;
            this._screenHeight = 768;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            throw new NotImplementedException();
        }
    }
}