using Microsoft.Extensions.DependencyInjection;
using nFastProxy.App.Shared;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace nFastProxy.App.Extensions.StartupExtensions;

public static class ConfigExt
{
    private const string SettingFileName = "setting.yml";
    
    public static IServiceCollection AddAppSettings(this IServiceCollection services, out AppSetting appSetting)
    {
        appSetting = null;
        if (!File.Exists(SettingFileName))
        {
            Console.Write("Setting file '{0}' does not exist.", SettingFileName);
            Environment.Exit(ExitCodeConst.AppSettingNotFound);
            return services;
        }

        try
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
        catch (FieldAccessException e)
        {
            Console.Write("Setting file '{0}' access is denied.", SettingFileName);
            Environment.Exit(ExitCodeConst.AppSettingAccessDenied);
            return services;
        }
        catch (Exception e)
        {
            Console.Write("Error int read setting file '{0}'.", SettingFileName);
            Environment.Exit(ExitCodeConst.AppSettingHasProblems);
            return services;
        }
    }
}