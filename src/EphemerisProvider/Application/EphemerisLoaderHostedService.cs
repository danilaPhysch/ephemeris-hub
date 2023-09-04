using EphemerisProvider.Infrastructure.Configuration;
using EphemerisProvider.Models;
using Mapster;

namespace EphemerisProvider.Application;

public class EphemerisLoaderHostedService : BackgroundService
{
    private readonly EphemerisLoaderSettings _ephemerisLoaderSettings;
    private readonly IServiceScopeFactory _serviceProvider;
    private readonly ILogger<EphemerisLoaderHostedService> _logger;

    public EphemerisLoaderHostedService(
        EphemerisLoaderSettings ephemerisLoaderSettings,
        IServiceScopeFactory serviceProvider,
        ILogger<EphemerisLoaderHostedService> logger)
    {
        _ephemerisLoaderSettings = ephemerisLoaderSettings ?? throw new ArgumentNullException(nameof(ephemerisLoaderSettings));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Started loading ephemeris");

            try
            {
                using var scope = _serviceProvider.CreateScope();

                var ephemerisProcessor = scope.ServiceProvider.GetService<IEphemerisProcessor>();
                var glonassEphemerisRepository = scope.ServiceProvider.GetService<IGlonassEphemerisRepository>();

                var glonassEphemerisRnx = await ephemerisProcessor.GetEphemeris(stoppingToken);

                _logger.LogInformation("Loaded {EphemerisCount} glonass ephemeris", glonassEphemerisRnx.Count);

                var glonassEphemeris = glonassEphemerisRnx.Adapt<IList<GlonassEphemeris>>();

                await glonassEphemerisRepository.InsertGlonassEphemeris(glonassEphemeris, stoppingToken);

                _logger.LogInformation("Glonass ephemeris saved in Db");
            }
            catch (Exception e) when (e is OperationCanceledException || e.InnerException is OperationCanceledException)
            {
                _logger.LogInformation("{MethodName} executing has been cancelled.", nameof(EphemerisLoaderHostedService));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{MethodName} unexpected error.", nameof(EphemerisLoaderHostedService));
            }

            try
            {
                await Task.Delay(_ephemerisLoaderSettings.LoadingInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("{MethodName} executing has been canceled during delay.", nameof(EphemerisLoaderHostedService));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{MethodName} unexpected error.", nameof(EphemerisLoaderHostedService));
            }

            await Task.Delay(_ephemerisLoaderSettings.LoadingInterval, stoppingToken);
        }
    }
}