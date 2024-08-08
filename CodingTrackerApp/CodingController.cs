using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;
using Spectre.Console;

namespace CodingTrackerApp.Models
{
    internal class CodingController
    {
        string dbConnString = ConfigurationManager.AppSettings.Get("SQLiteConnectionString");
        public void Post(CodingSession codingSession)
        {
            try
            {
                using (var connection = new SQLiteConnection(dbConnString))
                {
                    var sqlSentence = "INSERT INTO Coding (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";

                    connection.Open();

                    var rowsAffected = connection.Execute(sqlSentence, codingSession);
                    Console.WriteLine($"{rowsAffected} row(s) inserted");

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }

        public void Get()
        {
            using (var connection = new SQLiteConnection(dbConnString))
            {

                bool rowsExists = CheckIfRowsExist();

                if (rowsExists)
                {

                    var sqlSentence = "SELECT * FROM Coding";

                    connection.Open();

                    var codingSessions = connection.Query<CodingSession>(sqlSentence).ToList();

                    TableVisualization.ShowTable(codingSessions);

                    connection.Close();

                }
                else
                {
                    AnsiConsole.Markup("[red]Coding Session Updated![/]\n");
                    connection.Close();
                    return;
                }

            }
        }

        private bool CheckIfRowsExist()
        {
            using (var connection = new SQLiteConnection(dbConnString))
            {

                var sqlSentence = "SELECT COUNT(1) FROM Coding";

                connection.Open();

                int count = connection.ExecuteScalar<int>(sqlSentence);

                connection.Close();

                return count > 0;

            }

        }

        public void Update(int idRowToUpdate, int option, string newDuration, string newStartDate = null, string newEndDate = null)
        {
            string sql;
            int affectedRows = 0;
            using (var connection = new SQLiteConnection(dbConnString))
            {
                switch (option)
                {
                    case 1:
                        sql = $"UPDATE Coding SET StartTime = \"{newStartDate}\", Duration = \"{newDuration}\" WHERE Id = {idRowToUpdate}";
                        affectedRows = connection.Execute(sql);
                        break;
                    case 2:
                        sql = $"UPDATE Coding SET EndTime = \"{newEndDate}\", Duration = \"{newDuration}\" WHERE Id = {idRowToUpdate}";
                        affectedRows = connection.Execute(sql);
                        break;
                    case 3:
                        sql = $"UPDATE Coding SET StartTime = \"{newStartDate}\", EndTime = \"{newEndDate}\", Duration = \"{newDuration}\" WHERE Id = {idRowToUpdate}";
                        affectedRows = connection.Execute(sql);
                        break;
                }
                AnsiConsole.Markup("[green]Coding Session Updated![/]\n");
                AnsiConsole.Markup($"[green]Affected rows: {affectedRows}[/]\n");
                connection.Close();
            }

        }

        public bool RowExistsById(int id)
        {
            using (var connection = new SQLiteConnection(dbConnString))
            {

                var sqlSentence = $"SELECT COUNT(1) FROM Coding WHERE Id = {id}";

                connection.Open();

                int count = connection.ExecuteScalar<int>(sqlSentence);

                connection.Close();

                return count > 0;

            }

        }

        public CodingSession GetById(int id)
        {
            using (var connection = new SQLiteConnection(dbConnString))
            {

                var sqlSentence = $"SELECT * FROM Coding WHERE Id = {id}";

                connection.Open();

                var codingSession = connection.QuerySingle<CodingSession>(sqlSentence);

                connection.Close();

                return codingSession;

            }

        }

        public void Delete(int idRow)
        {
            var sql = $"DELETE FROM Coding WHERE Id = {idRow}";

            using (var connection = new SQLiteConnection(dbConnString))
            {
                connection.Open();

                var affectedRows = connection.Execute(sql);

                AnsiConsole.Markup("[green]Coding Session deleted![/]\n");
                AnsiConsole.Markup($"[green]Affected rows {affectedRows}[/]\n");
            }
        }

    }
}
