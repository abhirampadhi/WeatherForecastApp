using AutoMapper;
using Euronext.WeatherForecastApp.Application.Commands.Create;
using Euronext.WeatherForecastApp.Application.Commands.Delete;
using Euronext.WeatherForecastApp.Application.Commands.Update;
using Euronext.WeatherForecastApp.Application.Models.DTOs;
using Euronext.WeatherForecastApp.Application.Models.Requests;
using Euronext.WeatherForecastApp.Application.Models.Responses;
using Euronext.WeatherForecastApp.Application.Queries;
using Euronext.WeatherForecastAppCommon.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Euronext.WeatherForecastAppWebApi.Controllers;

[ApiController]
[Route("api/weatherForecast")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMapper _mapper;
    private IMediator _mediator;

    public WeatherForecastController(IMediator mediator, IMapper mapper,
       ILogger<WeatherForecastController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region User

    [HttpGet]
    [Authorize(Policy = "User")]
    [SwaggerOperation("Get all weather forecasts")]
    [ProducesResponseType(typeof(List<WeatherForecastResponseModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("BEGIN: Get all weather forecasts");

        var forecasts = await _mediator.Send(new GetWeatherForecastsQuery());

        if (forecasts is null)
        {
            return NotFound();
        }

        _logger.LogInformation("END: Get all weather forecasts");
        return Ok(forecasts);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    [SwaggerOperation("Get a specific weather forecast")]
    [ProducesResponseType(typeof(WeatherForecastResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetById([Required][FromQuery] int id)
    {
        _logger.LogInformation("BEGIN: Get a specific weather forecast");

        var forecast = await _mediator.Send(new GetWeatherForecastByIdQuery(id));

        if (forecast is null)
        {
            return NotFound();
        }

        _logger.LogInformation("END: Get a specific weather forecast");
        return Ok(forecast);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    [SwaggerOperation("Get weather forecast by date")]
    [ProducesResponseType(typeof(WeatherForecastResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetByDate([Required][FromQuery] DateTime date)
    {
        _logger.LogInformation("BEGIN: Get a specific weather forecast");

        var forecast = await _mediator.Send(new GetWeatherForecastsByDateQuery(date));

        if (forecast is null)
        {
            return NotFound();
        }

        _logger.LogInformation("END: Get a specific weather forecast");
        return Ok(forecast);
    }

    #endregion

    #region Admin

    [HttpPost]
    [Authorize(Policy = "Admin")]
    [SwaggerOperation("Add weather forecasts")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Post([FromBody] List<WeatherForecastRequestModel> inputModel)
    {
        _logger.LogInformation("BEGIN: Add weather forecasts");

        if (inputModel is null)
        {
            _logger.LogError("Request is not valid.");
            return BadRequest(new ErrorResponse((int)HttpStatusCode.BadRequest, "Request is not valid."));
        }

        var command = new AddWeatherForecastsCommand(_mapper.Map<List<WeatherForecastDto>>(inputModel));
        await _mediator.Send(command);

        _logger.LogInformation("END: Add weather forecasts");
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    [SwaggerOperation("Update an existing weather forecast")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Put([Required][FromQuery] int id, [FromBody] WeatherForecastRequestModel inputModel)
    {
        _logger.LogInformation("BEGIN: Create a Weather Forecast");

        if (inputModel is null)
        {
            _logger.LogError("Request is not valid.");
            return BadRequest(new ErrorResponse((int)HttpStatusCode.BadRequest, "Request is not valid."));
        }

        var command = new UpdateWeatherForecastCommand(id, _mapper.Map<WeatherForecastDto>(inputModel));
        await _mediator.Send(command);

        _logger.LogInformation("END:Create a Weather Forecast");
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    [SwaggerOperation("Delete a weather forecast")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete([Required][FromQuery] int id)
    {
        await _mediator.Send(new DeleteWeatherForecastCommand(id) { });
        return NoContent();
    }

    #endregion
}