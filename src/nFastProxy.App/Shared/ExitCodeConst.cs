namespace nFastProxy.App.Shared;

internal static class ExitCodeConst
{
    // App setting
    public const int AppSettingNotFound = 100;
    public const int AppSettingAccessDenied = 101;
    public const int AppSettingHasProblems = 102;
    
    // Logging settings
    public const int SettingLogError = 120;
    
    // Http proxy provider
    public const int HttpProxyProviderNotFound = 150;
}