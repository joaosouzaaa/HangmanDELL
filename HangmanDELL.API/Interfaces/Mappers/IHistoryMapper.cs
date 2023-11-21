using HangmanDELL.API.DataTransferObjects.History;
using HangmanDELL.API.Entities;

namespace HangmanDELL.API.Interfaces.Mappers;

public interface IHistoryMapper
{
    HistoryResponse DomainToResponse(History history);
}
