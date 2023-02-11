using Microsoft.Extensions.DependencyInjection;
using nFastProxy.Domain.Shared.Abstraction;
using nFastProxy.Domain.Shared.Models;

namespace nFastProxy.App;

public static class Startup
{
    private static ILogger<Program>? _logger;
    private static CancellationTokenSource? _cancellationTokenSource;

    public static void RegisterServices(IServiceCollection services, Options options)
    {
        services.AddSingleton(options);
        services
            .AddLoggingExt(options)
            .AddApplicationServices(options);
    }

    public static async Task RunAsync(IServiceProvider provider, Options options)
    {
        _logger = provider.GetService<ILogger<Program>>();
        _logger?.LogInformation("Starting nFastProxy on '{IP}' port '{Port}'...", options.IP, options.Port);
        Console.CancelKeyPress += StopApplication;

        using var proxyServer = provider.GetService<IProxyServer>();
        try
        {
            if (proxyServer is null)
            {
                _logger?.LogError("Proxy server provider is not available");
                Environment.Exit(ExitCodeConst.HttpProxyProviderNotFound);
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            await proxyServer.StartAsync(cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
            }
        }
        catch (TaskCanceledException)
        {
            //
        }
    }

    private static void StopApplication(object? sender, ConsoleCancelEventArgs e)
    {
        e.Cancel = true;
        Console.WriteLine();
        _logger?.LogInformation("Stopping nFastProxy...");
        _cancellationTokenSource?.Cancel();
        _logger?.LogInformation("nFastProxy stopped");
    }
}