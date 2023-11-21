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

![alt text](https://github.com/joaosouzaaa/HangmanDELL/blob/master/GitHubImages/setasstartup.png)

If you want to run the database locally change the connection string at "~\HangmanDELL\HangmanDELL.API\appsettings.json", changing the value for "LocalConnectionString":

![alt text](https://github.com/joaosouzaaa/HangmanDELL/blob/master/GitHubImages/localconnectionstringpath.png)

Then at "~\HangmanDELL\HangmanDELL.API\DependencyInjection\DependencyInjectionHandler.cs" change the value inside of "GetConnectionString" parameter call to "LocalConnectionString":

![alt text](https://github.com/joaosouzaaa/HangmanDELL/blob/master/GitHubImages/dependencyinjectionhandlerpath.png)

Run the project:

![alt text](https://github.com/joaosouzaaa/HangmanDELL/blob/master/GitHubImages/run.png)

# How to play the game

Access:
https://hangmandell.azurewebsites.net/swagger/index.html

In the json in "letterToGuess" parameter put the letter that you want to guess. 
Note that:
- It only accept letters (no numbers or full words);
- It does not accept repeated inputs for the word you are guessing at the moment;
- You only have 5 lives.
  
![alt text](https://github.com/joaosouzaaa/HangmanDELL/blob/master/GitHubImages/howtoplay1.png)

The game will tell you when the match you are playing it's over by the parameter called isFinalResult being true:

![alt text](https://github.com/joaosouzaaa/HangmanDELL/blob/master/GitHubImages/howtoplay2.png)
