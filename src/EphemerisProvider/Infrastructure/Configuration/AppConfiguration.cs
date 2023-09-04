namespace EphemerisProvider.Infrastructure.Configuration;

public static class AppConfiguration
{
    public static string ConnectionString { get; private set; }

    public static void ConfigureConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetConnectionString("DefaultConnection");
        var ephemerisLoaderSettings = configuration.GetSection("EphemerisLoaderSettings").Get<EphemerisLoaderSettings>();

        ConnectionString = connectionStrings;

        services.AddSingleton(ephemerisLoaderSettings);
    }
}