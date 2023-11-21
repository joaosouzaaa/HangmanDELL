using HangmanDELL.API.DataTransferObjects.Guess;
using HangmanDELL.API.Entities;

namespace HangmanDELL.API.Interfaces.Mappers;

public interface IGuessMapper
{
    List<GuessResponse> DomainListToResponseList(List<Guess> guessList);
}
