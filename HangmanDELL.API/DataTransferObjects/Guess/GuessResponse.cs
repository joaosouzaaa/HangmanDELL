namespace HangmanDELL.API.DataTransferObjects.Guess;

public sealed class GuessResponse
{
    public required char GuessedLetter { get; set; }
    public required DateTime Creation { get; set; }
}
