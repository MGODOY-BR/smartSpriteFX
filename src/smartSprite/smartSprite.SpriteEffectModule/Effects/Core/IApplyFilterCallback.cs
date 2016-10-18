using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSprite.SpriteEffectModule.Effects.Core
{
    /// <summary>
    /// Defines how a callback which handle the progress of an operation must be.
    /// </summary>
    public interface IApplyFilterCallback
    {
        /// <summary>
        /// Updates the progress
        /// </summary>
        void UpdateProgress(float percentage, bool completed);
    }
}
