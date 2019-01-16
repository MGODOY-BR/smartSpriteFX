using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.CharacterSet
{
    /// <summary>
    /// Defines how a character set must be
    /// </summary>
    public interface ICharacterSet
    {
        /// <summary>
        /// Gets the minimum range
        /// </summary>
        /// <returns></returns>
        string GetMinRange();

        /// <summary>
        /// Gets the maximum range
        /// </summary>
        /// <returns></returns>
        string GetMaxRange();

        /// <summary>
        /// Gets the short name of character set, used to generate folders
        /// </summary>
        /// <returns></returns>
        string GetShortName();
    }
}
