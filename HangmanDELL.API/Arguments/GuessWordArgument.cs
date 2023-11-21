using HangmanDELL.API.DataTransferObjects.Guess;

namespace HangmanDELL.API.Arguments;

public sealed class GuessWordArgument
{
    public required GuessRequest GuessRequest { get; set; }
    public string? IpAddress { get; set; }
    public int? IpPort { get; set; }
}
