
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using smartSuite.smartSprite.Effects.Infra.UI.Configuratons;
using smartSuite.smartSprite.Pictures;
using smartSuite.smartSprite.Effects.Infra;

namespace smartSuite.smartSprite.Effects.Filters{
    /// <summary>
    /// It´s a filter that simulates changing of frame rate of an animation
    /// </summary>
    public class FrameRateFilter : SmartSpriteOriginalFilterBase {

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

        /// <summary>
        /// Gets the identification
        /// </summary>
        /// <returns></returns>
        public override Identification GetIdentification()
        {
            var identification = base.GetIdentification();
            identification.SetName("Frame rate");
            identification.setDescription("A filter which is applied to entire animation to reduce the frames among it to give the aspect of old-school fighting games.");

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
            float currentFrameRate = this._frameCount;
            float framesPerSecRate = this._framesPerSec;

            if (framesPerSecRate <= currentFrameRate || this._keyFrames.Contains(index))
            {
                this._frameCount = 0;
                return true;
            }

            return false;
        }

        public override void Reset()
        {
            this._framesPerSec = 20f;
            this._keyFrames.Clear();
        }

        public override IConfigurationPanel ShowConfigurationPanel()
        {
            throw new NotImplementedException();
        }
    }
}