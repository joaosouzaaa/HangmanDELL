using HangmanDELL.API.Arguments;
using HangmanDELL.API.Constants;
using HangmanDELL.API.Entities;
using HangmanDELL.API.Interfaces.Mappers;
using HangmanDELL.API.Interfaces.Repositories;
using HangmanDELL.API.Interfaces.Services;
using HangmanDELL.API.Interfaces.Settings.NotificationSettings;
using HangmanDELL.API.Services;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;
public sealed class GuesserServiceTests
{
    private readonly Mock<IHistoryRepository> _historyRepositoryMock;
    private readonly Mock<IHistoryMapper> _historyMapperMock;
    private readonly Mock<IQueryWordsService> _queryWordsServiceMock;
    private readonly Mock<ILetterGuesserService> _letterGuesserServiceMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly GuesserService _guesserService;

    public GuesserServiceTests()
    {
        _historyRepositoryMock = new Mock<IHistoryRepository>();
        _historyMapperMock = new Mock<IHistoryMapper>();
        _queryWordsServiceMock = new Mock<IQueryWordsService>();
        _letterGuesserServiceMock = new Mock<ILetterGuesserService>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _guesserService = new GuesserService(_historyRepositoryMock.Object, _historyMapperMock.Object,
            _queryWordsServiceMock.Object, _letterGuesserServiceMock.Object, _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task GuessWordAsync_NonexistantGuess_CorrectGuess_SuccessfulScenario_ReturnsHistoryResponse()
    {
        // A
        var guessWordArgument = GuessBuilder.NewObject().WithLetterToGuess('s').WordArgumentBuild();

        _historyRepositoryMock.Setup(h => h.GetByAsync(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(Task.FromResult<History?>(null));

        _queryWordsServiceMock.Setup(q => q.GetRandomWord())
            .Returns("test");

        var rightLetterResult = RightLetterBuilder.NewObject().WithIsSucess(true).ResultBuild();
        _letterGuesserServiceMock.Setup(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()))
            .Returns(rightLetterResult);

        _historyRepositoryMock.Setup(h => h.AddAsync(It.Is<History>(h => h.NumberOfLives == LivesConstants.DefaultNumberOfLives)));

        var historyResponse = HistoryBuilder.NewObject().ResponseBuild();
        _historyMapperMock.Setup(h => h.DomainToResponse(It.IsAny<History>()))
            .Returns(historyResponse);

        // A
        var guessWordResult = await _guesserService.GuessWordAsync(guessWordArgument);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _queryWordsServiceMock.Verify(q => q.GetRandomWord(), Times.Once());
        _letterGuesserServiceMock.Verify(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.AddAsync(It.Is<History>(h => h.NumberOfLives == LivesConstants.DefaultNumberOfLives)), Times.Once());
        _historyMapperMock.Verify(h => h.DomainToResponse(It.IsAny<History>()), Times.Once());

        Assert.NotNull(guessWordResult);
    }

    [Fact]
    public async Task GuessWordAsync_NonexistantGuess_IncorrectGuess_SuccessfulScenario_ReturnsHistoryResponse()
    {
        // A
        var guessWordArgument = GuessBuilder.NewObject().WithLetterToGuess('s').WordArgumentBuild();

        _historyRepositoryMock.Setup(h => h.GetByAsync(It.IsAny<string>(), It.IsAny<int>()))
            .Returns(Task.FromResult<History?>(null));

        _queryWordsServiceMock.Setup(q => q.GetRandomWord())
            .Returns("test");

        var rightLetterResult = RightLetterBuilder.NewObject().WithIsSucess(false).ResultBuild();
        _letterGuesserServiceMock.Setup(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()))
            .Returns(rightLetterResult);

        _historyRepositoryMock.Setup(h => h.AddAsync(It.Is<History>(h => h.NumberOfLives == LivesConstants.DefaultNumberOfLives - 1)));

        var historyResponse = HistoryBuilder.NewObject().ResponseBuild();
        _historyMapperMock.Setup(h => h.DomainToResponse(It.IsAny<History>()))
            .Returns(historyResponse);

        // A
        var guessWordResult = await _guesserService.GuessWordAsync(guessWordArgument);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _queryWordsServiceMock.Verify(q => q.GetRandomWord(), Times.Once());
        _letterGuesserServiceMock.Verify(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.AddAsync(It.Is<History>(h => h.NumberOfLives == LivesConstants.DefaultNumberOfLives - 1)), Times.Once());
        _historyMapperMock.Verify(h => h.DomainToResponse(It.IsAny<History>()), Times.Once());

        Assert.NotNull(guessWordResult);
    }

    [Fact]
    public async Task GuessWordAsync_ExistantGuess_CorrectGuess_DoesNotDeleteHistory_SuccessfulScenario_ReturnsHistoryResponse()
    {
        // A
        var guessWordArgument = GuessBuilder.NewObject().WithLetterToGuess('s').WordArgumentBuild();

        const int numberOfLives = 2;
        var history = HistoryBuilder.NewObject().WithNumberOfLives(numberOfLives).DomainBuild();
        _historyRepositoryMock.Setup(h => h.GetByAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(history);

        var rightLetterResult = RightLetterBuilder.NewObject().WithIsSucess(true).WithGuessedWord("___a").ResultBuild();
        _letterGuesserServiceMock.Setup(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()))
            .Returns(rightLetterResult);

        _historyRepositoryMock.Setup(h => h.UpdateAsync(It.Is<History>(h => h.NumberOfLives == numberOfLives)));

        var historyResponse = HistoryBuilder.NewObject().ResponseBuild();
        _historyMapperMock.Setup(h => h.DomainToResponse(It.IsAny<History>()))
            .Returns(historyResponse);

        // A
        var guessWordResult = await _guesserService.GuessWordAsync(guessWordArgument);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _letterGuesserServiceMock.Verify(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.UpdateAsync(It.Is<History>(h => h.NumberOfLives == numberOfLives)), Times.Once());
        _historyMapperMock.Verify(h => h.DomainToResponse(It.IsAny<History>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.DeleteAsync(It.IsAny<History>()), Times.Never());

        Assert.NotNull(guessWordResult);
    }

    [Fact]
    public async Task GuessWordAsync_ExistantGuess_IncorrectGuess_DoesNotDeleteHistory_SuccessfulScenario_ReturnsHistoryResponse()
    {
        // A
        var guessWordArgument = GuessBuilder.NewObject().WithLetterToGuess('s').WordArgumentBuild();

        const int numberOfLives = 2;
        var history = HistoryBuilder.NewObject().WithNumberOfLives(numberOfLives).DomainBuild();
        _historyRepositoryMock.Setup(h => h.GetByAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(history);

        var rightLetterResult = RightLetterBuilder.NewObject().WithIsSucess(false).WithGuessedWord("___a").ResultBuild();
        _letterGuesserServiceMock.Setup(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()))
            .Returns(rightLetterResult);

        _historyRepositoryMock.Setup(h => h.UpdateAsync(It.Is<History>(h => h.NumberOfLives == numberOfLives - 1)));

        var historyResponse = HistoryBuilder.NewObject().ResponseBuild();
        _historyMapperMock.Setup(h => h.DomainToResponse(It.IsAny<History>()))
            .Returns(historyResponse);

        // A
        var guessWordResult = await _guesserService.GuessWordAsync(guessWordArgument);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _letterGuesserServiceMock.Verify(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.UpdateAsync(It.Is<History>(h => h.NumberOfLives == numberOfLives - 1)), Times.Once());
        _historyMapperMock.Verify(h => h.DomainToResponse(It.IsAny<History>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.DeleteAsync(It.IsAny<History>()), Times.Never());

        Assert.NotNull(guessWordResult);
    }

    [Fact]
    public async Task GuessWordAsync_ExistantGuess_NumberOfLivesEqualsMininumNumberOfLives_DeleteHistory_SuccessfulScenario_ReturnsHistoryResponse()
    {
        // A
        var guessWordArgument = GuessBuilder.NewObject().WithLetterToGuess('s').WordArgumentBuild();

        const int numberOfLives = 1;
        var history = HistoryBuilder.NewObject().WithNumberOfLives(numberOfLives).DomainBuild();
        _historyRepositoryMock.Setup(h => h.GetByAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(history);

        var rightLetterResult = RightLetterBuilder.NewObject().WithIsSucess(false).WithGuessedWord("___a").ResultBuild();
        _letterGuesserServiceMock.Setup(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()))
            .Returns(rightLetterResult);

        _historyRepositoryMock.Setup(h => h.UpdateAsync(It.Is<History>(h => h.NumberOfLives == numberOfLives - 1)));

        var historyResponse = HistoryBuilder.NewObject().WithNumberOfLives(numberOfLives - 1).ResponseBuild();
        _historyMapperMock.Setup(h => h.DomainToResponse(It.IsAny<History>()))
            .Returns(historyResponse);

        // A
        var guessWordResult = await _guesserService.GuessWordAsync(guessWordArgument);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _letterGuesserServiceMock.Verify(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.UpdateAsync(It.Is<History>(h => h.NumberOfLives == numberOfLives - 1)), Times.Once());
        _historyMapperMock.Verify(h => h.DomainToResponse(It.IsAny<History>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.DeleteAsync(It.IsAny<History>()), Times.Once());

        Assert.NotNull(guessWordResult);
    }

    [Fact]
    public async Task GuessWordAsync_ExistantGuess_WordDoesNotHaveAnyUnderlines_DeleteHistory_SuccessfulScenario_ReturnsHistoryResponse()
    {
        // A
        var guessWordArgument = GuessBuilder.NewObject().WithLetterToGuess('s').WordArgumentBuild();

        var history = HistoryBuilder.NewObject().DomainBuild();
        _historyRepositoryMock.Setup(h => h.GetByAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(history);

        var rightLetterResult = RightLetterBuilder.NewObject().WithIsSucess(true).WithGuessedWord("teste").ResultBuild();
        _letterGuesserServiceMock.Setup(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()))
            .Returns(rightLetterResult);

        _historyRepositoryMock.Setup(h => h.UpdateAsync(It.IsAny<History>()));

        var historyResponse = HistoryBuilder.NewObject().ResponseBuild();
        _historyMapperMock.Setup(h => h.DomainToResponse(It.IsAny<History>()))
            .Returns(historyResponse);

        // A
        var guessWordResult = await _guesserService.GuessWordAsync(guessWordArgument);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _letterGuesserServiceMock.Verify(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.UpdateAsync(It.IsAny<History>()), Times.Once());
        _historyMapperMock.Verify(h => h.DomainToResponse(It.IsAny<History>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.DeleteAsync(It.IsAny<History>()), Times.Once());

        Assert.NotNull(guessWordResult);
    }

    [Fact]
    public async Task GuessWordAsync_LetterToGuessIsNotALetter_ReturnsNull()
    {
        // A
        var guessWordArgument = GuessBuilder.NewObject().WithLetterToGuess('1').WordArgumentBuild();

        // A
        var guessWordResult = await _guesserService.GuessWordAsync(guessWordArgument);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _historyRepositoryMock.Verify(h => h.GetByAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Never());

        Assert.Null(guessWordResult);
    }

    [Fact]
    public async Task GuessWordAsync_ExistantGuess_LetterToGuessIsRepeated_ReturnsNull()
    {
        // A
        char repeatedLetter = 's';
        var guessWordArgument = GuessBuilder.NewObject().WithLetterToGuess(repeatedLetter).WordArgumentBuild();

        var guessListWithRepeatedLetter = new List<Guess>()
        {
            new Guess()
            {
                GuessedLetter = repeatedLetter,
                IsSuccess = false
            },
            new Guess()
            {
                GuessedLetter = 't',
                IsSuccess = false
            },
        };
        var history = HistoryBuilder.NewObject().WithGuessList(guessListWithRepeatedLetter).DomainBuild();
        _historyRepositoryMock.Setup(h => h.GetByAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(history);

        // A
        var guessWordResult = await _guesserService.GuessWordAsync(guessWordArgument);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _letterGuesserServiceMock.Verify(l => l.IsRightLetter(It.IsAny<RightLetterArgument>()), Times.Never());
        _historyRepositoryMock.Verify(h => h.UpdateAsync(It.IsAny<History>()), Times.Never());
        _historyMapperMock.Verify(h => h.DomainToResponse(It.IsAny<History>()), Times.Never());
        _historyRepositoryMock.Verify(h => h.DeleteAsync(It.IsAny<History>()), Times.Never());

        Assert.Null(guessWordResult);
    }
}
