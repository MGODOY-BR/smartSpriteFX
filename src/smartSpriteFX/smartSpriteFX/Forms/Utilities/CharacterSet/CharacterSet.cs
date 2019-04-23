using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.CharacterSet
{
    /// <summary>
    /// Gets the character set
    /// </summary>
    public static class CharacterSet
    {
        /// <summary>
        /// Represents a list of allowed character sets.
        /// </summary>
        private static List<ICharacterSet> _innetList = new List<ICharacterSet>
        {
            new CommonOcidentalCharacterSet(),
            new CJKUnifiedIdeogramsCharacterSet(),
            new ExtraLusofoneCharacterSet(),
            new BoxKeysAndArrowsCharacterSet()
        };

        /// <summary>
        /// Gets the list of allowed character sets
        /// </summary>
        /// <returns></returns>
        public static List<ICharacterSet> GetList()
        {
            return _innetList;
        }
    }
}
