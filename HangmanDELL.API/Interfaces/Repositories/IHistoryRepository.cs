using HangmanDELL.API.Entities;
using System.Linq.Expressions;

namespace HangmanDELL.API.Interfaces.Repositories;

public interface IHistoryRepository
{
    Task AddAsync(History history);
    Task UpdateAsync(History history);
    Task DeleteAsync(History history);
    Task<History?> GetByAsync(Expression<Func<History, bool>> predicate);
}
