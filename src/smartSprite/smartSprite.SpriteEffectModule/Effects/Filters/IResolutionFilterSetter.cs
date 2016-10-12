using System;
using System.Collections.Generic;
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
        /// Sets the amount of color in new resolution
        /// </summary>
        /// <param name="amount"></param>
        void setColorBufferAmount(int amount);
        /// <summary>
        /// Sets the screen width in new resolution
        /// </summary>
        /// <param name="amount"></param>
        void setDestinationScreenWidth(int amount);
        /// <summary>
        /// Sets the screen height in new resolution
        /// </summary>
        /// <param name="amount"></param>
        void setDestinationScreenHeight(int amount);
        /// <summary>
        /// Sets the contrast of colors
        /// </summary>
        /// <param name="factor"></param>
        void setContrast(float factor);
        /// <summary>
        /// Sets the total of width of screen
        /// </summary>
        /// <param name="screenWidth"></param>
        void setTotalScreenWidth(int screenWidth);
        /// <summary>
        /// Sets the total of height of screen
        /// </summary>
        /// <param name="screenHeight"></param>
        void setTotalScreenHeight(int screenHeight);
    }
}
