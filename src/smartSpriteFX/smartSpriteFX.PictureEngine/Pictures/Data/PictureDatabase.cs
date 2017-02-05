using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.Data
{
    /// <summary>
    /// Represents a manager for databases
    /// </summary>
    internal class PictureDatabase
    {
        /// <summary>
        /// It´s the start timeof transaction
        /// </summary>
        private DateTime? _transactionStartTime;

        /// <summary>
        /// It´s the sessionID of datas.
        /// </summary>
        private String _sessionID;

        /// <summary>
        /// It´s a datasource
        /// </summary>
        private static DataTable _dataSource;

        /// <summary>
        /// This constructor has been created for goals of design and can not be used for external calls
        /// </summary>
        private PictureDatabase()
        {
        }

        /// <summary>
        /// Gets the identification of session
        /// </summary>
        public string SessionID
        {
            get
            {
                return this._sessionID;
            }
        }

        /// <summary>
        /// Gets a opened database
        /// </summary>
        /// <returns></returns>
        public static PictureDatabase Open()
        {
            PictureDatabase returnValue = new PictureDatabase();
            lock (typeof(PictureDatabase))
            {
                if (PictureDatabase._dataSource == null)
                {
                    PictureDatabase._dataSource = new DataTable("TB_PICTURE");
                }
            }
            returnValue._sessionID = Guid.NewGuid().ToString();

            return returnValue;
        }

        /// <summary>
        /// Closes the database
        /// </summary>
        public void Close()
        {
            if (PictureDatabase._dataSource == null)
            {
                return;
            }

            this.CLEAR();
        }

        /// <summary>
        /// Creates the database in memory.
        /// </summary>
        public void CreateDatabase()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }
            if (PictureDatabase._dataSource.Columns.Count != 0)
            {
                return;
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                PictureDatabase._dataSource.Columns.Add("SESSIONID", typeof(String));
                PictureDatabase._dataSource.Columns.Add("X", typeof(Int32));
                PictureDatabase._dataSource.Columns.Add("Y", typeof(Int32));
                PictureDatabase._dataSource.Columns.Add("A", typeof(Int32));
                PictureDatabase._dataSource.Columns.Add("R", typeof(Int32));
                PictureDatabase._dataSource.Columns.Add("G", typeof(Int32));
                PictureDatabase._dataSource.Columns.Add("B", typeof(Int32));

                PictureDatabase._dataSource.Constraints.Add(
                    "PK",
                    new DataColumn[3]
                    {
                        PictureDatabase._dataSource.Columns["SESSIONID"],
                        PictureDatabase._dataSource.Columns["X"],
                        PictureDatabase._dataSource.Columns["Y"]
                    },
                    true);
            }
        }

        /// <summary>
        /// Inserts a record
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void INSERT(int x, int y, Color color)
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                var row = PictureDatabase._dataSource.NewRow();

                row["SESSIONID"] = this._sessionID;
                row["X"] = x;
                row["Y"] = y;
                row["A"] = color.A;
                row["R"] = color.R;
                row["G"] = color.G;
                row["B"] = color.B;

                PictureDatabase._dataSource.Rows.Add(row);
            }
        }

        /// <summary>
        /// Updates a record
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <returns>Gets the amount of records updated</returns>
        public int UPDATE(int x, int y, Color color)
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                var rowArray =
                    PictureDatabase._dataSource.Select(
                        "SESSIONID='@SESSIONID' AND X = @X AND Y = @Y"
                            .Replace("@SESSIONID", this._sessionID)
                            .Replace("@X", x.ToString())
                            .Replace("@Y", y.ToString()), "", DataViewRowState.CurrentRows);

                foreach (var rowItem in rowArray)
                {
                    rowItem.BeginEdit();
                    rowItem["A"] = color.A;
                    rowItem["R"] = color.R;
                    rowItem["G"] = color.G;
                    rowItem["B"] = color.B;
                    rowItem.EndEdit();
                }

                // PictureDatabase._dataSource.AcceptChanges();

                return rowArray.Length;
            }
       }

        /// <summary>
        /// Updates colors in all records
        /// </summary>
        /// <returns>Gets the amount of records updated</returns>
        public int UPDATE(Color color, Color replaceColor)
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                var rowArray =
                    PictureDatabase._dataSource.Select(
                            "SESSIONID = '@SESSIONID', A = @A, R = @R, G = @G, B = @B;"
                            .Replace("@SESSIONID", this._sessionID)
                            .Replace("@A", color.A.ToString())
                            .Replace("@R", color.R.ToString())
                            .Replace("@G", color.G.ToString())
                            .Replace("@B", color.B.ToString()), "", DataViewRowState.CurrentRows);

                foreach (var rowItem in rowArray)
                {
                    rowItem.BeginEdit();
                    rowItem["A"] = replaceColor.A;
                    rowItem["R"] = replaceColor.R;
                    rowItem["G"] = replaceColor.G;
                    rowItem["B"] = replaceColor.B;
                    rowItem.EndEdit();
                }

                PictureDatabase._dataSource.AcceptChanges();

                return rowArray.Length;
            }
        }

        /// <summary>
        /// Gets the color information of coordination
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ColorInfo SELECT(int x, int y)
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                var rowArray =
                PictureDatabase._dataSource.Select(
                    "SESSIONID='@SESSIONID' AND X = @X AND Y = @Y"
                        .Replace("@SESSIONID", this._sessionID)
                        .Replace("@X", x.ToString())
                        .Replace("@Y", y.ToString()), "", DataViewRowState.CurrentRows);

                if (rowArray.Length == 0)
                {
                    return null;
                }
                else
                {
                    return new ColorInfo(
                        Color.FromArgb(
                            (int)rowArray[0]["A"],
                            (int)rowArray[0]["R"],
                            (int)rowArray[0]["G"],
                            (int)rowArray[0]["B"]));
                }
            }
        }

        /// <summary>
        /// Gets the pointInfo of the point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public PointInfo SELECT(smartSpriteFX.Pictures.Point point)
        {
            var colorInfo = this.SELECT((int)point.X, (int)point.Y);

            if (colorInfo == null)
            {
                return null;
            }

            return new PointInfo(point, colorInfo.GetInnerColor());
        }

        /// <summary>
        /// Gets the points for the color
        /// </summary>
        /// <returns></returns>
        public List<smartSuite.smartSpriteFX.Pictures.Point> SELECT(Color color)
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                var rowArray =
                PictureDatabase._dataSource.Select(
                    "SESSIONID = '@SESSIONID' AND A = @A AND R = @R AND G = @G AND B = @B"
                        .Replace("@SESSIONID", this._sessionID)
                        .Replace("@A", color.A.ToString())
                        .Replace("@R", color.R.ToString())
                        .Replace("@G", color.G.ToString())
                        .Replace("@B", color.B.ToString()), "", DataViewRowState.CurrentRows);

                List<smartSuite.smartSpriteFX.Pictures.Point> returnValue = new List<smartSpriteFX.Pictures.Point>();

                foreach (var rowItem in rowArray)
                {
                    smartSuite.smartSpriteFX.Pictures.Point point =
                        new smartSpriteFX.Pictures.Point(
                                    (int)rowItem["X"],
                                    (int)rowItem["Y"]);
                    returnValue.Add(point);
                }

                return returnValue;
            }
        }

        /// <summary>
        /// Gets all the colors
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public List<PointInfo> SELECTALL()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                var rowArray =
                        PictureDatabase._dataSource.Select(
                            "SESSIONID = '@SESSIONID'"
                                .Replace("@SESSIONID", this._sessionID), "", DataViewRowState.CurrentRows);

                List<PointInfo> returnValue = new List<PointInfo>();

                foreach (var rowItem in rowArray)
                {
                    PointInfo point =
                        new PointInfo(
                            new smartSpriteFX.Pictures.Point(
                                        (int)rowItem["X"],
                                        (int)rowItem["Y"]),
                            Color.FromArgb(
                                (int)rowItem["A"],
                                (int)rowItem["R"],
                                (int)rowItem["G"],
                                (int)rowItem["B"]));

                    returnValue.Add(point);
                }

                return returnValue;
            }
        }

        /// <summary>
        /// Gets the list of results using custom filter
        /// </summary>
        /// <param name="whereClause">A commandString such as WHERE clause in SQL format to filter the datas.</param>
        /// <param name="parameterList">Zero or more parameters (starting by "@") to suply values to commandString. Use <see cref="PictureDatabase.CreateDbParameter(string, object)"/> method to create it.</param>
        /// <example>
        /// X=1 AND Y=3 AND SESSIONID='@SESSIONID';
        /// </example>
        /// <returns>
        /// The columns of TB_PICTURE are:
        /// <list type="bullet">
        /// <item>SESSIONID - It´s the identification of current image</item>
        /// <item>X - It´s the coordinate X</item>
        /// <item>Y - It´s the coordinate Y</item>
        /// <item>A - It´s the component A of color</item>
        /// <item>R - It´s the component R of color</item>
        /// <item>G - It´s the component G of color</item>
        /// <item>B - It´s the component B of color</item>
        /// </list>
        /// </returns>
        public IDataReader SELECT(String whereClause, params DbParameter[] parameterList)
        {
            DataRow[] rowArray = SELECT2(whereClause, parameterList);
            return new RowDataReader(rowArray);
        }

        /// <summary>
        /// Gets the list of results using custom filter
        /// </summary>
        /// <param name="whereClause">A commandString such as WHERE clause in SQL format to filter the datas.</param>
        /// <param name="parameterList">Zero or more parameters (starting by "@") to suply values to commandString. Use <see cref="PictureDatabase.CreateDbParameter(string, object)"/> method to create it.</param>
        /// <example>
        /// X=1 AND Y=3 AND SESSIONID='@SESSIONID';
        /// </example>
        /// <returns>
        /// The columns of TB_PICTURE are:
        /// <list type="bullet">
        /// <item>SESSIONID - It´s the identification of current image</item>
        /// <item>X - It´s the coordinate X</item>
        /// <item>Y - It´s the coordinate Y</item>
        /// <item>A - It´s the component A of color</item>
        /// <item>R - It´s the component R of color</item>
        /// <item>G - It´s the component G of color</item>
        /// <item>B - It´s the component B of color</item>
        /// </list>
        /// </returns>
        public DataRow[] SELECT2(string whereClause, params DbParameter[] parameterList)
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            String where = "(" + whereClause + ")";

            where +=
                "AND SESSIONID = '@SESSIONID'"
                        .Replace("@SESSIONID", this._sessionID);

            foreach (var parameterItem in parameterList)
            {
                where = where
                    .Replace(parameterItem.ParameterName, parameterItem.Value.ToString());
            }

            lock (PictureDatabase._dataSource)
            {
                return PictureDatabase._dataSource.Select(where, "", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// Creates and returns the parameter to use with <see cref="PictureDatabase.SELECT(string, KeyValuePair{string, object}[])"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(String name, Object value)
        {
            return new OleDbParameter(name, value);
        }

        /// <summary>
        /// Counts the amount of colors
        /// </summary>
        /// <returns></returns>
        public long COUNT()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            String commandString =
                "SESSIONID = '@SESSIONID'"
                    .Replace("@SESSIONID", this._sessionID);

            lock (PictureDatabase._dataSource)
            {
                return Convert.ToInt64(
                    PictureDatabase._dataSource.Compute("COUNT(SESSIONID)", commandString));
            }
        }

        /// <summary>
        /// Counts the amount of colors for the critera
        /// </summary>
        /// <returns></returns>
        public long COUNT(smartSpriteFX.Pictures.Point point, Color color)
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            String commandString =
                "SESSIONID = '@SESSIONID' AND X = @X AND Y = @Y AND A = @A AND R = @R AND G = @G AND B = @B"
                        .Replace("@SESSIONID", this._sessionID)
                        .Replace("@X", point.X.ToString())
                        .Replace("@Y", point.Y.ToString())
                        .Replace("@A", color.A.ToString())
                        .Replace("@R", color.R.ToString())
                        .Replace("@G", color.G.ToString())
                        .Replace("@B", color.B.ToString());

            lock (PictureDatabase._dataSource)
            {
                return (long)PictureDatabase._dataSource.Compute("COUNT(*)", commandString);
            }
        }

        /// <summary>
        /// Checks if exists the color for the coordinates
        /// </summary>
        /// <returns></returns>
        public bool EXISTS(smartSpriteFX.Pictures.Point point, Color color)
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            String commandString =
                "SESSIONID = '@SESSIONID' AND X = @X AND Y = @Y AND A = @A AND R = @R AND G = @G AND B = @B"
                        .Replace("@SESSIONID", this._sessionID)
                        .Replace("@X", point.X.ToString())
                        .Replace("@Y", point.Y.ToString())
                        .Replace("@A", color.A.ToString())
                        .Replace("@R", color.R.ToString())
                        .Replace("@G", color.G.ToString())
                        .Replace("@B", color.B.ToString());

            lock (PictureDatabase._dataSource)
            {
                var reader = PictureDatabase._dataSource.CreateDataReader();
                return reader.Read();
            }
        }

        /// <summary>
        /// Clear all the cache
        /// </summary>
        internal static void Clear()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                return;
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                PictureDatabase._dataSource.RejectChanges();
                PictureDatabase._dataSource.Clear();
                PictureDatabase._dataSource.AcceptChanges();
            }
        }

        /// <summary>
        /// Deletes all the content of table
        /// </summary>
        public void CLEAR()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            String commandString = "SESSIONID = '@SESSIONID'"
                    .Replace("@SESSIONID", this._sessionID);

            lock (PictureDatabase._dataSource)
            {
                var rowList = PictureDatabase._dataSource.Select(commandString, "", DataViewRowState.CurrentRows);
                foreach (var rowItem in rowList)
                {
                    PictureDatabase._dataSource.Rows.Remove(rowItem);
                }
                PictureDatabase._dataSource.AcceptChanges();
            }
        }

        /// <summary>
        /// Clones the database
        /// </summary>
        /// <returns></returns>
        public PictureDatabase Clone()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            PictureDatabase returnValue = new PictureDatabase();
            returnValue._sessionID = Guid.NewGuid().ToString();

            returnValue.CLEAR();

            lock (PictureDatabase._dataSource)
            {
                var sourceRowList =
                    PictureDatabase._dataSource.Select(
                        "SESSIONID='@SESSIONID'"
                            .Replace("@SESSIONID", this._sessionID), "", DataViewRowState.CurrentRows);

                foreach (var sourceRowItem in sourceRowList)
                {
                    var targetRow = 
                        PictureDatabase._dataSource.NewRow();

                    targetRow["SESSIONID"] = returnValue._sessionID;
                    targetRow["X"] = sourceRowItem["X"];
                    targetRow["Y"] = sourceRowItem["Y"];
                    targetRow["A"] = sourceRowItem["A"];
                    targetRow["R"] = sourceRowItem["R"];
                    targetRow["G"] = sourceRowItem["G"];
                    targetRow["B"] = sourceRowItem["B"];

                    PictureDatabase._dataSource.Rows.Add(targetRow);
                }
                PictureDatabase._dataSource.AcceptChanges();
            }

            return returnValue;
        }

        /// <summary>
        /// Merge other dataBase in the current dataBase
        /// </summary>
        /// <param name="other"></param>
        public void Merge(PictureDatabase other)
        {
            #region Entries validation

            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            #endregion

            this.CLEAR();

            // Replicating the datas
            lock (PictureDatabase._dataSource)
            {
                var sourceRowList =
                    PictureDatabase._dataSource.Select(
                        "SESSIONID='@SESSIONID'"
                            .Replace("@SESSIONID", other._sessionID), "", DataViewRowState.CurrentRows);

                foreach (var sourceRowItem in sourceRowList)
                {
                    var targetRow =
                        PictureDatabase._dataSource.NewRow();

                    targetRow["SESSIONID"] = this._sessionID;
                    targetRow["X"] = sourceRowItem["X"];
                    targetRow["Y"] = sourceRowItem["Y"];
                    targetRow["A"] = sourceRowItem["A"];
                    targetRow["R"] = sourceRowItem["R"];
                    targetRow["G"] = sourceRowItem["G"];
                    targetRow["B"] = sourceRowItem["B"];

                    PictureDatabase._dataSource.Rows.Add(targetRow);
                }
                PictureDatabase._dataSource.AcceptChanges();
            }
        }

        /// <summary>
        /// Gets the amount of colors of database
        /// </summary>
        /// <param name="other"></param>
        public long CountColor()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                var result = from rowItem in PictureDatabase._dataSource.AsEnumerable()
                             where rowItem.Field<string>("SESSIONID") == this._sessionID && (rowItem.RowState == DataRowState.Unchanged || rowItem.RowState == DataRowState.Added)
                             group rowItem by new
                             {
                                 A = rowItem.Field<int>("A"),
                                 R = rowItem.Field<Int32>("R"),
                                 G = rowItem.Field<Int32>("G"),
                                 B = rowItem.Field<Int32>("B")
                             } into groupItem
                             select groupItem;

                return result.Count();
            }
        }

        /// <summary>
        /// Gets a version of main dataSource for use in Linq filter
        /// </summary>
        /// <returns></returns>
        public EnumerableRowCollection<DataRow> AsEnumerable()
        {
            return PictureDatabase._dataSource.AsEnumerable();
        }

        /// <summary>
        /// Begins the transaction
        /// </summary>
        public void beginTransaction()
        {
            // HACK: Initially, this was prepared for SQlite and another databases.
            this._transactionStartTime = DateTime.Now;
        }

        /// <summary>
        /// Rolls back the transaction
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void rollbackTransaction()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                try
                {
                    PictureDatabase._dataSource.RejectChanges();
                }
                catch
                {
                    // Errors here can't turn around the flow
                }
                this.printTransactionTime(new StackFrame(1, true).GetMethod().Name);
            }
        }

        /// <summary>
        /// Commits the transaction
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void commitTransaction()
        {
            #region Entries validation

            if (PictureDatabase._dataSource == null)
            {
                throw new ArgumentNullException("PictureDatabase._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (PictureDatabase._dataSource)
            {
                try
                {
                    PictureDatabase._dataSource.AcceptChanges();
                }
                finally
                {
                    this.printTransactionTime(new StackFrame(1, true).GetMethod().Name);
                }
            }
        }

        /// <summary>
        /// Prints the transaction time
        /// </summary>
        private void printTransactionTime(String calledMethod)
        {
            #region Entries validation

            if (!this._transactionStartTime.HasValue)
            {
                return;
            }

            #endregion

            TimeSpan time = DateTime.Now.Subtract(this._transactionStartTime.Value);
            Console.WriteLine("[" + calledMethod + "] Ending transaction. Time: " + time.ToString(@"hh\:mm\:ss"));
            this._transactionStartTime = null;
        }
    }
}
