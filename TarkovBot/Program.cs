using System.Text.Json;
using TarkovBot;
using TarkovBot.Guilded;
using static TarkovBot.Core.TarkovCore;

public class Program
{
    //private DiscordBot DiscordBot { get; } = new();

    private GuildedBot GuildedBot { get; } = new();

    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    private async Task MainAsync()
    {
#if RELEASE
        string filePath = Path.Combine(Environment.CurrentDirectory, Constants.TokensFile);
        if (!File.Exists(filePath))
        {
            WriteLine("Missing tokens file.", ConsoleColor.Red);
            return;
        }

        await using FileStream reader = File.OpenRead(filePath);
        var tokens = await JsonSerializer.DeserializeAsync<BotsTokens>(reader);
        if (tokens == null)
        {
            WriteLine("Failed to deserialize tokens.", ConsoleColor.Red);
            return;
        }
#elif DEBUG
        var tokens = new BotsTokens();
#endif

        await Initialize();

        //await DiscordBot.Initialize(tokens.DiscordToken);
        await GuildedBot.Initialize(tokens.GuildedToken);

        HandleInput();
    }

    private void HandleInput()
    {
        string input;
        do
        {
            input = Console.ReadLine();
            if (input == "clear")
                Console.Clear();
        } while (input != null && input != "exit");
    }
}