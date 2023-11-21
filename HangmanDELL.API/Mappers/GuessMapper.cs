using HangmanDELL.API.DataTransferObjects.Guess;
using HangmanDELL.API.Entities;
using HangmanDELL.API.Interfaces.Mappers;
namespace HangmanDELL.API.Mappers;

public sealed class GuessMapper : IGuessMapper
{
    public List<GuessResponse> DomainListToResponseList(List<Guess> guessList) =>
        guessList.Select(DomainToResponse).ToList();

    private GuessResponse DomainToResponse(Guess guess) =>
        new GuessResponse()
        {
            Creation = guess.Creation,
            GuessedLetter = guess.GuessedLetter
        };
}
