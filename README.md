<i>Clean-Architecture backend</i>

<b>Installation</b>

To run this backend app you need to have the following installed on your computer:<br/><b>.NET SDK.</b> for .NET CLI(command line interface). It is availible on https://dotnet.microsoft.com/en-us/download. <br/>This project was made using <b>.NET 8.0</b>.


Steps to install the project:
```
git clone git@github.com:DorianLeci/Internship-4-OOP2.git
cd Internship-4-OOP
cd Internship-4-OOP.Api
```

Before starting program you must create PostgreSQL database in PgAdmin.
In this repo there are .txt files which contain instructions for creating databases and tables.

If you are on mac/linux start program with 
```
ASPNETCORE_ENVIRONMENT=Development dotnet run
```

If you are on windows start program with 
```
$Env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run
```
After starting program type ```http://localhost:5245/swagger/index.html``` (or other number-whatever port os gives you) to open SwaggerUI where you can test various API end-points.







