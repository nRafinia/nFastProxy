using Microsoft.Extensions.DependencyInjection;
using nFastProxy.App.Shared.Logging;
using nFastProxy.Domain.Shared.Models;

namespace nFastProxy.App.Extensions.StartupExtensions;

public static class LoggingExt
{
    public static IServiceCollection AddLoggingExt(this IServiceCollection services, Options setting)
    {
        try
        {
            services.AddLogging(_ => _
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
                .SetMinimumLevel(setting.LogLevel)
                .AddConsole(_ => _.FormatterName = DefaultConsoleFormatter.DefaultConsoleFormatterName)
                .AddConsoleFormatter<DefaultConsoleFormatter, DefaultConsoleFormatterOptions>()
            );

            return services;
        }
        catch (Exception e)
        {
            Console.Write("Internal error!");
            if (setting.LogLevel <= LogLevel.Debug)
            {
                Console.WriteLine(JsonSerializer.Serialize(e));
            }

            Environment.Exit(ExitCodeConst.SettingLogError);
            return services;
        }
    }
}