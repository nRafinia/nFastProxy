using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using nFastProxy.App.Shared.Logging;

namespace nFastProxy.App.Extensions.StartupExtensions;

public static class LoggingExt
{
    public static IServiceCollection AddLoggingExt(this IServiceCollection services, AppSetting setting)
    {
        try
        {
            services.AddLogging(_ => _
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
                    .SetMinimumLevel(setting.LogLevel)
                    .AddConsole(_=>_.FormatterName=DefaultConsoleFormatter.DefaultConsoleFormatterName)
                    .AddConsoleFormatter<DefaultConsoleFormatter, DefaultConsoleFormatterOptions>()
                );

            return services;
        }
        catch (Exception e)
        {
            Console.Write("Internal error!");
            Environment.Exit(ExitCodeConst.SettingLogError);
            return services;
        }
    }
}