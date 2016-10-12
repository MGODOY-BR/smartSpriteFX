
using smartSuite.smartSprite.Effects.Infra;
using smartSuite.smartSprite.Effects.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Pictures;
using smartSprite.SpriteEffectModule.Effects.Filters;
using smartSprite.SpriteEffectModule.Effects.Filters.UI;

namespace smartSuite.smartSprite.Effects.Filters{
	/// <summary>
	/// Represents a filter to convert the frame to 16 bit appearence.
	/// </summary>
	public class _24BitFilter : SmartSpriteOriginalFilterBase, IResolutionFilterSetter
    {

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
        /// It큦 a contrast among the colors
        /// </summary>
        private float _contrast;

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("24 bit");
            identification.setDescription("A filter which turns the image in a 24 bit style, similar to Neo Geo");

            return identification;
        }

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
                    this._colorBufferAmount,
                    this._contrast);

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
            // This specification are initially based on Neo Geo Console
            this._colorBufferAmount = 4096;
            this._destinationScreenWidth = 320;
            this._destinationScreenHeight = 224;

            this._contrast = -0.75f;

            this._screenWidth = 1366;
            this._screenHeight = 768;
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ResolutionConfigurationPanelControl();
        }


        void IResolutionFilterSetter.setColorBufferAmount(int amount)
        {
            this._colorBufferAmount = amount;
        }

        void IResolutionFilterSetter.setDestinationScreenWidth(int amount)
        {
            this._destinationScreenWidth = amount;
        }

        void IResolutionFilterSetter.setDestinationScreenHeight(int amount)
        {
            this._destinationScreenHeight = amount;
        }

        void IResolutionFilterSetter.setContrast(float factor)
        {
            this._contrast = factor;
        }

        void IResolutionFilterSetter.setTotalScreenWidth(int screenWidth)
        {
            this._screenWidth = screenWidth;
        }

        void IResolutionFilterSetter.setTotalScreenHeight(int screenHeight)
        {
            this._screenHeight = screenHeight;
        }

    }
}