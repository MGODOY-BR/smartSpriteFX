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
using System.Threading;
using System.Threading.Tasks;
using smartSuite.smartSpriteFX.Pictures.PixelPatterns;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.Data
{
    /// <summary>
    /// Represents a manager for databases
    /// </summary>
    public class PictureDatabase
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
        /// Relates the key with the point in dataSource
        /// </summary>
        private Dictionary<String, int> _dataSourceIndex;

        /// <summary>
        /// It´s a datasource
        /// </summary>
        private List<PointInfo> _dataSource;

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
        /// Relates the key with the point in dataSource
        /// </summary>
        public Dictionary<string, int> DataSourceIndex { get => _dataSourceIndex; }

        /// <summary>
        /// It´s a datasource
        /// </summary>
        public List<PointInfo> DataSource { get => _dataSource; }

        /// <summary>
        /// Gets a opened database
        /// </summary>
        /// <returns></returns>
        public static PictureDatabase Open()
        {
            PictureDatabase returnValue = new PictureDatabase();
            returnValue._dataSource = new List<PointInfo>();
            returnValue._dataSourceIndex = new Dictionary<string, int>();
            returnValue._sessionID = Guid.NewGuid().ToString();

            return returnValue;
        }

        /// <summary>
        /// Closes the database
        /// </summary>
        public void Close()
        {
            if (this._dataSource == null)
            {
                return;
            }

            this.CLEAR();
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

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (this._dataSourceIndex)
            {
                lock (this._dataSource)
                {
                    var row = new PointInfo(x, y, color);
                    int index = this._dataSourceIndex.Count;
                    var key = row.ToString();

                    try
                    {
                        this._dataSourceIndex.Add(key, index);
                        this._dataSource.Add(row);
                    }
                    catch (ArgumentException ex)
                    {
                        Trace.WriteLine(ex.ToString());

                        index = this._dataSourceIndex[key];
                        this._dataSource[index].Color = color;
                    }
                }
            }
        }

        /// <summary>
        /// Inserts a bunch of points
        /// </summary>
        /// <param name="pointInfoList"></param>
        public void INSERT(List<PointInfo> pointInfoList)
        {
            #region Entries validation

            if (pointInfoList == null)
            {
                throw new ArgumentNullException("pointInfoList");
            }
            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource");
            }
            if (this._dataSourceIndex == null)
            {
                throw new ArgumentNullException("this._dataSourceIndex");
            }

            #endregion

            lock (_dataSourceIndex)
            {
                // Protecting from duplicate keys
                // pointInfoList.RemoveAll(item => _dataSourceIndex.ContainsKey(item.ToString()));
                pointInfoList.RemoveAll(item => item == null || item.Color == Color.Transparent);

                lock (this._dataSource)
                {
                    // HACK: Disabled due to the performance matters:
                    // _dataSource.RemoveAll(item => pointInfoList.Contains(item));
                    this._dataSource.AddRange(pointInfoList);
                }

                // HACK: Removed because of errors in syncronism occured in Merge operation and Load
                //Task taskIndex = new Task(delegate ()
                //{
                    foreach (var pointInfoItem in pointInfoList)
                    {
                        #region Entries validation

                        if(pointInfoItem == null)
                        {
                            continue;
                        }

                    #endregion

                        int index = _dataSourceIndex.Count;
                        if (!_dataSourceIndex.ContainsKey(pointInfoItem.ToString()))
                        {
                            _dataSourceIndex.Add(pointInfoItem.ToString(), index);
                        }
                        else
                        {
                            _dataSourceIndex[pointInfoItem.ToString()] = index;
                        }
                    }
                //});
                //taskIndex.Start();
            }
        }

        /// <summary>
        /// Removes the pixel
        /// </summary>
        /// <returns>An indicator informing that the pixel has been excluded.</returns>
        public bool DELETE(int x, int y)
        {
            #region Entries validation


            #endregion

            lock (this._dataSourceIndex)
            {
                lock (this._dataSource)
                {
                    PointInfo pointInfo = new PointInfo(x, y, Color.Transparent);
                    var key = pointInfo.ToString();

                    #region Entries validation

                    if (!this._dataSourceIndex.ContainsKey(key))
                    {
                        return false;
                    }

                    #endregion

                    var index = this._dataSourceIndex[key];

                    this._dataSource.RemoveAt(index);
                    this._dataSourceIndex.Remove(key);

                    return true;
                }
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

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            PointInfo pointInfoRef = new PointInfo(x, y, color);

            lock (this._dataSourceIndex)
            {
                lock (pointInfoRef.ToString())
                {
                    if (!this._dataSourceIndex.ContainsKey(pointInfoRef.ToString()))
                    {
                        return 0;
                    }

                    var index = this._dataSourceIndex[pointInfoRef.ToString()];
                    this._dataSource[index].Color = color;

                    return 1;
                }
            }
       }

        /// <summary>
        /// Updates colors in all records
        /// </summary>
        /// <returns>Gets the amount of records updated</returns>
        public int UPDATE(Color color, Color replaceColor)
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            var affectResult = from row in this._dataSource
                               where color.ToArgb() == row.Color.ToArgb()
                               select row;

            foreach (var affectRow in affectResult)
            {
                lock (affectRow.ToString())
                {
                    affectRow.Color = replaceColor;
                }
            }

            return affectResult.Count();
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

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (this._dataSourceIndex)
            {
                PointInfo pointInfoRef = new PointInfo(x, y, Color.Transparent);

                if (!this._dataSourceIndex.ContainsKey(pointInfoRef.ToString()))
                {
                    return null;
                }

                int index = this._dataSourceIndex[pointInfoRef.ToString()];
                return new ColorInfo(this._dataSource[index].Color);
            }
        }

        /// <summary>
        /// Gets the pointInfo of the point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public PointInfo SELECT(smartSpriteFX.Pictures.Point point)
        {
            PointInfo pointInfoRef = new PointInfo(point, Color.Transparent);

            lock (this._dataSourceIndex)
            {
                if (!this._dataSourceIndex.ContainsKey(pointInfoRef.ToString()))
                {
                    return null;
                }

                int index = this._dataSourceIndex[pointInfoRef.ToString()];
                return this._dataSource[index];
            }
        }

        /// <summary>
        /// Gets the points for the color
        /// </summary>
        /// <returns></returns>
        public List<PointInfo> SELECT(Color color)
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            var result = from item in this._dataSource
                         where item.Color.ToArgb() == color.ToArgb()
                         select item;

            return result.ToList();
        }

        /// <summary>
        /// Gets all the points
        /// </summary>
        /// <returns></returns>
        public List<PointInfo> SELECTALL()
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            // this._dataSource.Sort();
            return this._dataSource;
        }

        /// <summary>
        /// Counts the amount of pixels
        /// </summary>
        /// <returns></returns>
        public long COUNT()
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            return this._dataSource.Count;
        }

        /// <summary>
        /// Counts the amount of colors for the critera
        /// </summary>
        /// <returns></returns>
        public long COUNT(Color color)
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            var result = from item in this._dataSource
                         where
                            item.Color.ToArgb() == color.ToArgb()
                         group item by item.Color.ToArgb();

            return result.Count();
        }

        /// <summary>
        /// Checks if exists the color for the coordinates
        /// </summary>
        /// <returns></returns>
        public bool EXISTS(smartSpriteFX.Pictures.Point point, Color color)
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (this._dataSourceIndex)
            {
                if (!this.EXISTS(point))
                {
                    return false;
                }

                int index = this._dataSourceIndex[point.ToString()];
                return this._dataSource[index].Color.ToArgb() == color.ToArgb();
            }
        }

        /// <summary>
        /// Checks if exists the color for the coordinates
        /// </summary>
        /// <returns></returns>
        public bool EXISTS(smartSpriteFX.Pictures.Point point)
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            lock (this._dataSourceIndex)
            {
                return this._dataSourceIndex.ContainsKey(point.ToString());
            }
        }

        /// <summary>
        /// Clear all the cache
        /// </summary>
        internal static void Clear()
        {
        }

        /// <summary>
        /// Deletes all the content of table
        /// </summary>
        public void CLEAR()
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }
            if (this._dataSource.Count == 0)
            {
                return;
            }

            #endregion

            lock (this._dataSourceIndex)
            {
                this._dataSource.Clear();
                this._dataSourceIndex.Clear();
            }
        }

        /// <summary>
        /// Clones the database
        /// </summary>
        /// <returns></returns>
        public PictureDatabase Clone()
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            PictureDatabase returnValue = PictureDatabase.Open();
            returnValue._sessionID = Guid.NewGuid().ToString();

            returnValue.CLEAR();

            lock (returnValue._dataSourceIndex)
            {
                lock (this._dataSource)
                {
                    returnValue._dataSource.Clear();
                    returnValue._dataSourceIndex.Clear();

                    var sourceRowList = this._dataSource;

                    #region Starting the indices

                    AutoResetEvent indexSync = new AutoResetEvent(false);

                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
                    {
                        foreach (var sourceRowItem in sourceRowList)
                        {
                            var targetRow = sourceRowItem.Clone();
                            string key = PictureDatabase.FormatKeyIndex(targetRow);

                            if(returnValue._dataSourceIndex.ContainsKey(key))
                            {
                                key += string.Format(" ({0})", Guid.NewGuid().ToString().Substring(0, 8));
                            }

                            returnValue._dataSourceIndex.Add(
                                key,
                                returnValue._dataSourceIndex.Count);
                        }
                        indexSync.Set();
                    }));

                    #endregion

                    #region Starting the data

                    AutoResetEvent dataSync = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate (object state)
                    {
                        foreach (var sourceRowItem in sourceRowList)
                        {
                            var targetRow = sourceRowItem.Clone();

                            returnValue._dataSource.Add(targetRow);
                            // This isn't made here anymore for performance issues:
                            //returnValue._dataSourceIndex.Add(targetRow.ToString(), targetRow);
                        }
                        dataSync.Set();
                    }));

                    #endregion

                    indexSync.WaitOne();
                    dataSync.WaitOne();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Formats the index used in indexes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string FormatKeyIndex(smartSuite.smartSpriteFX.Pictures.Point data)
        {
            #region Entries validation

            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            #endregion

            return data.ToString();
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
            var sourceRowList = other.SELECTALL();

            // Getting the lack points
            var lackPointList = from item in sourceRowList
                                where !this.EXISTS(item)
                                select item;

            // Getting the update points
            var updatePointList = from item in sourceRowList
                                  where this.EXISTS(item)
                                  select item;

            // Updating the update points
            foreach (var updateItem in updatePointList)
            {
                this.UPDATE((int)updateItem.X, (int)updateItem.Y, updateItem.Color);
            }

            // Adding the lack points
            this.INSERT(lackPointList.ToList());
        }

        /// <summary>
        /// Gets all the colors of database
        /// </summary>
        /// <param name="other"></param>
        public List<Color> GetAllColors()
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            var result = from item in this._dataSource
                         group item by item.Color into g
                         select g.Key;

            return result.ToList();
        }

        /// <summary>
        /// Gets the amount of colors of database
        /// </summary>
        /// <param name="other"></param>
        public long CountColor()
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            var result = from item in this._dataSource
                         group item by item.Color;

            return result.Count();
        }

        /// <summary>
        /// Begins the transaction
        /// </summary>
        public void BeginTransaction()
        {
            // HACK: Initially, this was prepared for SQlite and another databases.
            this._transactionStartTime = DateTime.Now;
        }

        /// <summary>
        /// Rolls back the transaction
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RollbackTransaction()
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            this.PrintTransactionTime(new StackFrame(2, true).GetMethod().Name);
        }

        /// <summary>
        /// Commits the transaction
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CommitTransaction()
        {
            #region Entries validation

            if (this._dataSource == null)
            {
                throw new ArgumentNullException("this._dataSource", "Connection hadn't been opened yet.");
            }

            #endregion

            this.PrintTransactionTime(new StackFrame(2, true).GetMethod().Name);
        }

        /// <summary>
        /// Prints the transaction time
        /// </summary>
        private void PrintTransactionTime(String calledMethod)
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
