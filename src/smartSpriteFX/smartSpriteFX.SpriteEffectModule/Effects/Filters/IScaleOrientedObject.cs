using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Defines a filter that is based in scale terms.
    /// </summary>
    public interface IScaleOrientedObject
    {
        /// <summary>
        /// Gets or sets the scale proportion
        /// </summary>
        float Scale
        {
            get;
            set;
        }
    }
}
