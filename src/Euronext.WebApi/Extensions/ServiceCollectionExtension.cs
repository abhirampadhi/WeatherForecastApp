using Euronext.WeatherForecastApp.Application.Validators;
using Euronext.WeatherForecastAppWebApi.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace Euronext.WeatherForecastAppWebApi.Extensions;
public static class ServiceCollectionExtension
{
    internal static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => policy.RequireRole("User"));
        });

        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationExceptionFilterAttribute>();
            options.Filters.Add<CustomExceptionFilter>();
        });

        //services.AddMvc(options =>
        //{
        //    var policy = new AuthorizationPolicyBuilder()
        //        .RequireAuthenticatedUser()
        //        .Build();
        //    options.Filters.Add(new AuthorizeFilter(policy));
        //});

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddProblemDetails();

        services.AddValidatorsFromAssemblyContaining<WeatherForecastRequestModelValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginUserModelRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterUserModelRequestValidator>();

        return services;
    }
}
