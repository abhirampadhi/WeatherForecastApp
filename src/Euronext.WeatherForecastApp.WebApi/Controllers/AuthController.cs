using AutoMapper;
using Euronext.WeatherForecastApp.Application.Commands.Create;
using Euronext.WeatherForecastApp.Application.Interfaces;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Models.Requests;
using Euronext.WeatherForecastAppCommon.Models;
using Isopoh.Cryptography.Argon2;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Euronext.WeatherForecastApp.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IAuthService _authService;

    public AuthController(IMediator mediator, IMapper mapper, ILogger<AuthController> logger, IAuthService authService)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [SwaggerOperation("Login user")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Login([FromBody] LoginUserModelRequest user)
    {

        if (user is null)
        {
            _logger.LogWarning("User value can't be null");
            return BadRequest(new ErrorResponse((int)HttpStatusCode.BadRequest, "Invalid user data provided"));
        }

        _logger.LogInformation("BEGIN: User login attempt for {UserName}", user.UserName);

        var loggedInUser = await _authService.LoginAsync(new LoginUserDto() { UserName = user.UserName, Password = user.Password });

        if (loggedInUser != null)
        {
            _logger.LogInformation("END: User login successful for {UserName}", user.UserName);
            return Ok(loggedInUser);
        }

        _logger.LogWarning("User login unsuccessful for {UserName}", user.UserName);
        return BadRequest(new ErrorResponse((int)HttpStatusCode.BadRequest, "User login unsuccessful"));
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [SwaggerOperation("Register user")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
    {
        if (user is null)
        {
            _logger.LogWarning("User value can't be null");
            return BadRequest(new ErrorResponse((int)HttpStatusCode.BadRequest, "Invalid user data provided"));
        }

        _logger.LogInformation("BEGIN: User registration attempt for {UserName}", user.UserName);

        var registerCommand = new AddUserCommand(user.UserName, user.Name, Argon2.Hash(user.Password), user.Roles);
        await _mediator.Send(registerCommand);

        _logger.LogInformation("END: User registration successful for {UserName}", user.UserName);
        return Ok(new { Message = "User registered successfully" });
    }
}
