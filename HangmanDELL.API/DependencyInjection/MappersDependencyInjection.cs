using HangmanDELL.API.Interfaces.Mappers;
using HangmanDELL.API.Mappers;

namespace HangmanDELL.API.DependencyInjection;

public static class MappersDependencyInjection
{
    public static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IGuessMapper, GuessMapper>();
        services.AddScoped<IHistoryMapper, HistoryMapper>();
    }
}
