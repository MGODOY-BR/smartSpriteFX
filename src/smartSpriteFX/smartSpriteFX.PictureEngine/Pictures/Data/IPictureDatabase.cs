using System.Collections.Generic;
using System.Drawing;
using smartSuite.smartSpriteFX.Pictures;
using smartSuite.smartSpriteFX.Pictures.ColorPattern;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.Data
{
    /// <summary>
    /// Defines how a picture data base must be
    /// </summary>
    public interface IPictureDatabase
    {
        /// <summary>
        /// Gets or sets the background color, used in clear methods to stablish the paper color
        /// </summary>
        /// <returns></returns>
        Color? BackgroundColor 
        {
            get;
            set;
        }

        /// <summary>
        /// Begins the transaction
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Deletes all the content of table
        /// </summary>
        void CLEAR();
        /// <summary>
        /// Clones the database
        /// </summary>
        /// <returns></returns>
        IPictureDatabase Clone();
        /// <summary>
        /// Closes the database
        /// </summary>
        void Close();
        /// <summary>
        /// Commits the transaction
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// Counts the amount of pixels
        /// </summary>
        /// <returns></returns>
        long COUNT();
        /// <summary>
        /// Counts the amount of colors for the critera
        /// </summary>
        /// <returns></returns>
        long COUNT(Color color);
        /// <summary>
        /// Gets the amount of colors of database
        /// </summary>
        long CountColor();
        /// <summary>
        /// Removes the pixel
        /// </summary>
        /// <returns>An indicator informing that the pixel has been excluded.</returns>
        bool DELETE(int x, int y);
        /// <summary>
        /// Checks if exists the color for the coordinates
        /// </summary>
        /// <returns></returns>
        bool EXISTS(smartSpriteFX.Pictures.Point point);
        /// <summary>
        /// Checks if exists the color for the coordinates
        /// </summary>
        /// <returns></returns>
        bool EXISTS(smartSpriteFX.Pictures.Point point, Color color);
        /// <summary>
        /// Gets all the colors of database
        /// </summary>
        List<Color> GetAllColors();
        /// <summary>
        /// Inserts a record
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        void INSERT(int x, int y, Color color);
        /// <summary>
        /// Inserts a bunch of points
        /// </summary>
        /// <param name="pointInfoList"></param>
        void INSERT(List<PointInfo> pointInfoList);
        /// <summary>
        /// Merge other dataBase in the current dataBase
        /// </summary>
        /// <param name="other"></param>
        void Merge(IPictureDatabase other);
        /// <summary>
        /// Rolls back the transaction
        /// </summary>
        void RollbackTransaction();
        /// <summary>
        /// Gets the points for the color
        /// </summary>
        /// <returns></returns>
        List<PointInfo> SELECT(Color color);
        /// <summary>
        /// Gets the color information of coordination
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        ColorInfo SELECT(int x, int y);
        /// <summary>
        /// Gets the pointInfo of the point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        PointInfo SELECT(smartSpriteFX.Pictures.Point point);
        /// <summary>
        /// Gets all the points
        /// </summary>
        /// <returns></returns>
        List<PointInfo> SELECTALL();
        /// <summary>
        /// Updates colors in all records
        /// </summary>
        /// <returns>Gets the amount of records updated</returns>
        int UPDATE(Color color, Color replaceColor);
        /// <summary>
        /// Updates colors in all records
        /// </summary>
        /// <returns>Gets the amount of records updated</returns>
        int UPDATE(int x, int y, Color color);
    }
}