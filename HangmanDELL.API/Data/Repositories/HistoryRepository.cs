using HangmanDELL.API.Data.DatabaseContexts;
using HangmanDELL.API.Data.Repositories.BaseRepositories;
using HangmanDELL.API.Entities;
using HangmanDELL.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HangmanDELL.API.Data.Repositories;

public sealed class HistoryRepository : BaseRepository<History>, IHistoryRepository
{
    public HistoryRepository(HangmanDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddAsync(History history)
    {
        await _dbContextSet.AddAsync(history);

        await SaveChangesAsync();
    }

    public async Task UpdateAsync(History history)
    {
        _dbContext.Entry(history).State = EntityState.Modified;

        await SaveChangesAsync();
    }

    public async Task DeleteAsync(History history)
    {
        _dbContextSet.Remove(history);

        await SaveChangesAsync();
    }

    public async Task<History?> GetByAsync(string? ipAddress, int? ipPort)  =>
        await _dbContextSet.Include(h => h.Guesses).FirstOrDefaultAsync(u => u.IpAddress == ipAddress && u.IpPort == ipPort);
}
