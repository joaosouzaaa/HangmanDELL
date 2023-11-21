using HangmanDELL.API.Arguments;
using HangmanDELL.API.Constants;
using HangmanDELL.API.DataTransferObjects.History;
using HangmanDELL.API.Entities;
using HangmanDELL.API.Interfaces.Mappers;
using HangmanDELL.API.Interfaces.Repositories;
using HangmanDELL.API.Interfaces.Services;
using HangmanDELL.API.Interfaces.Settings.NotificationSettings;

namespace HangmanDELL.API.Services;

public sealed class GuesserService : IGuesserService
{
    private readonly IHistoryRepository _historyRepository;
    private readonly IQueryWordsService _queryWordsService;
    private readonly IHistoryMapper _historyMapper;
    private readonly INotificationHandler _notificationHandler;

    public GuesserService(IHistoryRepository historyRepository, IQueryWordsService queryWordsService,
                          IHistoryMapper historyMapper, INotificationHandler notificationHandler)
    {
        _historyRepository = historyRepository;
        _queryWordsService = queryWordsService;
        _historyMapper = historyMapper;
        _notificationHandler = notificationHandler;
    }

    public async Task<HistoryReponse?> GuessWordAsync(GuessWordArgument guessWord)
    {
        if (!char.IsLetter(guessWord.GuessRequest.LetterToGuess))
        {
            _notificationHandler.AddNotification("Invalid", "Invalid Letter.");
            return null;
        }

        var history = await _historyRepository.GetByAsync(u => u.IpAddress == guessWord.IpAddress && u.IpPort == guessWord.IpPort);

        if (history is null)
            return await AddNonexistantGuessAsync(guessWord);

        if(history.Guesses.Any(g => char.ToUpper(g.GuessedLetter) == guessWord.GuessRequest.LetterToGuess))
        {
            _notificationHandler.AddNotification("Repeated letter", "You cannot play the same letter twice.");
            return null;
        }

        var historyResponse = await UpdateExistantGuessAsync(history, guessWord);

        if (historyResponse.NumberOfLives == LivesConstants.MinimumNumberOfLives || !history.WordProgress!.Contains('_'))
        {
            await _historyRepository.DeleteAsync(history);
            historyResponse.IsFinalResult = true;
        }
        
        return historyResponse; 
    }

    private async Task<HistoryReponse> AddNonexistantGuessAsync(GuessWordArgument guessWord)
    {
        var wordToGuess = _queryWordsService.GetRandomWord();
        var letterToGuess = guessWord.GuessRequest.LetterToGuess;
        var (wordProgress, isSuccess) = GuessWord(wordToGuess, new string('_', wordToGuess.Length), letterToGuess);

        var guess = new Guess()
        {
            GuessedLetter = letterToGuess,
            IsSuccess = isSuccess
        };
        var history = new History()
        {
            IpAddress = guessWord.IpAddress,
            IpPort = guessWord.IpPort,
            WordToGuess = wordToGuess,
            Guesses = new List<Guess>(),
            WordProgress = wordProgress
        };

        history.Guesses.Add(guess);
        history.NumberOfLives = isSuccess ? LivesConstants.DefaultNumberOfLives : LivesConstants.DefaultNumberOfLives - 1;

        await _historyRepository.AddAsync(history);

        return _historyMapper.DomainToResponse(history);
    }

    private async Task<HistoryReponse> UpdateExistantGuessAsync(History history, GuessWordArgument guessWord)
    {
        var letterToGuess = guessWord.GuessRequest.LetterToGuess;
        var (wordProgress, isSuccess) = GuessWord(history.WordToGuess!, history.WordProgress!, letterToGuess);

        var guess = new Guess()
        {
            GuessedLetter = letterToGuess,
            IsSuccess = isSuccess,
            Creation = DateTime.Now,
            HistoryId = history.Id
        };

        history.WordProgress = wordProgress;
        history.Guesses.Add(guess);
        history.NumberOfLives = isSuccess ? history.NumberOfLives : history.NumberOfLives - 1;

        await _historyRepository.UpdateAsync(history);

        return _historyMapper.DomainToResponse(history);
    }

    private (string guessedWord, bool isSuccess) GuessWord(string wordToGuess, string guessProgress, char letter)
    {
        var isSuccess = false;
        char[] guessedWord = guessProgress.ToCharArray();

        for (int i = 0; i < wordToGuess.Length; i++)
        {
            if (wordToGuess[i] == letter)
            {
                guessedWord[i] = letter;
                isSuccess = true;
            }
        }

        return (new string(guessedWord), isSuccess);
    }
}
