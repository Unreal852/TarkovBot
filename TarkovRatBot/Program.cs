using TarkovRatBot;
using TarkovRatBot.Bots;
using TarkovRatBot.GraphQL;
using TarkovRatBot.Tarkov;

public class Program
{
    public static readonly GraphQlQuery ItemsByNameQuery;
    public static readonly GraphQlQuery AmmoQuery;

    static Program()
    {
        ItemsByNameQuery = new GraphQlQuery("ItemsByName");
        AmmoQuery = new GraphQlQuery("Ammo");
    }

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

    private GuildedBot GuildedBot = new(Consts.GuildedBotToken);
    private DiscordBot DiscordBot = new(Consts.DiscordBotToken);

    private async Task MainAsync()
    {
        
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