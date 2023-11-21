namespace HangmanDELL.API.DataTransferObjects.Guess;

public sealed class GuessRequest
{
    public required char LetterToGuess { get; set; }
}
