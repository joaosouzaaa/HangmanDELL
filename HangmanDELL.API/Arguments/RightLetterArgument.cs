namespace HangmanDELL.API.Arguments;

public sealed class RightLetterArgument
{
    public required string WordToGuess { get; set; }
    public required string GuessProgress { get; set; }
    public required char Letter { get; set; }
}
