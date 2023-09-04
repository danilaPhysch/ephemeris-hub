using EphemerisProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace EphemerisProvider.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<GlonassEphemeris> GlonassEphemerides { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GlonassEphemeris>()
            .HasKey(x => new { x.CsSatNumber, x.EpochTimeUtc });
    }
}