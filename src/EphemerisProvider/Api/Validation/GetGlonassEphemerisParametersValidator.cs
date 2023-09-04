using EphemerisProvider.Api.Parameters;
using FluentValidation;

namespace EphemerisProvider.Api.Validation;

public class GetGlonassEphemerisParametersValidator : AbstractValidator<GetGlonassEphemerisParameters>
{
    public GetGlonassEphemerisParametersValidator()
    {
        RuleFor(x => x.CsSatelliteNumber)
            .NotEmpty()
            .InclusiveBetween(500, 599)
            .WithMessage("{PropertyName} is required.");
    }
}