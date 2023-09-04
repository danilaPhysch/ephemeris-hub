using EphemerisProvider.Models;

namespace EphemerisProvider.Application;

public interface IGlonassEphemerisRepository
{
    Task<GlonassEphemeris> GetGlonassEphemeris(long csSatelliteNumber, DateTime time, CancellationToken cancellationToken);
    Task InsertGlonassEphemeris(IList<GlonassEphemeris> glonassEphemeris, CancellationToken cancellationToken);
}