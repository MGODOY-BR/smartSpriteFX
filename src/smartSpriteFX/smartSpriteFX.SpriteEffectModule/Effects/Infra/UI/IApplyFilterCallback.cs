using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Pictures;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Infra.UI
{
    /// <summary>
    /// Defines how a callback which handle the progress of an operation must be.
    /// </summary>
    public interface IApplyFilterCallback
    {
        /// <summary>
        /// Updates the progress and returns a boolean indicating cancelation
        /// </summary>
        void UpdateProgress(float percentage, bool completed);

        /// <summary>
        /// Allows shows up the update progress window
        /// </summary>
        void ShowUpdateProgress();

        /// <summary>
        /// Applies the filters, considering the UI infrastructure
        /// </summary>
        void ApplyFilter();

        /// <summary>
        /// Applies the filters on the specified frame, considering the UI infrastructure
        /// </summary>
        void ApplyFilter(Picture frame);
    }
}
