
namespace smartSuite.smartSprite.AutoPainting{
	/// <summary>
	/// Relates the status of polygon
	/// </summary>
	public enum PolygonStatusEnum
    {
        /// <summary>
        /// The polygon has no points at all
        /// </summary>
		EMPTY,
        /// <summary>
        /// The set of points is not recognized as a polygon
        /// </summary>
		NOT_RECOGNIZED,
        /// <summary>
        /// The set of points is not entirely recognized as a polygon (require been enclosed)
        /// </summary>
		INCOMPLETE,
        /// <summary>
        /// The set of points is allthrough recognized as a polygon
        /// </summary>
		COMPLETE,
        /// <summary>
        /// The polygon is part of other polygon
        /// </summary>
        PIECE,
        /// <summary>
        /// A deep analysis polygon is required
        /// </summary>
        PENDING
    }
}