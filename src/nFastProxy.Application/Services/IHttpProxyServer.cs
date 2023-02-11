using Microsoft.Extensions.Logging;
using nFastProxy.Domain.Shared.Abstraction;

namespace nFastProxy.Application.Services;

public class HttpProxyServer : IProxyServer
{
    private readonly ILogger<HttpProxyServer> _logger;

    public HttpProxyServer(ILogger<HttpProxyServer> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(200, cancellationToken);
            Console.Write('.');
        }

        _logger.LogDebug("Stopping Http proxy server...");
    }

    public void Dispose()
    {
        _logger.LogTrace("Disposing Http proxy server...");
        GC.SuppressFinalize(this);
    }
}