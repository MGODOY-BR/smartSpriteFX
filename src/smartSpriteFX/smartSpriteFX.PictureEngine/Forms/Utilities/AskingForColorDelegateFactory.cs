using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities
{
    /// <summary>
    /// Offers an factory for delegation of asking color delegate
    /// </summary>
    static class AskingForColorDelegateFactory
    {
        /// <summary>
        /// Get a instance for background color definition
        /// </summary>
        /// <returns></returns>
        public static IAskingForColorDelegate GetInstanceForBackgroundColor()
        {
            var backgroundSupportForm = new ColorSupportForm();
            backgroundSupportForm.SetAsking("I need your support to indicate the background of this piece.");

            return backgroundSupportForm;
        }
    }
}
