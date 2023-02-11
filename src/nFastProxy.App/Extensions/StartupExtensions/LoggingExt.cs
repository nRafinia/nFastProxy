using Microsoft.Extensions.DependencyInjection;

namespace nFastProxy.App.Extensions.StartupExtensions;

public static class LoggingExt
{
    public static IServiceCollection AddLoggingExt(this IServiceCollection services, AppSetting setting)
    {
        services.AddLogging(_ => _
            .AddFilter("Microsoft", LogLevel.Warning)
            .AddFilter("System", LogLevel.Warning)
            .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
            .SetMinimumLevel(setting.LogLevel)
            .AddConsole()
        );

        return services;
    }
}