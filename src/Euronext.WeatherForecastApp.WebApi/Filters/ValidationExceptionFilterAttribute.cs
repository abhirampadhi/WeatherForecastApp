using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Euronext.WeatherForecastAppCommon.Models;

namespace Euronext.WeatherForecastAppWebApi.Filters;

public sealed class ValidationExceptionFilterAttribute : ActionFilterAttribute
{
    private readonly ILogger<ValidationExceptionFilterAttribute> _logger;

    public ValidationExceptionFilterAttribute(ILogger<ValidationExceptionFilterAttribute> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();

            var errorResponse = new ErrorResponse(
                (int)HttpStatusCode.BadRequest,
                "Validation failed",
                string.Join(", ", errors)
            );

            context.Result = new BadRequestObjectResult(errorResponse);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}