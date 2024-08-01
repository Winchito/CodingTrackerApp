using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CodingTrackerApp.Data
{
    internal class DatabaseManager
    {

        internal void CreateTable(string connectionString)
        {
            using (var sqliteConnection = new SQLiteConnection(connectionString))
            {
                using (var command = sqliteConnection.CreateCommand())
                {
                    sqliteConnection.Open();

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Coding (Id INTEGER PRIMARY KEY AUTOINCREMENT, StartTime TEXT, EndTime TEXT, Duration TEXT)";

                    command.ExecuteNonQuery();

                    sqliteConnection.Close();
                }
            }
        }


    }
}
