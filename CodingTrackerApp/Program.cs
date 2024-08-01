using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using CodingTrackerApp.Data;
using System.Data.SQLite;
using Spectre.Console;



namespace CodingTrackerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbConnString = ConfigurationManager.AppSettings
    .Get("SQLiteConnectionString") ?? throw new ArgumentNullException("missing 'SQLiteConnectionString' in App.config");

            DatabaseManager dbManager = new DatabaseManager();

            dbManager.CreateTable(dbConnString);

            GetUserInput userInput = new GetUserInput();

            userInput.MainMenu();

        }
    }
}
