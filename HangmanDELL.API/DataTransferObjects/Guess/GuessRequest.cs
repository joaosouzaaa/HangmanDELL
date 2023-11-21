namespace HangmanDELL.API.DataTransferObjects.Guess;

public sealed class GuessRequest
{
    private char _letterToGuess;
    public required char LetterToGuess
    {
        get => _letterToGuess;
        set => _letterToGuess = char.ToUpper(value);
    }
}
