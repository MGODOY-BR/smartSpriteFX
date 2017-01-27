using smartSuite.smartSpriteFX.Pictures.ColorPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.Data
{
    /// <summary>
    /// Represents a manager for databases
    /// </summary>
    internal class PictureDatabase
    {
        /// <summary>
        /// It´s the current connection
        /// </summary>
        private SQLiteConnection _currentConnection;

        /// <summary>
        /// This constructor has been created for goals of design and can not be used for external calls
        /// </summary>
        private PictureDatabase()
        {
        }

        /// <summary>
        /// Gets a opened database
        /// </summary>
        /// <returns></returns>
        public static PictureDatabase Open()
        {
            PictureDatabase returnValue = new PictureDatabase(); 
            returnValue._currentConnection = new SQLiteConnection("Data Source=:memory:;");
            returnValue._currentConnection.Open();

            return returnValue;
        }

        /// <summary>
        /// Closes the database
        /// </summary>
        public void Close()
        {
            if (this._currentConnection == null)
            {
                return;
            }

            this._currentConnection.Dispose();
            this._currentConnection = null;
        }

        /// <summary>
        /// Creates the database in memory.
        /// </summary>
        public void CreateDatabase()
        {
            #region Entries validation

            if (this._currentConnection == null)
            {
                throw new ArgumentNullException("this._currentConnection", "Connection hadn't been opened yet.");
            }

            #endregion

            using (SQLiteCommand command = this._currentConnection.CreateCommand())
            {
                command.CommandText =
                    @"CREATE TABLE TB_PICTURE(
                        X INTEGER  NOT NULL,
                        Y INTEGER  NOT NULL,
                        A INTEGER  NOT NULL,
                        R INTEGER  NOT NULL,
                        G INTEGER  NOT NULL,
                        B INTEGER  NOT NULL,
                        PRIMARY KEY (X, Y)
                    );";

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserts a record
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="colorInfo"></param>
        public void INSERT(int x, int y, ColorInfo colorInfo)
        {
            String commandString =
                "INSERT INTO TB_PICTURE (X, Y, A, R, G, B) VALUES (@X, @Y, @A, @R, @G, @B);";
            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                var color = colorInfo.GetInnerColor();

                command.Parameters.AddWithValue("@X", x);
                command.Parameters.AddWithValue("@Y", y);
                command.Parameters.AddWithValue("@A", color.A);
                command.Parameters.AddWithValue("@R", color.R);
                command.Parameters.AddWithValue("@G", color.G);
                command.Parameters.AddWithValue("@B", color.B);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates a record
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="colorInfo"></param>
        /// <returns>Gets the amount of records updated</returns>
        public int UPDATE(int x, int y, ColorInfo colorInfo)
        {
            String commandString =
                "UPDATE TB_PICTURE SET A = @A, R = @R, G = @G, B = @B WHERE X = @X AND Y = @Y;";
            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                var color = colorInfo.GetInnerColor();

                command.Parameters.AddWithValue("@X", x);
                command.Parameters.AddWithValue("@Y", y);
                command.Parameters.AddWithValue("@A", color.A);
                command.Parameters.AddWithValue("@R", color.R);
                command.Parameters.AddWithValue("@G", color.G);
                command.Parameters.AddWithValue("@B", color.B);

                return command.ExecuteNonQuery();
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
            String commandString =
                "SELECT A, R, G, B FROM TB_PICTURE WHERE x=@X AND y=@Y;";

            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@X", x);
                command.Parameters.AddWithValue("@Y", y);

                var reader = command.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                while (reader.Read())
                {
                    Color color =
                        Color.FromArgb(
                            Convert.ToByte(reader.GetInt32(0)),
                            Convert.ToByte(reader.GetInt32(1)),
                            Convert.ToByte(reader.GetInt32(2)),
                            Convert.ToByte(reader.GetInt32(3)));

                    return new ColorInfo(color);
                }

                return null;
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
            String commandString =
                "SELECT X, Y, A, R, G, B FROM TB_PICTURE;";

            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                List<PointInfo> returnValue = new List<PointInfo>();

                var reader = command.ExecuteReader(System.Data.CommandBehavior.Default);
                while (reader.Read())
                {
                    Color color =
                        Color.FromArgb(
                            Convert.ToByte(reader.GetInt32(2)),
                            Convert.ToByte(reader.GetInt32(3)),
                            Convert.ToByte(reader.GetInt32(4)),
                            Convert.ToByte(reader.GetInt32(5)));

                    returnValue.Add(
                        new PointInfo(
                            reader.GetFloat(0), 
                            reader.GetFloat(1),
                            color));
                }
                return returnValue;
            }
        }

        /// <summary>
        /// Deletes all the content of table
        /// </summary>
        public void CLEAR()
        {
            String commandString = "DELETE FROM TB_PICTURE;";
            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
