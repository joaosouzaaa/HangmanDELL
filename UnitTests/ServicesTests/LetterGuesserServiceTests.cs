using HangmanDELL.API.Arguments;
using HangmanDELL.API.Services;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;
public sealed class LetterGuesserServiceTests
{
    private readonly LetterGuesserService _letterGuesserService;

    public LetterGuesserServiceTests()
    {
        _letterGuesserService = new LetterGuesserService();
    }

    [Fact]
    public void IsRightLetter_IsTheRightLetter()
    {
        // A
        var rightLetterArgument = new RightLetterArgument()
        {
            GuessProgress = "_____",
            Letter = 't',
            WordToGuess = "teste"
        };
        const string guessProgressResultExpected = "t__t_";

        // A
        var rightLetterResult = _letterGuesserService.IsRightLetter(rightLetterArgument);

        // A
        Assert.Equal(guessProgressResultExpected, rightLetterResult.GuessedWord);
        Assert.True(rightLetterResult.IsSuccess);
    }

    [Fact]
    public void IsRightLetter_IsNotTheRightLetter()
    {
        // A
        var rightLetterArgument = new RightLetterArgument()
        {
            GuessProgress = "_____",
            Letter = 'a',
            WordToGuess = "teste"
        };
        const string guessProgressResultExpected = "_____";

        // A
        var rightLetterResult = _letterGuesserService.IsRightLetter(rightLetterArgument);

        // A
        Assert.Equal(guessProgressResultExpected, rightLetterResult.GuessedWord);
        Assert.False(rightLetterResult.IsSuccess);
    }
}
