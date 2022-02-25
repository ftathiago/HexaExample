using HexaEmployee.Api.Services;
using HexaEmployee.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace HexaEmployee.Api.Filters
{
    public class MessageFilter : IActionFilter
    {
        private readonly INotification _notifications;
        private readonly ILogger<MessageFilter> _logger;

        public MessageFilter(INotification notifications, ILogger<MessageFilter> logger)
        {
            _notifications = notifications;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do nothing
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!_notifications.Any())
            {
                return;
            }

            LogNotifications();

            context.HttpContext.Response.StatusCode =
                MappingErrorToHttpStatusCode(_notifications.ErrorCode);
            context.Result = new ObjectResult(new
            {
                errorMessage = _notifications.StringifyMessages(),
                originalData = context.Result,
            });
        }

        private static int MappingErrorToHttpStatusCode(string errorCode) =>
            (int)ErrorCodeMapper.Map(errorCode);

        private void LogNotifications() =>
            _notifications.All().ToList().ForEach(message =>
                _logger.LogError("{0} - (ErrorCode: {1})", message.Content, message.Code));
    }
}
