using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.CharacterSet
{
    /// <summary>
    /// Represents a character set common in ocident
    /// </summary>
    public class CommonOcidentalCharacterSet : ICharacterSet
    {
        public string GetMaxRange()
        {
            return "007E";
        }

        public string GetMinRange()
        {
            return "0020";
        }

        public override string ToString()
        {
            return "Common Ocidental (almost ASCII)";
        }

        public string GetShortName()
        {
            return "ascii";
        }
    }
}
