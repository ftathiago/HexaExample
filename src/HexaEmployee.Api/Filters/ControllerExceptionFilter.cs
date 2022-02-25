using HexaEmployee.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace HexaEmployee.Api.Filters
{
    public class ControllerExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ControllerExceptionFilter> _logger;

        public ControllerExceptionFilter(ILogger<ControllerExceptionFilter> logger) =>
            _logger = logger;

        public void OnException(ExceptionContext context)
        {
            var errorMessage = context.Exception
                .GetAllMessage(",");

            _logger.LogError(context.Exception, errorMessage);

            context.ExceptionHandled = true;

            context.Result = new ObjectResult(new { errorMessage })
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
