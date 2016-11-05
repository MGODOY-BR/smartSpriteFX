using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSpriteFX.Forms.Controls
{
    /// <summary>
    /// Defines an item as a destroyable
    /// </summary>
    public interface IDestroyable
    {
        /// <summary>
        /// Destroy the item
        /// </summary>
        void DestroyYourSelf();
    }
}
