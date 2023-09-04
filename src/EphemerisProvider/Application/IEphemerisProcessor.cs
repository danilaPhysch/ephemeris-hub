using EphemerisProvider.Models;

namespace EphemerisProvider.Application;

public interface IEphemerisProcessor
{
    Task<IList<GlonassEphemerisRnx>> GetEphemeris(CancellationToken cancellationToken);
}