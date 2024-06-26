using AutoMapper;
using Euronext.WeatherForecastApp.Application.Commands.Create;
using Euronext.WeatherForecastApp.Domain.Entities;
using Euronext.WeatherForecastApp.Domain.Interface;
using MediatR;

namespace Euronext.WeatherForecastApp.Application.Handlers;

public sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand>
{
    private readonly IUserRepository _repository;

    public AddUserCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
    }

    public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            IsActive = true,    
            Name = request.Name,  
            Password = request.Password,
            Roles = request.Roles, 
            UserName = request.UserName        
        };

        await _repository.AddUserAsync(user);
    }
}
