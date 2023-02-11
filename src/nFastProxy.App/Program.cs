using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddAppSettings(out var appSetting)
    .AddLoggingExt(appSetting);

var provider = services.BuildServiceProvider();
