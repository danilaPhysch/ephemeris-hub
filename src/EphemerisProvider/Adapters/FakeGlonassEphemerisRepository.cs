using EphemerisProvider.Application;
using EphemerisProvider.Infrastructure.Database;
using EphemerisProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace EphemerisProvider.Adapters;

public class FakeGlonassEphemerisRepository : IGlonassEphemerisRepository
{
    private readonly AppDbContext _appDbContext;

    public FakeGlonassEphemerisRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
    }

    public async Task<GlonassEphemeris> GetGlonassEphemeris(long csSatelliteNumber, DateTime time, CancellationToken cancellationToken)
    {
        return await _appDbContext.GlonassEphemerides.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertGlonassEphemeris(IList<GlonassEphemeris> glonassEphemeris, CancellationToken cancellationToken)
    {
        _appDbContext.GlonassEphemerides.AddRange(glonassEphemeris);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}