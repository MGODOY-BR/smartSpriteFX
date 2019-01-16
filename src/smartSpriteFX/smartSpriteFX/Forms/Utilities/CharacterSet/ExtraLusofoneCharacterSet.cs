using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.CharacterSet
{
    /// <summary>
    /// Represents a character correspondent to accent in lusophone countries
    /// </summary>
    public class ExtraLusofoneCharacterSet : ICharacterSet
    {
        public string GetMaxRange()
        {
            return "00FF";
        }

        public string GetMinRange()
        {
            return "00A0";
        }

        public string GetShortName()
        {
            return "lusophone.accent";
        }

        public override string ToString()
        {
            return "Latin (lusophone) accents";
        }
    }
}
