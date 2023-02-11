using nFastProxy.Domain.Shared.Models;

namespace nFastProxy.Domain.Shared.Abstraction;

public interface IProxyServer : IDisposable
{
    Task StartAsync(CancellationToken cancellationToken);
}