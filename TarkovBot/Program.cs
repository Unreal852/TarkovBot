using System.Text.Json;
using System.Text.Json.Serialization;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using TarkovBot;
using TarkovBot.EFT.Data.Provider;
using TarkovBot.Guilded;

public class Program
{
    private static GuildedBot GuildedBot { get; } = new();

    public static Task Main(string[] args)
    {
        return MainAsync();
    }

    private static async Task MainAsync()
    {
        Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    .WriteTo.File("logs/logs_.log", rollingInterval: RollingInterval.Day).CreateLogger();

        string configFilePath = Path.Combine(Environment.CurrentDirectory, Constants.BotConfigFile);
        var config = new BotConfig();
        if (!config.IsValid)
        {
            if (!File.Exists(configFilePath))
            {
                Log.Error("The bot configuration file does not exists");
                return;
            }

            config = await JsonSerializer.DeserializeAsync<BotConfig>(File.OpenRead(configFilePath),
                    new JsonSerializerOptions() { Converters = { new JsonStringEnumConverter() } });
            if (config is not { IsValid: true })
            {
                Log.Error("The bot configuration file is invalid");
                return;
            }
        }

        await DataProviders.Initialize().ConfigureAwait(false);
        await GuildedBot.Initialize(config).ConfigureAwait(false);

        HandleInput();
    }

    /// <summary>
    /// Handle basic user input and keep the console running.
    /// </summary>
    private static void HandleInput()
    {
        string? input;
        do
        {
            input = Console.ReadLine();
            if (input == "clear")
                Console.Clear();
        } while (input != "exit");

        Log.Information("Stopping bot");
    }
}