using HangmanDELL.API.Arguments;
using HangmanDELL.API.Controllers;
using HangmanDELL.API.DataTransferObjects.Guess;
using HangmanDELL.API.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using UnitTests.TestBuilders;

namespace UnitTests.ControllersTests;
public sealed class HangmanControllerTests
{
    private readonly Mock<IGuesserService> _guesserServiceMock;
    private readonly HangmanController _hangmanController;

    public HangmanControllerTests()
    {
        _guesserServiceMock = new Mock<IGuesserService>();
        _hangmanController = new HangmanController(_guesserServiceMock.Object); 
    }

    [Fact]
    public async Task GuessWordAsync_IpAddressIsNotNull_SuccessfulScenario_ReturnsHistoryResponse()
    {
        // A
        var guessRequest = new GuessRequest()
        {
            LetterToGuess = 'j'
        };

        const string ipAddress = "192.168.1.1";
        const int remotePort = 12;
        var httpContext = new DefaultHttpContext
        {
            Connection = { RemoteIpAddress = IPAddress.Parse(ipAddress), RemotePort = remotePort }
        };
        _hangmanController.ControllerContext = new ControllerContext { HttpContext = httpContext };

        var historyResponse = HistoryBuilder.NewObject().ResponseBuild();
        _guesserServiceMock.Setup(g => g.GuessWordAsync(It.IsAny<GuessWordArgument>()))
            .ReturnsAsync(historyResponse);

        // A
        var historyResponseResult = await _hangmanController.GuessWordAsync(guessRequest);

        // A
        _guesserServiceMock.Verify(g => g.GuessWordAsync(It.Is<GuessWordArgument>(g => g.IpAddress == ipAddress && g.IpPort == remotePort)), Times.Once());

        Assert.NotNull(historyResponseResult);
    }

    [Fact]
    public async Task GuessWordAsync_IpAddressIsNull_SuccessfulScenario_ReturnsHistoryResponse()
    {
        // A
        var guessRequest = new GuessRequest()
        {
            LetterToGuess = 'j'
        };

        var httpContext = new DefaultHttpContext
        {
            Connection = { RemoteIpAddress = null }
        };
        _hangmanController.ControllerContext = new ControllerContext { HttpContext = httpContext };

        var historyResponse = HistoryBuilder.NewObject().ResponseBuild();
        _guesserServiceMock.Setup(g => g.GuessWordAsync(It.IsAny<GuessWordArgument>()))
            .ReturnsAsync(historyResponse);

        // A
        var historyResponseResult = await _hangmanController.GuessWordAsync(guessRequest);

        // A
        _guesserServiceMock.Verify(g => g.GuessWordAsync(It.Is<GuessWordArgument>(g => g.IpAddress == null)), Times.Once());

        Assert.NotNull(historyResponseResult);
    }
}
