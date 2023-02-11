namespace nFastProxy.Domain.Shared.Models;

public class Options
{
    [Option(Default = LogLevel.Information, HelpText = "Log level (trace|debug|information|warning|error|critical|none)")]
    public LogLevel LogLevel { get; set; }

    [Option(Default = "127.0.0.1", HelpText = "Run proxy in local IP address")]
    public string IP { get; set; } = "127.0.0.1";

    [Option(Default = 1024, HelpText = "Run proxy in local port number")]
    public int Port { get; set; }
}