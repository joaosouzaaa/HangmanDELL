using Microsoft.EntityFrameworkCore;

namespace HangmanDELL.API.Data.DatabaseContexts;

public sealed class HangmanDbContext : DbContext
{
    public HangmanDbContext(DbContextOptions<HangmanDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HangmanDbContext).Assembly);
    }
}
