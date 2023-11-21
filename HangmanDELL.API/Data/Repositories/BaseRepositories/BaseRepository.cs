using HangmanDELL.API.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HangmanDELL.API.Data.Repositories.BaseRepositories;

public abstract class BaseRepository<TEntity> : IDisposable
    where TEntity : class
{
    protected readonly HangmanDbContext _dbContext;
    protected DbSet<TEntity> _dbContextSet => _dbContext.Set<TEntity>();

    protected BaseRepository(HangmanDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    protected async Task SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync();
}
