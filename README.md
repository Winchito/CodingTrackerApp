# CodingTrackerApp

## Intro

I made a Coding Tracker App which performs CRUD actions to a SQLIteDatabase created on local, the Database is called **"CodingTracker.db"**. On App.config there is a Key called SQLiteConnectionString whose purpose is to stablish a string to the Database. 

The Db is called on Program.cs through a library (System.Configuration) which gets the SQLiteConnectionString value, then creates de Coding table from Data/DataBaseMagnager.cs

The ConsoleApp looks like this

![image](https://github.com/user-attachments/assets/b77b54a9-c881-4830-82be-8ef335499d25)

As the image shows, you can Create, Read, Update and Delete Coding Records the CodingController.cs performs those operations. The outputs on console were made with Spectre.Console library.


# Create Method

![image](https://github.com/user-attachments/assets/7458cd47-fbe0-4d2c-b8d9-41ae10d20acb)


Asks for a Start Date and a End Date, if any input is not valid it wont let the user to continue

![image](https://github.com/user-attachments/assets/3fb10be1-6277-4b10-966e-cf2685417c67)

If valid, it will create a Record on the table Database


#Read Method (WIP)

![image](https://github.com/user-attachments/assets/07a4b1aa-ecb4-4d70-9291-815b36d92f3d)

Shows the values stored in the table.

# Update Method

![image](https://github.com/user-attachments/assets/4d95cb01-4333-4dc6-b0b7-139b2b84b6c2)


Shows the table and ask the user for an id to Update, if the Id does not exist, it will return to the main menu, if the id exists, it will ask the user if he wants to edit the Start Time, the End Time or Both. In any case it will update the Date and the Duration (Cause is a new Date)

#Delete Method

Just shows the table and asks the user for an Id, if the Id does not exist, it will return to the main menu, if the id exists it will delete it


## Documentation

- Thanks to C# Academy: https://www.thecsharpacademy.com/project/13/coding-tracker
- https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/store-custom-information-config-file (Retrieve information from XML App.Config)
- https://spectreconsole.net/ (Spectre.Console library)
- https://www.learndapper.com/non-query (Dapper Micro-ORM, Performs SQL Querys)
- https://medium.com/@Has_San/datetime-in-c-1aef47db4feb (DateTime in C#)
