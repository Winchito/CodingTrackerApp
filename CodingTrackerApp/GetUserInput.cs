using System;
using System.Globalization;
using CodingTrackerApp.Models;
using Spectre.Console;

namespace CodingTrackerApp
{
    internal class GetUserInput
    {
        private bool closeApp = false;
        private CodingController codingController = new CodingController();
        internal void MainMenu()
        {
            
            while (closeApp == false)
            {
                var option = AnsiConsole.Ask<int>("-------MAIN MENU-------\n1.[lime]Create Record for Coding[/]\n2.[yellow]Read Coding Records[/]\n3.[aqua]Update Coding Record[/]\n4.[maroon]Delete Coding Record[/]\nSelect an option: ");
                Console.Clear();
                switch (option) 
                { 
                    case 1:
                        CreateUserRecord();
                        break;
                    case 2:
                        ShowUserRecords();
                        break;
                    case 3:
                        UpdateUserRecord();
                        break;
                    case 4:
                        DeleteUserRecord();
                        break;
                    default:
                        AnsiConsole.Markup("[red]Select a valid option![/]\n");
                        break;
                }


            }
        }
        private void CreateUserRecord()
        {
            DateTime startDate = InsertStartDate();

            DateTime endDate = InsertEndDate();

            string duration = GetDurationInput(startDate, endDate);

            CodingSession codingSession = new CodingSession
            {
                StartTime = startDate.ToString("dd/MM/yyyy HH:mm"),
                EndTime = endDate.ToString("dd/MM/yyyy HH:mm"),
                Duration = duration
            };

            codingController.Post(codingSession);
        }

        private DateTime InsertStartDate()
        {
            DateTime startDateTime;

            string startDateInput = AnsiConsole.Ask<string>("Insert the [darkslategray2]start date[/] in this format [green]\"dd/MM/yyyy HH:mm[/]\", [fuchsia] type 0 to exit[/]: ");

            if (startDateInput == "0") MainMenu();

            while (!DateTime.TryParseExact(startDateInput, "dd/MM/yyyy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out startDateTime))
            {
                startDateInput = AnsiConsole.Ask<string>("[red]Not a valid date![/] Please insert the [green]\"dd/MM/yyyy HH:mm[/]\" format or [fuchsia] type 0 to exit[/]:  ");

                if (startDateInput == "0") MainMenu();
            }

            return startDateTime;
        }

        private DateTime InsertEndDate()
        {
            DateTime endDateTime;

            string endDateInput = AnsiConsole.Ask<string>("Insert the [cornflowerblue]end date[/] in this format [green]\"dd/MM/yyyy HH:mm[/]\", [fuchsia] type 0 to exit[/]: ");

            if (endDateInput == "0") MainMenu();

            while (!DateTime.TryParseExact(endDateInput, "dd/MM/yyyy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out endDateTime))
            {
                endDateInput = AnsiConsole.Ask<string>("[red]Not a valid date![/] Please insert the [green]\"dd/MM/yyyy HH:mm[/]\" format or or [fuchsia] type 0 to exit[/]: ");

                if (endDateInput == "0") MainMenu();
            }

            return endDateTime;
        }

        private string GetDurationInput(DateTime startDate, DateTime endDate)
        {
            var durationInput = endDate - startDate;

            return $"{durationInput.Hours}:{durationInput.Minutes}:{durationInput.Seconds}";
        }

        private void ShowUserRecords()
        {
            codingController.Get();
        }
        private void UpdateUserRecord()
        {
            codingController.Get();

            var rowId = AnsiConsole.Ask<int>("Select the ID you want to update: ");

            bool rowExists = codingController.RowExistsById(rowId);

            if(rowExists)
            {
                string newDuration;
                DateTime newStartDate;
                DateTime newEndDate;
                var oldRowValues = codingController.GetById(rowId);

                var option = AnsiConsole.Ask<int>("0. Cancel Operation\n1.[orange1]Edit Start Time [/]\n2.[gold1]Edit End Time[/]\n3.[dodgerblue2]Edit both [/]\nSelect an option: ");

                switch (option)
                {
                    case 0:
                        break;
                    case 1:
                        newStartDate = InsertStartDate();
                        newDuration = GetDurationInput(newStartDate, DateTime.Parse(oldRowValues.EndTime));
                        codingController.Update(rowId, option, newDuration, newStartDate.ToString("dd/MM/yyyy HH:mm"));
                        return;
                    case 2:
                        newEndDate = InsertEndDate();
                        newDuration = GetDurationInput(DateTime.Parse(oldRowValues.StartTime), newEndDate);
                        codingController.Update(rowId, option, newDuration, newEndDate: newEndDate.ToString("dd/MM/yyyy HH:mm"));
                        return;
                    case 3:
                        newStartDate = InsertStartDate();
                        newEndDate = InsertEndDate();
                        newDuration = GetDurationInput(newStartDate, newEndDate);
                        codingController.Update(rowId, option, newDuration, newStartDate.ToString("dd/MM/yyyy HH:mm"), newEndDate.ToString());
                        return;
                    default:
                        AnsiConsole.Markup("[red]Select a valid option![/]\n");
                        break;

                }
            }
            else
            {
                AnsiConsole.Markup("[red]The ID doesn't exist![/]\n");
                return;
            }

        }

        private void DeleteUserRecord()
        {
            codingController.Get();

            var rowId = AnsiConsole.Ask<int>("Select the ID you want to Delete: ");

            bool rowExists = codingController.RowExistsById(rowId);

            if (rowExists)
            {
                codingController.Delete(rowId);
            }
            else
            {
                AnsiConsole.Markup("[red]The ID doesn't exist![/]\n");
                return;
            }
        }

    }
}