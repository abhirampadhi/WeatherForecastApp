using MediatR;

namespace Euronext.WeatherForecastApp.Application.Commands.Create;

public sealed class AddUserCommand : IRequest
{
    public string UserName { get; }
    public string Name { get; }
    public string Password { get; }
    public List<string>? Roles { get; }


    public AddUserCommand(string userName, string name, string password, List<string>? roles)
    {
        UserName = userName;
        Name = name;
        Password = password;    
        Roles = roles;
    }
}
