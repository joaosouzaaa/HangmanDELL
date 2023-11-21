using HangmanDELL.API.Interfaces.Services;
using HangmanDELL.API.Services;

namespace HangmanDELL.API.DependencyInjection;

public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IGuesserService, GuesserService>();
        services.AddScoped<ILetterGuesserService, LetterGuesserService>();
        services.AddScoped<IQueryWordsService, QueryWordsService>();
    }
}
