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
    private readonly IHistoryMapper _historyMapper;
    private readonly IQueryWordsService _queryWordsService;
    private readonly ILetterGuesserService _letterGuesserService;
    private readonly INotificationHandler _notificationHandler;

    public GuesserService(IHistoryRepository historyRepository, IHistoryMapper historyMapper,
                          IQueryWordsService queryWordsService, ILetterGuesserService letterGuesserService,
                          INotificationHandler notificationHandler)
    {
        _historyRepository = historyRepository;
        _historyMapper = historyMapper;
        _queryWordsService = queryWordsService;
        _letterGuesserService = letterGuesserService;
        _notificationHandler = notificationHandler;
    }

    public async Task<HistoryResponse?> GuessWordAsync(GuessWordArgument guessWord)
    {
        if (!char.IsLetter(guessWord.LetterToGuess))
        {
            _notificationHandler.AddNotification("Invalid", "Invalid Letter.");
            return null;
        }

        var history = await _historyRepository.GetByAsync(guessWord.IpAddress, guessWord.IpPort);

        if (history is null)
            return await AddNonexistantGuessAsync(guessWord);

        if(history.Guesses.Any(g => char.ToUpper(g.GuessedLetter) == guessWord.LetterToGuess))
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

    private async Task<HistoryResponse> AddNonexistantGuessAsync(GuessWordArgument guessWord)
    {
        var wordToGuess = _queryWordsService.GetRandomWord();
        var letterToGuess = guessWord.LetterToGuess;

        var rightLetter = new RightLetterArgument()
        {
            GuessProgress = new string('_', wordToGuess.Length),
            Letter = letterToGuess,
            WordToGuess = wordToGuess
        };
        var rightLetterResult = _letterGuesserService.IsRightLetter(rightLetter);

        var guess = new Guess()
        {
            GuessedLetter = letterToGuess,
            IsSuccess = rightLetterResult.IsSuccess
        };
        var history = new History()
        {
            IpAddress = guessWord.IpAddress,
            IpPort = guessWord.IpPort,
            WordToGuess = wordToGuess,
            Guesses = new List<Guess>(),
            WordProgress = rightLetterResult.GuessedWord
        };

        history.Guesses.Add(guess);
        history.NumberOfLives = rightLetterResult.IsSuccess ? LivesConstants.DefaultNumberOfLives : LivesConstants.DefaultNumberOfLives - 1;

        await _historyRepository.AddAsync(history);

        return _historyMapper.DomainToResponse(history);
    }

    private async Task<HistoryResponse> UpdateExistantGuessAsync(History history, GuessWordArgument guessWord)
    {
        var letterToGuess = guessWord.LetterToGuess;
        var rightLetter = new RightLetterArgument()
        {
            GuessProgress = history.WordProgress!,
            Letter = letterToGuess,
            WordToGuess = history.WordToGuess!
        };
        var rightLetterResult = _letterGuesserService.IsRightLetter(rightLetter);

        var guess = new Guess()
        {
            GuessedLetter = letterToGuess,
            IsSuccess = rightLetterResult.IsSuccess,
            Creation = DateTime.Now,
            HistoryId = history.Id
        };

        history.WordProgress = rightLetterResult.GuessedWord;
        history.Guesses.Add(guess);
        history.NumberOfLives = rightLetterResult.IsSuccess ? history.NumberOfLives : history.NumberOfLives - 1;

        await _historyRepository.UpdateAsync(history);

        return _historyMapper.DomainToResponse(history);
    }
}
