namespace HangmanDELL.API.Entities;

public sealed class History
{
    public int Id { get; set; }
    public string? IpAddress { get; set; }
    public int? IpPort { get; set; }
    public string? WordToGuess { get; set; }
    public string? WordProgress { get; set; }
    public int NumberOfLives { get; set; }

    public List<Guess> Guesses { get; set; }
}
