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
        private Regex _numericRegEx = new Regex(@"(\d+)", RegexOptions.Compiled);

        public int Compare(String path, String other)
        {
            if (_numericRegEx.IsMatch(path) && _numericRegEx.IsMatch(other))
            {
                var pathRegExList = _numericRegEx.Matches(path);
                var otherRegExList = _numericRegEx.Matches(other);

                for (int j = 0; j < pathRegExList.Count; j++)
                {
                    var pathRegEx = pathRegExList[j];

                    #region Entries validation

                    if (j > otherRegExList.Count - 1)
                    {
                        continue;
                    }

                    #endregion

                    var otherRegEx = otherRegExList[j];
                    int compare = 
                        int.Parse(pathRegEx.Value)
                                    .CompareTo(
                                        int.Parse(otherRegEx.Value));

                    if(compare != 0)
                    {
                        return compare;
                    }
                }

                return 0;
            }
            else
            {
                return path.CompareTo(other);
            }
        }
    }
}
