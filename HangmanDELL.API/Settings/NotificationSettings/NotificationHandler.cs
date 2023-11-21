using HangmanDELL.API.Interfaces.Settings.NotificationSettings;

namespace HangmanDELL.API.Settings.NotificationSettings;

public sealed class NotificationHandler : INotificationHandler
{
    private readonly List<Notification> _notificationList;

    public NotificationHandler()
    {
        _notificationList = new List<Notification>();
    }

    public List<Notification> GetNotifications() =>
        _notificationList;

    public bool HasNotifications() =>
        _notificationList.Any();

    public void AddNotification(string key, string message)
    {
        var notification = new Notification()
        {
            Key = key,
            Message = message
        };

        _notificationList.Add(notification);
    }
}
