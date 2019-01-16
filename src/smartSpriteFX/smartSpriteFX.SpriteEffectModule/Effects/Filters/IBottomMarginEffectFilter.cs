using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Defines how filter which supports bottom margin definition should be
    /// </summary>
    public interface IBottomMarginEffectFilter : IMarginEnabledEffect
    {
        /// <summary>
        /// Gets or sets the bottom margin
        /// </summary>
        int BottomMargin { get; set; }
    }
}
