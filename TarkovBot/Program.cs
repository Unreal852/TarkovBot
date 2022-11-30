using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using TarkovBot.Services.Abstractions;
using ServiceProvider = TarkovBot.Services.ServiceProvider;

namespace TarkovBot;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        ConfigureLogger();
        ConfigureServices();

        var guilded = ServiceProvider.Instance.GetService<IGuildedService>();
        var itemService = ServiceProvider.Instance.GetService<IItemsProvider>();
        
        //await itemService.UpdateItems();
        await guilded.ConnectAsync();


        while (Console.ReadLine() != "exit")
        {
        }

        await guilded.DisconnectAsync();
    }

    private static void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console(theme: AnsiConsoleTheme.Code).CreateLogger();
    }

    private static void ConfigureServices()
    {
        _ = new ServiceProvider();
    }
}