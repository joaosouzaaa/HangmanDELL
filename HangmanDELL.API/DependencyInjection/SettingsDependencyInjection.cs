using HangmanDELL.API.Interfaces.Settings.NotificationSettings;
using HangmanDELL.API.Settings.NotificationSettings;

namespace HangmanDELL.API.DependencyInjection;

public static class SettingsDependencyInjection
{
    public static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
