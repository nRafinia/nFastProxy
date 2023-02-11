using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using nFastProxy.Domain.Shared.Models;

var parser = new Parser(settings =>
{
    settings.HelpWriter = Console.Error;
    settings.CaseInsensitiveEnumValues = true;
});

await parser.ParseArguments<Options>(args)
    //.WithNotParsed(HandleParseError)
    .WithParsedAsync(RunAsync);

static async Task RunAsync(Options options)
{
    var services = new ServiceCollection();
    Startup.RegisterServices(services,options);

    var provider = services.BuildServiceProvider();
    await Startup.RunAsync(provider, options);
}

/*static void HandleParseError(IEnumerable<Error> errs)
{
    //handle errors
}*/
