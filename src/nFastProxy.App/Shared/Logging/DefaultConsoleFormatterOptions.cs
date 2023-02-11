using Microsoft.Extensions.Logging.Console;

namespace nFastProxy.App.Shared.Logging;

public class DefaultConsoleFormatterOptions : ConsoleFormatterOptions
{
    public DefaultConsoleFormatterOptions()
    {
        TimestampFormat = "HH:mm:ss ";
        IncludeScopes = true;
    }
}