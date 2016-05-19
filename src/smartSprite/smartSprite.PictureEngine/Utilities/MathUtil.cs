using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSprite.Utilities
{
    /// <summary>
    /// Offers math utilities
    /// </summary>
    public static class MathUtil
    {
        /// <summary>
        /// Gets the bigger number
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public static T GetBigger<T>(T number1, T number2) where T : IComparable
        {
            if (number1.CompareTo(number2) == 1)
            {
                return number1;
            }
            else if (number1.CompareTo(number2) == -1)
            {
                return number2;
            }
            else
            {
                return number1;
            }
        }

        /// <summary>
        /// Gets the lower number
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public static T GetLower<T>(T number1, T number2) where T : IComparable
        {
            if (number1.CompareTo(number2) == -1)
            {
                return number1;
            }
            else if (number1.CompareTo(number2) == 1)
            {
                return number2;
            }
            else
            {
                return number1;
            }
        }

        /// <summary>
        /// Gets the module resulted from substraction
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public static float SubtractAbsolute(float number1, float number2)
        {
            return Math.Abs(number2 - number1);
        }
    }
}
