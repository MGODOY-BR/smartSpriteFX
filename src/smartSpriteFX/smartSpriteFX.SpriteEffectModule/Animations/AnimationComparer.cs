using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.SpriteEffectModule.Animations
{
    /// <summary>
    /// It´s a comparison used to sort animation
    /// </summary>
    public class AnimationComparer : IComparer<string>
    {
        private Regex _regEx = new Regex(@"(\d+).*", RegexOptions.Compiled);

        public int Compare(String path, String other)
        {
            if (_regEx.IsMatch(path) && _regEx.IsMatch(other))
            {
                var pathRegEx = _regEx.Match(path);
                var otherRegEx = _regEx.Match(other);

                return
                    int.Parse(pathRegEx.Groups[1].Value)
                        .CompareTo(
                            int.Parse(otherRegEx.Groups[1].Value));
            }
            else
            {
                return path.CompareTo(other);
            }
        }
    }
}
