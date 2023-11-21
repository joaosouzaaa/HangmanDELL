# HangmanDELL

Access the deployed api here: https://hangmandell.azurewebsites.net/swagger/index.html

Create a web application that implements the Hangman, a word guessing game. 

# Technologies used

- C#;
- .NET 7;
- Azure;
- SQL Server;
- EntityFrameworkCore;
- xUnit;
- Moq

# Architecture, Patterns and Best Practices

- Monolith architecture;
- Code first architecture;
- Mapper pattern;
- Notification pattern;
- Ioc;
- DDD;
- Data Transfer Objects;
- Unit tests coverage;
- Clean Code and SOLID best practices.
 
# Application Setup
Set the api project as startup project:

If you want to run the database locally change the connection string at "~\HangmanDELL\HangmanDELL.API\appsettings.json", changing the value for "LocalConnectionString":

Then at "~\HangmanDELL\HangmanDELL.API\DependencyInjection\DependencyInjectionHandler.cs" change the value inside of "GetConnectionString" parameter call to "LocalConnectionString":

Run the project:
