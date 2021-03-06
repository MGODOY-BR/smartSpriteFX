
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSpriteFX.Effects.Infra.UI.Configuratons;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Effects.Infra;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters.UI;
using smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters;

namespace smartSuite.smartSpriteFX.Effects.Filters{
    /// <summary>
    /// It´s a filter that simulates changing of frame rate of an animation
    /// </summary>
    public class FrameRateFilter : SmartSpriteOriginalFilterBase, IScaleOrientedObject
    {

        #region Not configurable state

        /// <summary>
        /// It´s the amount of frames handled in the second
        /// </summary>
        private int _frameCount = 0;

        /// <summary>
        /// It´s the last handled frame index
        /// </summary>
        private int _lastFrameIndex;

        #endregion

        /// <summary>
        /// It´s the reason of frame per second
        /// </summary>
        private float _framesPerSec;

        /// <summary>
        /// It´s a collection of frames which the user desires, regardless of the framesPerSec rate.
        /// </summary>
        private List<int> _keyFrames = new List<int>();

        /// <summary>
        /// Gets the keyFrames
        /// </summary>
        internal List<int> KeyFrames
        {
            get
            {
                return _keyFrames;
            }
        }

        public float Scale
        {
            get
            {
                return _framesPerSec;
            }
            set
            {
                _framesPerSec = value;
            }
        }

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Frame rate");
            identification.SetDescription("A filter which is applied to entire animation to reduce the frames among it to give the aspect of old-school fighting games.");
            identification.SetGroup("Animation");

            return identification;
        }

        /// <summary>
        /// Aplies the filter
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override bool ApplyFilter(Picture frame, int index)
        {
            this._frameCount++;
            this._lastFrameIndex = index;
            var framesPerSec =
                Math.Round(40 / this._framesPerSec, 
                    0, 
                    MidpointRounding.ToEven);

            if (this._frameCount >= framesPerSec || this._keyFrames.Contains(index))
            {
                this._frameCount = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Reset()
        {
            this._frameCount = 0;
            this._lastFrameIndex = 0;
            this._framesPerSec = 20f;
            this._keyFrames.Clear();
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            return new ScaleConfigurationPanelControl(10f, 40f);
        }
    }
}