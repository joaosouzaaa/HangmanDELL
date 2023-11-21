using HangmanDELL.API.Entities;

namespace HangmanDELL.API.Interfaces.Repositories;

public interface IHistoryRepository
{
    Task AddAsync(History history);
    Task UpdateAsync(History history);
    Task DeleteAsync(History history);
    Task<History?> GetByAsync(string? ipAddress, int? ipPort);
}
