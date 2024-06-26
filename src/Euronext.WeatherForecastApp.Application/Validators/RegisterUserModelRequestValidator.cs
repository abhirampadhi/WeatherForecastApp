using Euronext.WeatherForecastApp.Application.Models.Requests;
using FluentValidation;

namespace Euronext.WeatherForecastApp.Application.Validators;

public sealed class RegisterUserModelRequestValidator : AbstractValidator<RegisterUserModelRequest>
{
    private static readonly List<string> AllowedRoles = new List<string> { "User", "Admin" };
    public RegisterUserModelRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must be entered.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name must be entered.")
            .MinimumLength(3).WithMessage("User name must be at least 3 characters long.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must be entered.")
            .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");

        RuleFor(x => x.Roles)
            .Must(roles => roles == null || roles.Count > 0).WithMessage("Roles cannot be empty if provided.");

        RuleFor(x => x.Roles)
            .Must(roles => roles == null || roles.All(role => AllowedRoles.Contains(role)))
            .WithMessage("Only 'User' and 'Admin' roles are allowed.");
    }
}
