using Bogus;
using HangmanDELL.API.Settings.NotificationSettings;

namespace UnitTests.SettingsTests.NotificationSettingsTests;

public sealed class NotificationHandlerTests
{
    private readonly NotificationHandler _notificationHandler;
    private readonly Randomizer _random;

    public NotificationHandlerTests()
    {
        _notificationHandler = new NotificationHandler();
        _random = new Faker().Random;
    }

    [Fact]
    public void GetNotifications_AddNotifications_ListHasNotifications()
    {
        // A
        const int notificationCount = 2;
        AddNotificationsInRange(notificationCount);

        // A
        var notificationListResult = _notificationHandler.GetNotifications();

        // A
        Assert.Equal(notificationCount, notificationListResult.Count);
    }

    [Fact]
    public void HasNotification_AddNotification_HasNotificationTrue()
    {
        // A
        var key = _random.Word();
        var message = _random.Word();
        _notificationHandler.AddNotification(key, message);

        // A
        var hasNotificationResult = _notificationHandler.HasNotifications();

        // A
        Assert.True(hasNotificationResult);
    }

    [Fact]
    public void HasNotification_HasNotificationFalse()
    {
        var hasNotificationResult = _notificationHandler.HasNotifications();

        Assert.False(hasNotificationResult);
    }

    private void AddNotificationsInRange(int range)
    {
        for (var i = 0; i < range; i++)
        {
            _notificationHandler.AddNotification(_random.Word(), _random.Word());
        }
    }
}
