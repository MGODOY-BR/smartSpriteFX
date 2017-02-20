
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System.Drawing;

namespace smartSuite.smartSpriteFX.Effects.Filters{
	/// <summary>
	/// Represents a filter to convert the frame to 16 bit appearence.
	/// </summary>
	public class _8BitFilter : SmartSpriteOriginalFilterBase, IResolutionFilterSetter
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
        private int _contrast;

        /// <summary>
        /// It큦 a list of color to avoid
        /// </summary>
        private List<Color> _avoidColorList;

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("8 bit");
            identification.SetDescription("A filter which turns the image in a 8 bit style, similar to Sega Master System or NES");
            identification.SetGroup("Old School Video-Game");

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
                    _contrast);

            translator.AvoidColorList = this._avoidColorList;

            var sourceList = frame.GetAllPixels();
            foreach (var sourceItem in sourceList)
            {
                translator.Translate(
                    (int)sourceItem.X,
                    (int)sourceItem.Y,
                    sourceItem.Color);
            }

            Picture filteredFrame = translator.CreateTranslatedPicture();
            // Releasing the buffer
            frame.ReleaseBuffer();
            // Importing the pixeled buffer of translated picture
            frame.ShareDataWithMe(filteredFrame);

            return true;
        }

        public override void Reset()
        {
            // This specification are initially based on Sega Master System Console
            this._colorBufferAmount = 32;
            this._destinationScreenWidth = 256;
            this._destinationScreenHeight = 192;
            this._contrast = 0;

            this._screenWidth = 1366;
            this._screenHeight = 768;

            this._avoidColorList = new List<Color>();
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ResolutionConfigurationPanelControl();
        }


        int IResolutionFilterSetter.ColorBufferAmount
        {
            get
            {
                return this._colorBufferAmount;
            }
            set
            {
                this._colorBufferAmount = value;
            }
        }

        int IResolutionFilterSetter.DestinationScreenWidth
        {
            get
            {
                return this._destinationScreenWidth;
            }
            set
            {
                this._destinationScreenWidth = value;
            }
        }

        int IResolutionFilterSetter.DestinationScreenHeight
        {
            get
            {
                return this._destinationScreenHeight;
            }
            set
            {
                this._destinationScreenHeight = value;
            }
        }

        int IResolutionFilterSetter.Contrast
        {
            get
            {
                return this._contrast;
            }
            set
            {
                this._contrast = value;
            }
        }

        int IResolutionFilterSetter.TotalScreenWidth
        {
            get
            {
                return this._screenWidth;
            }
            set
            {
                this._screenWidth = value;
            }
        }

        int IResolutionFilterSetter.TotalScreenHeight
        {
            get
            {
                return this._screenHeight;
            }
            set
            {
                this._screenHeight = value;
            }
        }

        List<Color> IResolutionFilterSetter.AvoidColorList
        {
            get
            {
                return this._avoidColorList;
            }
            set
            {
                this._avoidColorList = value;
            }
        }


    }
}