using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.Forms.Utilities.CharacterSet
{
    /// <summary>
    /// Offers special characteres for use with boxes, keys and arrows
    /// </summary>
    public class BoxKeysAndArrowsCharacterSet : ICharacterSet
    {
        public string GetMaxRange()
        {
            return "02FF";
        }

        public string GetMinRange()
        {
            return "02C2";
        }

        public string GetShortName()
        {
            return "BoxKeysArrows";
        }

        public override string ToString()
        {
            return "Box / Keys / Arrows";
        }
    }
}
