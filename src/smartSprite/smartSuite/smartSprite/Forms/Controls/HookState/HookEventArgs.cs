using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSpriteFX.Forms.Controls.HookState
{
    /// <summary>
    /// Offers a state for events
    /// </summary>
    public class HookEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the first created hook of a pair
        /// </summary>
        public HookControl MainHook { get; set; }
    }
}
