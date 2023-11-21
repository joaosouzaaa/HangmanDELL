using HangmanDELL.API.DataTransferObjects.Guess;
using HangmanDELL.API.Entities;
using HangmanDELL.API.Interfaces.Mappers;
using HangmanDELL.API.Mappers;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public sealed class HistoryMapperTests
{
    private readonly Mock<IGuessMapper> _guessMapperMock;
    private readonly HistoryMapper _historyMapper;

    public HistoryMapperTests()
    {
        _guessMapperMock = new Mock<IGuessMapper>();
        _historyMapper = new HistoryMapper(_guessMapperMock.Object);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario()
    {
        // A
        var history = HistoryBuilder.NewObject().DomainBuild();

        var guessResponseList = new List<GuessResponse>()
        {
            GuessBuilder.NewObject().ResponseBuild(),
            GuessBuilder.NewObject().ResponseBuild()
        };
        _guessMapperMock.Setup(g => g.DomainListToResponseList(It.IsAny<List<Guess>>()))
            .Returns(guessResponseList);

        // A
        var historyResponseResult = _historyMapper.DomainToResponse(history);

        // A
        Assert.Equal(historyResponseResult.Guesses.Count, guessResponseList.Count);
        Assert.Equal(historyResponseResult.NumberOfLives, history.NumberOfLives);
        Assert.Equal(historyResponseResult.WordProgress, history.WordProgress);
        Assert.False(historyResponseResult.IsFinalResult);
    }
}
