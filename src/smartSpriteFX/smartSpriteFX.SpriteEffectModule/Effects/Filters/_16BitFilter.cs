
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.Effects.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using System.Windows.Forms;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters;
using System.Drawing;

namespace smartSuite.smartSpriteFX.Effects.Filters{
	/// <summary>
	/// Represents a filter to convert the frame to 16 bit appearence.
	/// </summary>
	public class _16BitFilter : SmartSpriteOriginalFilterBase, IResolutionFilterSetter
    {

        /// <summary>
        /// It�s the color buffer amount
        /// </summary>
        private int _colorBufferAmount;
        /// <summary>
        /// It�s the destination width
        /// </summary>
        private int _destinationScreenWidth;
        /// <summary>
        /// It�s the destination height
        /// </summary>
        private int _destinationScreenHeight;

        /// <summary>
        /// It�s the origin width
        /// </summary>
        private int _screenWidth;
        /// <summary>
        /// It�s the origin height
        /// </summary>
        private int _screenHeight;

        /// <summary>
        /// It�s a contrast among the colors
        /// </summary>
        private int _contrast;

        /// <summary>
        /// It�s a list of color to avoid
        /// </summary>
        private List<Color> _avoidColorList; 

        public int ColorBufferAmount
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

        public int DestinationScreenWidth
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

        public int DestinationScreenHeight
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

        public int Contrast
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

        public int TotalScreenWidth
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

        public int TotalScreenHeight
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

        public List<Color> AvoidColorList
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

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("16 bit");
            identification.SetDescription("A filter which turns the image in a 16 bit style, similar to Sega Genesis or Super NES");
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
            // This specification are initially based on modification and mix between Sega Genesis and SNES Console
            this._colorBufferAmount = 64;
            this._destinationScreenWidth = 256 + 50;
            this._destinationScreenHeight = 224 + 50;
            this._contrast = 0;

            this._screenWidth = 1366;
            this._screenHeight = 768;

            this._avoidColorList = new List<Color>();
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ResolutionConfigurationPanelControl();
        }


    }
}