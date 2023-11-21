using HangmanDELL.API.DataTransferObjects.Guess;
using HangmanDELL.API.DataTransferObjects.History;
using HangmanDELL.API.Entities;

namespace UnitTests.TestBuilders;
public sealed class HistoryBuilder
{
    private int _numberOfLives = 10;
    private string _wordProgress = "oooo";
    private List<Guess> _guessList = new List<Guess>();

    public static HistoryBuilder NewObject() =>
        new HistoryBuilder();

    public History DomainBuild() =>
        new History()
        {
            IpAddress = "random",
            NumberOfLives = _numberOfLives,
            WordProgress = _wordProgress,
            WordToGuess = "test",
            IpPort = 123,
            Guesses = _guessList
        };

    public HistoryResponse ResponseBuild() =>
        new HistoryResponse()
        {
            Guesses = new List<GuessResponse>(),
            IsFinalResult = true,
            NumberOfLives = _numberOfLives,
            WordProgress = _wordProgress
        };

    public HistoryBuilder WithNumberOfLives(int numberOfLives)
    {
        _numberOfLives = numberOfLives; 
        
        return this;
    }

    public HistoryBuilder WithGuessList(List<Guess> guessList)
    {
        _guessList = guessList;

        return this;
    }
}
