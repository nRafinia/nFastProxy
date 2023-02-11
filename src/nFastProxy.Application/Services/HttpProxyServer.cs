using Microsoft.Extensions.Logging;
using nFastProxy.Domain.Shared.Abstraction;
using nFastProxy.Domain.Shared.Models;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.Exceptions;
using Titanium.Web.Proxy.Models;

namespace nFastProxy.Application.Services;

public class HttpProxyServer : IProxyServer
{
    private readonly ILogger<HttpProxyServer> _logger;
    private readonly Options _options;
    private ProxyServer? _proxyServer = null;

    public HttpProxyServer(ILogger<HttpProxyServer> logger, Options options)
    {
        _logger = logger;
        _options = options;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
// we do all this in here so we can reload settings with a simple restart

        _proxyServer = new ProxyServer(false);

            /*if (Settings.Default.ListeningPort <= 0 ||
                Settings.Default.ListeningPort > 65535)
                throw new Exception("Invalid listening port");*/
//https://github.com/justcoding121/titanium-web-proxy/blob/develop/examples/Titanium.Web.Proxy.Examples.WindowsService/App.config
            _proxyServer.CheckCertificateRevocation = Settings.Default.CheckCertificateRevocation;
            _proxyServer.ConnectionTimeOutSeconds = Settings.Default.ConnectionTimeOutSeconds;
            _proxyServer.Enable100ContinueBehaviour = Settings.Default.Enable100ContinueBehaviour;
            _proxyServer.EnableConnectionPool = Settings.Default.EnableConnectionPool;
            _proxyServer.EnableTcpServerConnectionPrefetch = Settings.Default.EnableTcpServerConnectionPrefetch;
            _proxyServer.EnableWinAuth = Settings.Default.EnableWinAuth;
            _proxyServer.ForwardToUpstreamGateway = Settings.Default.ForwardToUpstreamGateway;
            _proxyServer.MaxCachedConnections = Settings.Default.MaxCachedConnections;
            _proxyServer.ReuseSocket = Settings.Default.ReuseSocket;
            _proxyServer.TcpTimeWaitSeconds = Settings.Default.TcpTimeWaitSeconds;
            _proxyServer.CertificateManager.SaveFakeCertificates = Settings.Default.SaveFakeCertificates;
            _proxyServer.EnableHttp2 = Settings.Default.EnableHttp2;
            _proxyServer.NoDelay = Settings.Default.NoDelay;

            if (Settings.Default.ThreadPoolWorkerThreads < 0)
                _proxyServer.ThreadPoolWorkerThread = Environment.ProcessorCount;
            else
                _proxyServer.ThreadPoolWorkerThread = Settings.Default.ThreadPoolWorkerThreads;

            if (Settings.Default.ThreadPoolWorkerThreads < Environment.ProcessorCount)
                ProxyServiceEventLog.WriteEntry(
                    $"Worker thread count of {Settings.Default.ThreadPoolWorkerThreads} is below the " +
                    $"processor count of {Environment.ProcessorCount}. This may be on purpose.",
                    EventLogEntryType.Warning);

            var explicitEndPointV4 = new ExplicitProxyEndPoint(IPAddress.Any, Settings.Default.ListeningPort,
                Settings.Default.DecryptSsl);

            _proxyServer.AddEndPoint(explicitEndPointV4);

            if (Settings.Default.EnableIpV6)
            {
                var explicitEndPointV6 = new ExplicitProxyEndPoint(IPAddress.IPv6Any, Settings.Default.ListeningPort,
                    Settings.Default.DecryptSsl);

                _proxyServer.AddEndPoint(explicitEndPointV6);
            }

            if (Settings.Default.LogErrors)
                _proxyServer.ExceptionFunc = ProxyException;

            _proxyServer.Start();

            ProxyServiceEventLog.WriteEntry($"Service Listening on port {Settings.Default.ListeningPort}",
                EventLogEntryType.Information);
    }

    private void ProxyException(Exception exception)
    {
        string message;
        if (exception is ProxyHttpException pEx)
            message =
                $"Unhandled Proxy Exception in ProxyServer, UserData = {pEx.Session?.UserData}, URL = {pEx.Session?.HttpClient.Request.RequestUri} Exception = {pEx}";
        else
            message = $"Unhandled Exception in ProxyServer, Exception = {exception}";

        ProxyServiceEventLog.WriteEntry(message, EventLogEntryType.Error);
    }
    
    public void Dispose()
    {
        _logger.LogTrace("Disposing Http proxy server...");
        _proxyServer?.Stop();
        _proxyServer?.Dispose();
        GC.SuppressFinalize(this);
    }
}