using HangmanDELL.API.Data.Repositories;
using HangmanDELL.API.Interfaces.Repositories;

namespace HangmanDELL.API.DependencyInjection;

public static class RepositoriesDependencyInjection
{
    public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IHistoryRepository, HistoryRepository>();
    }
}
