using Microsoft.Extensions.DependencyInjection;
using nFastProxy.Application.Services;
using nFastProxy.Domain.Shared.Abstraction;
using nFastProxy.Domain.Shared.Models;

namespace nFastProxy.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, Options options)
    {
        services.AddSingleton<IProxyServer, HttpProxyServer>();
        return services;
    }
}