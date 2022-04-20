using TarkovRatBot;
using TarkovRatBot.Bots;
using TarkovRatBot.GraphQL;
using TarkovRatBot.Tarkov;

public class Program
{
    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private readonly GuildedBot GuildedBot = new(Consts.GuildedBotToken);
    private readonly DiscordBot DiscordBot = new(Consts.DiscordBotToken);

    private async Task MainAsync()
    {
        Queries.InitQueries();

        if (!await TarkovCache.CacheAmmoInfos())
            WriteLine("Failed to cache ammo data.", ConsoleColor.Red);
        else
            WriteLine($"Successfully cached {TarkovCache.AmmoCache.Count} ammos.", ConsoleColor.Green);

        await DiscordBot.Initialize();
        await GuildedBot.Initialize();

        HandleInput();
    }

    private void HandleInput()
    {
        string? input;
        do
        {
            input = Console.ReadLine();
            if (input == "clear")
                Console.Clear();
        } while (input != null && input != "exit");
    }
}