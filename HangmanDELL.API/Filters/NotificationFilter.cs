using HangmanDELL.API.Interfaces.Settings.NotificationSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HangmanDELL.API.Filters;

public sealed class NotificationFilter : ActionFilterAttribute 
{
    private readonly INotificationHandler _notificationHandler;

    public NotificationFilter(INotificationHandler notificationHandler)
    {
        _notificationHandler = notificationHandler;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (_notificationHandler.HasNotifications())
            context.Result = new BadRequestObjectResult(_notificationHandler.GetNotifications());

        base.OnActionExecuted(context);
    }
}
