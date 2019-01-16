
using smartSuite.smartSpriteFX.PictureEngine.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace smartSuite.smartSpriteFX.Pictures
{
    /// <summary>
    /// Represents a cartesyan coordinate
    /// </summary>
    [Serializable]
    public class Point : IComparable<Point>, IFromString
    {
        /// <summary>
        /// It's a regex that extract data from fileSet
        /// </summary>
        private static Regex _regExDataFromFile = new Regex(@"X = (\d+), Y = (\d+)", RegexOptions.Compiled);

        public Point()
        {

        }

        public Point(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// It's a coordinate X
        /// </summary>
        public virtual float X
        {
            get;
            set;
        }

        /// <summary>
        /// It´s a coordinate Y
        /// </summary>
        public virtual float Y
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a dictionary to use with comparer
        /// </summary>
        /// <returns></returns>
        public static IComparer<string> GetDictionaryComparer()
        {
            return new PointStringComparer();
        }

        public virtual int CompareTo(Point other)
        {
            #region Entries validation

            if (other == null)
            {
                return 1;
            }
            //if(this.ToString() == other.ToString())
            //{
            //    return 0;
            //}
            if(this.X == other.X && this.Y == other.Y)
            {
                return 0;
            }

            #endregion

            if (this.Y < other.Y)
            {
                return -1;
            }
            else if (this.Y > other.Y)
            {
                return 1;
            }
            else
            {
                return this.X.CompareTo(other.X);
            }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Point other = (Point)obj;

            return this.CompareTo(other) == 0;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Returns a clone of point
        /// </summary>
        /// <returns></returns>
        public virtual Point Clone()
        {
            return new Point(this.X, this.Y);
        }

        public override string ToString()
        {
            return String.Format("X = {0}, Y = {1}", this.X, this.Y);
        }

        /// <summary>
        /// Fills the data
        /// </summary>
        /// <param name="valueString"></param>
        public virtual void FillMe(string valueString)
        {
            #region Entries validation

            if (String.IsNullOrEmpty(valueString))
            {
                return;
            }
            if (!_regExDataFromFile.IsMatch(valueString))
            {
                throw new ArgumentException("Invalid format data");
            }

            #endregion

            Match match = _regExDataFromFile.Match(valueString);
            this.X = int.Parse(match.Groups[1].Value);
            this.Y = int.Parse(match.Groups[2].Value);
        }

        /// <summary>
        /// Offers a comparer to correct string sortings
        /// </summary>
        public class PointStringComparer : IComparer<string>
        {
            private static Regex _regex = new Regex(@"X = (-{0,1}\d+), Y = (-{0,1}\d+)", RegexOptions.Compiled);

            /// <summary>
            /// Gets the comparison factor
            /// </summary>
            /// <param name="x">A string like the return of Point.ToString()</param>
            /// <param name="y">A string like the return of Point.ToString()</param>
            /// <returns></returns>
            public int Compare(string x, string y)
            {
                #region Entries validation

                if (String.IsNullOrEmpty(x))
                {
                    throw new ArgumentNullException("x");
                }
                if (String.IsNullOrEmpty(y))
                {
                    throw new ArgumentNullException("y");
                }
                if (!_regex.IsMatch(x))
                {
                    throw new FormatException(x + " is not valid");
                }
                if (!_regex.IsMatch(y))
                {
                    throw new FormatException(y + " is not valid");
                }

                #endregion

                Point pointA = new Point
                {
                    X = int.Parse(_regex.Match(x).Groups[1].Value),
                    Y = int.Parse(_regex.Match(x).Groups[2].Value),
                };

                Point pointB = new Point
                {
                    X = int.Parse(_regex.Match(y).Groups[1].Value),
                    Y = int.Parse(_regex.Match(y).Groups[2].Value),
                };

                return pointA.CompareTo(pointB);
            }
        }

        /// <summary>
        /// Compares the current point to another and returns true if both are almost similar
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool NearlyEqualsTo(Point other)
        {
            int tolerance = 5;

            return this.NearlyEqualsTo(other, tolerance);
        }

        /// <summary>
        /// Compares the current point to another and returns true if both are almost similar
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool NearlyEqualsTo(Point other, int tolerance)
        {
            int myX = (int)this.X;
            int myY = (int)this.Y;

            int otherX = (int)other.X;
            int otherY = (int)other.Y;

            return
                this.CompareIntervalViceVersa(myX, otherX, tolerance) &&
                this.CompareIntervalViceVersa(myY, otherY, tolerance);
        }

        /// <summary>
        /// Compares two numbers by interval in both way
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        private bool CompareIntervalViceVersa(int numberA, int numberB, int tolerance)
        {
            return
                this.CompareInterval(numberA, numberB, tolerance) ||
                this.CompareInterval(numberB, numberA, tolerance);
        }

        /// <summary>
        /// Compares two numbers by interval
        /// </summary>
        /// <param name="numberA"></param>
        /// <param name="numberB"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        private bool CompareInterval(int numberA, int numberB, int tolerance)
        {
            int minNumberB = numberB - tolerance;
            int maxNumberB = numberB + tolerance;

            return 
                minNumberB <= numberA && numberA <= maxNumberB;
        }
    }
}