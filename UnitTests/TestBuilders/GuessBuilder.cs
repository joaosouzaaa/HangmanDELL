using HangmanDELL.API.Arguments;
using HangmanDELL.API.DataTransferObjects.Guess;
using HangmanDELL.API.Entities;

namespace UnitTests.TestBuilders;
public sealed class GuessBuilder
{
    private char _guessedLetter = 'a';
    private DateTime _creation = DateTime.Now;
    private char _letterToGuess = 'a';

    public static GuessBuilder NewObject() =>
        new GuessBuilder();

    public Guess DomainBuild() =>
        new Guess()
        {
            GuessedLetter = _guessedLetter,
            Creation = _creation,
            IsSuccess = true
        };

    public GuessResponse ResponseBuild() =>
        new GuessResponse()
        {
            Creation = _creation,
            GuessedLetter = _guessedLetter
        };

    public GuessWordArgument WordArgumentBuild() =>
        new GuessWordArgument()
        {
            IpAddress = "127.0.0.1",
            LetterToGuess = _letterToGuess,
            IpPort = 1
        };

    public GuessBuilder WithLetterToGuess(char letterToGuess)
    {
        _letterToGuess = letterToGuess;

        return this;
    }
}
