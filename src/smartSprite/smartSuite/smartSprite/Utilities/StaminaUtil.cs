using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace smartSprite.Utilities
{
    /// <summary>
    /// Offers a control to manipulate the "strength" of system, mainly about loopings
    /// </summary>
    public static class StaminaUtil
    {
        /// <summary>
        /// It´s the moment when the system got rest for the last time
        /// </summary>
        private static DateTime _lastRest;

        /// <summary>
        /// Gets rest sometimes
        /// </summary>
        public static void GetRestSometimes()
        {
            if (_lastRest == null || DateTime.Now.Subtract(_lastRest).TotalSeconds == 10)
            {
                _lastRest = DateTime.Now;
                Thread.Sleep(5);
            }
        }
    }
}
