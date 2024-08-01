using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace CodingTrackerApp.Models
{
    internal class TableVisualization
    {

        internal static void ShowTable(List<CodingSession> tableValues)
        {
            var table = new Table();

            table.AddColumn("ID");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Duration");

            foreach (var codingSession in tableValues)
            {
                table.AddRow($"{codingSession.Id}", $"{codingSession.StartTime}", $"{codingSession.EndTime}", $"{codingSession.Duration}");
            }

            AnsiConsole.Write(table);

        }

    }
}
