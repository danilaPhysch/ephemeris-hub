using EphemerisProvider.Api.Parameters;
using EphemerisProvider.Application;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace EphemerisProvider.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EphemerisController : ControllerBase
{
    private readonly IGlonassEphemerisRepository _glonassEphemerisRepository;
    private readonly IValidator<GetGlonassEphemerisParameters> _validator;
    private readonly ILogger<EphemerisController> _logger;

    public EphemerisController(IGlonassEphemerisRepository glonassEphemerisRepository, IValidator<GetGlonassEphemerisParameters> validator, ILogger<EphemerisController> logger)
    {
        _glonassEphemerisRepository = glonassEphemerisRepository ?? throw new ArgumentNullException(nameof(glonassEphemerisRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    public async Task<IActionResult> GetGlonassEphemeris([FromBody] GetGlonassEphemerisParameters parameters)
    {
        _logger.LogInformation("Method {MethodName} was called with parameters: {Parameters}", nameof(GetGlonassEphemeris), parameters);

        var validationResult = await _validator.ValidateAsync(parameters, HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            _logger.LogError("{ValidationResult}", validationResult);

            return BadRequest(validationResult);
        }

        var ephemeris = await _glonassEphemerisRepository.GetGlonassEphemeris(parameters.CsSatelliteNumber, parameters.Time, HttpContext.RequestAborted);

        _logger.LogInformation("Method {MethodName} returns: {Ephemeris}", nameof(GetGlonassEphemeris), ephemeris);

        return Ok(ephemeris);
    }
}