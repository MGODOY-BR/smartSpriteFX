using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSprite.SpriteEffectModule.Effects.Filters
{
    /// <summary>
    /// Defines a filter specified to handle resolution
    /// </summary>
    public interface IResolutionFilterSetter
    {
        /// <summary>
        /// Gets or gets the amount of color in new resolution
        /// </summary>
        int ColorBufferAmount
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or gets the screen width in new resolution
        /// </summary>
        int DestinationScreenWidth
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or gets the screen height in new resolution
        /// </summary>
        int DestinationScreenHeight
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or gets the contrast of colors
        /// </summary>
        int Contrast
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or gets the total of width of screen
        /// </summary>
        int TotalScreenWidth
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or gets the total of height of screen
        /// </summary>
        int TotalScreenHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a list of color to avoid
        /// </summary>
        List<Color> AvoidColorList
        {
            get;
            set;
        }
    }
}
