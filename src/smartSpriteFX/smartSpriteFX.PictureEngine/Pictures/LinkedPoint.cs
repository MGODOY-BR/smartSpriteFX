using smartSuite.smartSpriteFX.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures
{
    /// <summary>
    /// Represents a point which saves information about another adjacent points
    /// </summary>
    [Obsolete]
    public class LinkedPoint : Point
    {
        /// <summary>
        /// Indicates if the relatives point has been loaded
        /// </summary>
        public virtual bool HasRelativesLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// It's the list of relative points
        /// </summary>
        private List<Point> _relativeList = new List<Point>();

        /// <summary>
        /// Gets the relative list
        /// </summary>
        /// <returns></returns>
        public virtual List<Point> GetRelativeList()
        {
            return this._relativeList;
        }

        /// <summary>
        /// Register a relationship between this point and the indicated
        /// </summary>
        /// <param name="point"></param>
        public virtual void RegisterRelationship(Point point)
        {
            this._relativeList.Add(point);
            this.HasRelativesLoaded = true;
        }

        /// <summary>
        /// Indicates that this point hasn't any relates at all
        /// </summary>
        public virtual void IndicatesNoRelationship()
        {
            this.HasRelativesLoaded = true;
        }

        /// <summary>
        /// Gets an indicator informing if the point is relative of this one
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool IsRelative(Point point)
        {
            #region Entries validation

            if (point == null)
            {
                throw new ArgumentNullException("point");
            }

            #endregion

            return this._relativeList.Contains(point);
        }

        /// <summary>
        /// Gets an indicator informing if this point has relatives
        /// </summary>
        /// <returns></returns>
        public virtual bool HasRelatives()
        {
            #region Entries validation

            if(this.HasRelativesLoaded)
            {
                throw new InvalidOperationException("Nor RegisterRelationship() and IndicatesNoRelationship() hasn't been called yet");
            }

            #endregion

            return this._relativeList.Count > 0;
        }

    }
}
