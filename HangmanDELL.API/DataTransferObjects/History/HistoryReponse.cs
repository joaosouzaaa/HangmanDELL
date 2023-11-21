using HangmanDELL.API.DataTransferObjects.Guess;

namespace HangmanDELL.API.DataTransferObjects.History;

public sealed class HistoryReponse
{
    public string? WordProgress { get; set; }
    public required int NumberOfLives { get; set; }
    public required bool IsFinalResult { get; set; } 
    public required List<GuessResponse> Guesses { get; set; }
}
