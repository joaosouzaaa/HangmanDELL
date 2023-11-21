namespace HangmanDELL.API.Arguments;

public sealed class GuessWordArgument
{
    private char _letterToGuess;
    public required char LetterToGuess
    {
        get => _letterToGuess;
        set => _letterToGuess = char.ToUpper(value);
    }
    public string? IpAddress { get; set; }
    public int? IpPort { get; set; }
}
