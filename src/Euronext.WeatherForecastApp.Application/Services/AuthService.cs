using Euronext.WeatherForecastApp.Application.Commands.Create;
using Euronext.WeatherForecastApp.Application.Interfaces;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Queries;
using Euronext.WeatherForecastApp.Common.JwtWrapper;
using Isopoh.Cryptography.Argon2;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Euronext.WeatherForecastApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;
    private readonly IJwtTokenHandler _jwtTokenHandler;

    public AuthService(IMediator mediator, IConfiguration configuration, 
        ILogger<AuthService> logger, IJwtTokenHandler jwtTokenHandler)
    {
        _mediator = mediator;
        _configuration = configuration;
        _logger = logger;
        _jwtTokenHandler = jwtTokenHandler;
    }

    public async Task<UserDto> LoginAsync(LoginUserDto loginUser)
    {
        try
        {
            var user = await _mediator.Send(new GetUserQuery(loginUser.UserName));

            if (user is null || !Argon2.Verify(user.Password, loginUser.Password))
            {
                _logger.LogWarning("Login failed for user {UserName}", loginUser.UserName);
                return null; // Return null to show that login was unsuccessful
            }

            // Create JWT token handler and get secret key
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

            // Prepare list of user claims
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.GivenName, user.Name)
                };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            // Create token and set it to user
            var token = _jwtTokenHandler.CreateToken(tokenDescriptor);
            user.Token = _jwtTokenHandler.WriteToken(token);
            user.IsActive = true;

            _logger.LogInformation("User {UserName} logged in successfully", loginUser.UserName);

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while logging in user {UserName}", loginUser.UserName);
            throw;
        }
    }

    public async Task RegisterAsync(RegisterUserDto registerUser)
    {
        try
        {
            registerUser.Password = Argon2.Hash(registerUser.Password);
            await _mediator.Send(new AddUserCommand(registerUser.UserName, registerUser.Name, registerUser.Password, registerUser.Roles));
            _logger.LogInformation("User {UserName} registered successfully", registerUser.UserName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering user {UserName}", registerUser.UserName);
            throw;
        }
    }
}