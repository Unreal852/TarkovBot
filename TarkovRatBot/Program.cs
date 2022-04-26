using System.Text.Json;
using TarkovRatBot;
using TarkovRatBot.Discord;
using TarkovRatBot.Guilded;
using static TarkovRatBot.Core.TarkovCore;

public class Program
{
    private DiscordBot DiscordBot { get; } = new();

    private GuildedBot GuildedBot { get; set; }

    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    private async Task MainAsync()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, Consts.TokensFile);
        if (!File.Exists(filePath))
        {
            WriteLine("Missing tokens file.", ConsoleColor.Red);
            return;
        }

        (string DiscordToken, string GuildedToken) = JsonSerializer.Deserialize<(string, string)>(filePath);

        await Initialize();

        await DiscordBot.Initialize(DiscordToken);
        await GuildedBot.Initialize(GuildedToken);

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