using AutoFixture;
using EphemerisProvider.Models;

namespace EphemerisProvider.Application;

public class FakeEphemerisProcessor : IEphemerisProcessor
{
    private readonly Fixture _fixture = new();

    public Task<IList<GlonassEphemerisRnx>> GetEphemeris(CancellationToken cancellationToken)
    {
        var ephemeris = new List<GlonassEphemerisRnx>();
        _fixture.Customize(new GlonassEphemerisBuilder(501));

        ephemeris.Add(_fixture.Create<GlonassEphemerisRnx>());

        _fixture.Customize(new GlonassEphemerisBuilder(502));

        ephemeris.Add(_fixture.Create<GlonassEphemerisRnx>());

        return Task.FromResult<IList<GlonassEphemerisRnx>>(ephemeris);
    }
}

public class GlonassEphemerisBuilder : ICustomization
{
    private readonly int _csSatelliteNumber;

    public GlonassEphemerisBuilder(int csSatelliteNumber)
    {
        _csSatelliteNumber = csSatelliteNumber;
    }


    public void Customize(IFixture fixture)
    {
        fixture.Customize<GlonassEphemerisRnx>(composer =>
            composer.With(p => p.CsSatNumber, _csSatelliteNumber)
                .With(p => p.EpochTimeUtc, DateTime.UtcNow));
    }
}