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
        /// It´s the sessionID of datas.
        /// </summary>
        private String _sessionID;

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
            returnValue._sessionID = Guid.NewGuid().ToString();

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
                        SESSIONID TEXT NOT NULL,
                        X INTEGER  NOT NULL,
                        Y INTEGER  NOT NULL,
                        A INTEGER  NOT NULL,
                        R INTEGER  NOT NULL,
                        G INTEGER  NOT NULL,
                        B INTEGER  NOT NULL,
                        PRIMARY KEY (SESSIONID, X, Y)
                    );";

                command.ExecuteNonQuery();
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
            String commandString =
                "INSERT INTO TB_PICTURE (SESSIONID, X, Y, A, R, G, B) VALUES (@SESSIONID, @X, @Y, @A, @R, @G, @B);";
            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
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
        /// <param name="color"></param>
        /// <returns>Gets the amount of records updated</returns>
        public int UPDATE(int x, int y, Color color)
        {
            String commandString =
                "UPDATE TB_PICTURE SET A = @A, R = @R, G = @G, B = @B WHERE SESSIONID=@SESSIONID AND X = @X AND Y = @Y;";
            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
                command.Parameters.AddWithValue("@X", x);
                command.Parameters.AddWithValue("@Y", y);
                command.Parameters.AddWithValue("@A", (object)color.A);
                command.Parameters.AddWithValue("@R", (object)color.R);
                command.Parameters.AddWithValue("@G", (object)color.G);
                command.Parameters.AddWithValue("@B", (object)color.B);

                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates colors in all records
        /// </summary>
        /// <returns>Gets the amount of records updated</returns>
        public int UPDATE(Color color, Color replaceColor)
        {
            String commandString =
                "UPDATE TB_PICTURE SET A = @A, R = @R, G = @G, B = @B WHERE SESSIONID = @SESSIONID, A = @A_NEW, R = @R_NEW, G = @G_NEW, B = @B_NEW;";
            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
                command.Parameters.AddWithValue("@A", color.A);
                command.Parameters.AddWithValue("@R", color.R);
                command.Parameters.AddWithValue("@G", color.G);
                command.Parameters.AddWithValue("@B", color.B);

                command.Parameters.AddWithValue("@A_NEW", replaceColor.A);
                command.Parameters.AddWithValue("@R_NEW", replaceColor.R);
                command.Parameters.AddWithValue("@G_NEW", replaceColor.G);
                command.Parameters.AddWithValue("@B_NEW", replaceColor.B);

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
                "SELECT A, R, G, B FROM TB_PICTURE WHERE SESSIONID=@SESSIONID AND x=@X AND y=@Y;";

            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
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
            String commandString =
                "SELECT X, Y FROM TB_PICTURE WHERE SESSIONID=@SESSIONID AND A=@A AND R=@R AND G=@G AND B=@B;";

            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
                command.Parameters.AddWithValue("@A", color.A);
                command.Parameters.AddWithValue("@R", color.R);
                command.Parameters.AddWithValue("@G", color.G);
                command.Parameters.AddWithValue("@B", color.B);

                var reader = command.ExecuteReader(System.Data.CommandBehavior.Default);
                List<smartSuite.smartSpriteFX.Pictures.Point> returnValue = new List<smartSuite.smartSpriteFX.Pictures.Point>();
                while (reader.Read())
                {
                    returnValue.Add(
                        new smartSuite.smartSpriteFX.Pictures.Point(
                            reader.GetInt32(0),
                            reader.GetInt32(1)));
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
            String commandString =
                "SELECT X, Y, A, R, G, B FROM TB_PICTURE WHERE SESSIONID=@SESSIONID;";

            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                List<PointInfo> returnValue = new List<PointInfo>();

                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
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
        /// Counts the amount of colors
        /// </summary>
        /// <returns></returns>
        public long COUNT()
        {
            String commandString =
                "SELECT COUNT(*) FROM TB_PICTURE WHERE SESSIONID = @SESSIONID;";

            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
                return (long)command.ExecuteScalar(CommandBehavior.SingleResult);
            }
        }

        /// <summary>
        /// Deletes all the content of table
        /// </summary>
        public void CLEAR()
        {
            String commandString = "DELETE FROM TB_PICTURE WHERE SESSIONID = @SESSIONID;";
            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Clones the database
        /// </summary>
        /// <returns></returns>
        public PictureDatabase Clone()
        {
            #region Entries validation

            if (this._currentConnection == null)
            {
                throw new ArgumentNullException("this._currentConnection");
            }

            #endregion

            PictureDatabase returnValue = new PictureDatabase();
            returnValue._currentConnection = this._currentConnection;
            returnValue._sessionID = Guid.NewGuid().ToString();

            // Replicating the datas
            String commandString = @"
            INSERT INTO TB_PICTURE 
            (            
                SESSIONID, X, Y, A, R, G, B
            )
            SELECT
                @SESSIONID_NEW, X, Y, A, R, G, B
            FROM TB_PICTURE
            WHERE SESSIONID = @SESSIONID;";
            using (SQLiteCommand command = new SQLiteCommand(commandString, this._currentConnection))
            {
                command.Parameters.AddWithValue("@SESSIONID", this._sessionID);
                command.Parameters.AddWithValue("@SESSIONID_NEW", returnValue._sessionID);
                command.ExecuteNonQuery();
            }

            return returnValue;
        }
    }
}
