using Euronext.WeatherForecastAppCommon.Exceptions;
using Euronext.WeatherForecastAppCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using UnauthorizedAccessException = Euronext.WeatherForecastAppCommon.Exceptions.UnauthorizedAccessException;

namespace Euronext.WeatherForecastAppWebApi.Filters;

/// <summary>
/// A filter that catches exceptions thrown during the execution of an action and converts them into a JSON response.
/// </summary>
public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, context.Exception.Message);

        var code = HttpStatusCode.InternalServerError;
        string details = context.Exception.StackTrace;

        if (context.Exception is NotFoundException)
        {
            code = HttpStatusCode.NotFound;
        }
        else if (context.Exception is System.ComponentModel.DataAnnotations.ValidationException)
        {
            code = HttpStatusCode.BadRequest;
        }
        else if (context.Exception is ArgumentException)
        {
            code = HttpStatusCode.BadRequest;
        }
        else if(context.Exception is UnauthorizedAccessException)
        {
            code = HttpStatusCode.Unauthorized;
          
        }

        var errorResponse = new ErrorResponse((int)code, context.Exception.Message, details);
        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = (int)code
        };
        context.ExceptionHandled = true;
    }
}