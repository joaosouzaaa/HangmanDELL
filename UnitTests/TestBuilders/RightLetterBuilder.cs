using HangmanDELL.API.Arguments;

namespace UnitTests.TestBuilders;
public sealed class RightLetterBuilder
{
    private bool _isSuccess = true;
    private string _guessedWord = "test";

    public static RightLetterBuilder NewObject() =>
        new RightLetterBuilder();

    public RightLetterResultArgument ResultBuild() =>
        new RightLetterResultArgument() 
        { 
            IsSuccess = _isSuccess,
            GuessedWord = _guessedWord
        };

    public RightLetterBuilder WithIsSucess(bool isSuccess)
    {
        _isSuccess = isSuccess;

        return this;
    }

    public RightLetterBuilder WithGuessedWord(string guessedWord)
    {
        _guessedWord = guessedWord;

        return this;
    }
}
