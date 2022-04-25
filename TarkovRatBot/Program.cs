using TarkovRatBot;
using TarkovRatBot.Discord;
using TarkovRatBot.Guilded;
using static TarkovRatBot.Core.TarkovCore;

public class Program
{
    private readonly DiscordBot DiscordBot = new(Consts.DiscordBotToken);

    private readonly GuildedBot GuildedBot = new(Consts.GuildedBotToken);

    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    private async Task MainAsync()
    {
        await Initialize();

        await DiscordBot.Initialize();
        await GuildedBot.Initialize();

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