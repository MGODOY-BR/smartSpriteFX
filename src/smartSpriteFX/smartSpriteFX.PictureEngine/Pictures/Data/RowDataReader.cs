using System;
using System.Data;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.Data
{
    /// <summary>
    /// Represents a data reader for a subset of rows
    /// </summary>
    public class RowDataReader : IDataReader
    {
        /// <summary>
        /// It´s the subset
        /// </summary>
        private DataRow[] _rowArray;

        /// <summary>
        /// It´s the current row index.
        /// </summary>
        private int _rowIndex = -1;

        public RowDataReader(DataRow[] rowArray)
        {
            this._rowArray = rowArray;
        }

        public object this[string name]
        {
            get
            {
                #region Entries validation

                if (this._rowIndex == -1)
                {
                    throw new ApplicationException("You must have called Read() method before.");
                }

                #endregion

                return this._rowArray[this._rowIndex][name];
            }
        }

        public object this[int i]
        {
            get
            {
                #region Entries validation

                if (this._rowIndex == -1)
                {
                    throw new ApplicationException("You must have called Read() method before.");
                }

                #endregion

                return this._rowArray[this._rowIndex][i];
            }
        }

        public int Depth
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public int FieldCount
        {
            get
            {
                #region Entries validation

                if (this._rowIndex == -1)
                {
                    throw new ApplicationException("You must have called Read() method before.");
                }

                #endregion

                return this._rowArray[this._rowIndex].Table.Columns.Count;
            }
        }

        public bool IsClosed
        {
            get
            {
                return this._rowArray  == null;
            }
        }

        public int RecordsAffected
        {
            get
            {
                return this._rowArray.Length;
            }
        }

        public void Close()
        {
            this._rowIndex = -1;
            this._rowArray = null;
        }

        public void Dispose()
        {
            this.Close();
        }

        public bool GetBoolean(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (bool)this._rowArray[this._rowIndex][i];
        }

        public byte GetByte(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (byte)this._rowArray[this._rowIndex][i];
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public char GetChar(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (char)this._rowArray[this._rowIndex][i];
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotSupportedException();
        }

        public string GetDataTypeName(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return this._rowArray[this._rowIndex].Table.Columns[i].GetType().Name;
        }

        public DateTime GetDateTime(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (DateTime)this._rowArray[this._rowIndex][i];
        }

        public decimal GetDecimal(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (decimal)this._rowArray[this._rowIndex][i];
        }

        public double GetDouble(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (double)this._rowArray[this._rowIndex][i];
        }

        public Type GetFieldType(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return this._rowArray[this._rowIndex].Table.Columns[i].GetType();
        }

        public float GetFloat(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (float)this._rowArray[this._rowIndex][i];
        }

        public Guid GetGuid(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return Guid.Parse((string)this._rowArray[this._rowIndex][i]);
        }

        public short GetInt16(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (short)this._rowArray[this._rowIndex][i];
        }

        public int GetInt32(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (int)this._rowArray[this._rowIndex][i];
        }

        public long GetInt64(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (long)this._rowArray[this._rowIndex][i];
        }

        public string GetName(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return this._rowArray[this._rowIndex].Table.Columns[i].ColumnName;
        }

        public int GetOrdinal(string name)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return this._rowArray[this._rowIndex].Table.Columns[name].Ordinal;
        }

        public DataTable GetSchemaTable()
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return this._rowArray[this._rowIndex].Table;
        }

        public string GetString(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return (string)this._rowArray[this._rowIndex][i];
        }

        public object GetValue(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return this._rowArray[this._rowIndex][i];
        }

        public int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        public bool IsDBNull(int i)
        {
            #region Entries validation

            if (this._rowIndex == -1)
            {
                throw new ApplicationException("You must have called Read() method before.");
            }

            #endregion

            return this._rowArray[this._rowIndex][i] == DBNull.Value;
        }

        public bool NextResult()
        {
            throw new NotSupportedException();
        }

        public bool Read()
        {
            if (this._rowArray.Length == this._rowIndex + 1)
            {
                return false;
            }
            if (this._rowArray.Length == 0)
            {
                return false;
            }
            this._rowIndex++;
            return true;
        }
    }
}