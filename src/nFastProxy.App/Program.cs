using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddAppSettings(out var appSetting)
    .AddLoggingExt(appSetting);

var provider = services.BuildServiceProvider();
var logger = provider.GetService<ILogger<Program>>();
logger!.LogTrace("Starting...");
logger!.LogDebug("Starting...");
logger!.LogInformation("Starting...");
logger!.LogWarning("Starting...");
logger!.LogError("Starting...");
logger!.LogCritical("Starting...");

