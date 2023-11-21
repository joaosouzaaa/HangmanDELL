namespace HangmanDELL.API.Entities;

public sealed class Guess
{
    public int Id { get; set; }
    public required char GuessedLetter { get; set; }
    public required bool IsSuccess { get; set; }
    public DateTime Creation { get; set; }

    public int HistoryId { get; set; }
    public History History { get; set; }
}
