using smartSuite.smartSpriteFX.Forms;
using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Forms
{
    /// <summary>
    /// It´s a version of <see cref="ColorSupportForm"/> used to support animations 
    /// </summary>
    public class ColorSupportFormForAnimation : ColorSupportForm
    {
        /// <summary>
        /// It´s the last selected color
        /// </summary>
        private static Color? _lastSelectedColor;

        /// <summary>
        /// Asks for support from user to indicate how of colors patterns identified in search is the background color.
        /// </summary>
        /// <param name="piece">The piece that has been analysed.</param>
        /// <param name="colorList"></param>
        /// <returns></returns>
        public override Color AnswerMe(Piece piece, List<Color> colorList)
        {
            if (_lastSelectedColor.HasValue)
            {
                return _lastSelectedColor.Value;
            }

            _lastSelectedColor = base.AnswerMe(piece, colorList);
            return _lastSelectedColor.Value;
        }

        /// <summary>
        /// Clears the cache
        /// </summary>
        public static void ClearCache()
        {
            _lastSelectedColor = null;
        }
    }
}
