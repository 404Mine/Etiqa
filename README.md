# Assessment Project

The code works by opening the sln file and then clicking run.

I configured it to run both backend and frontend at the same time
Removed swagger browser launch for backend after successful integration of UI to API

===========================================================================


How the code works
 - configure ConnectionStrings if needed
 - navigate to EmployeeAPI\EmployeeAPI directory
 - run dotnet tool install --global dotnet-ef in CLI
 - run dotnet ef database update (uses MySQL and SQL Server Management Studio 22)
 - then open the sln file, and run (both backend and frontend will run automatically since i set it up that way)

Demonstration
    The ui is pretty simple and self explanatory.
 - Start by loading employees
 - or create one
 - once shown in table, it has edit and delete functionality
 - use EmployeeNumber in computing takehomepay
