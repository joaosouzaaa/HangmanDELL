using HangmanDELL.API.Arguments;
using HangmanDELL.API.DataTransferObjects.Guess;
using HangmanDELL.API.DataTransferObjects.History;
using HangmanDELL.API.Interfaces.Services;
using HangmanDELL.API.Settings.NotificationSettings;
using Microsoft.AspNetCore.Mvc;

namespace HangmanDELL.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class HangmanController : ControllerBase
{
    private readonly IGuesserService _guesserService;

    public HangmanController(IGuesserService guesserService)
    {
        _guesserService = guesserService;
    }

    [HttpPost("guess-word")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HistoryResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<HistoryResponse?> GuessWordAsync([FromBody] GuessRequest guessRequest)
    {
        var guessWordArgument = new GuessWordArgument()
        {
            IpAddress = HttpContext.Connection.RemoteIpAddress == null ? null : HttpContext.Connection.RemoteIpAddress.ToString(),
            LetterToGuess = guessRequest.LetterToGuess,
            IpPort = HttpContext.Connection.RemotePort
        };

        return await _guesserService.GuessWordAsync(guessWordArgument);
    }
}
