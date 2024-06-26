using FluentValidation.Results;

namespace Euronext.WeatherForecastAppCommon.Exceptions;
public sealed class ValidationException : Exception
{
    public List<string> ValidationErrors { get; private set; }

    public ValidationException(List<string> errors)
        : base("Validation failed")
    {
        ValidationErrors = errors;
    }

    public ValidationException(string error)
        : base(error)
    {
        ValidationErrors = new List<string> { error };
    }

    public ValidationException(string error, Exception innerException)
        : base(error, innerException)
    {
        ValidationErrors = new List<string> { error };
    }

    public ValidationException(List<ValidationFailure> failures)
        : base("Validation failed")
    {
        ValidationErrors = failures.Select(failure => failure.ErrorMessage).ToList();
    }
}