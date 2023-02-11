using Microsoft.Extensions.DependencyInjection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace nFastProxy.App.Extensions.StartupExtensions;

public static class ConfigExt
{
    private const string SettingFileName = "setting.yml";
    
    public static IServiceCollection AddAppSettings(this IServiceCollection services, out AppSetting appSetting)
    {
        var settingFile = File.ReadAllText(SettingFileName);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(NullNamingConvention.Instance)
            .Build();

        var setting = deserializer.Deserialize<AppSetting>(settingFile);
        services.AddSingleton(setting);

        appSetting = setting;
        return services;
    }
}