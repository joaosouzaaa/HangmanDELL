using HangmanDELL.API.Settings.NotificationSettings;

namespace HangmanDELL.API.Interfaces.Settings.NotificationSettings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}
