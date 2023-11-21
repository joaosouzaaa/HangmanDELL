namespace HangmanDELL.API.Arguments;

public sealed class RightLetterResultArgument
{
    public string GuessedWord { get; set; }
    public required bool IsSuccess { get; set; }
}
