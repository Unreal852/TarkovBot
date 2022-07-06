// ReSharper disable IdentifierTypo

using Guilded;
using Guilded.Base.Users;
using Guilded.Commands;
using TarkovBot.Guilded.Commands;
using static TarkovBot.Core.TarkovCore;

namespace TarkovBot.Guilded;

public class GuildedBot
{
    public GuildedBot()
    {
        Bot = new GuildedBotClient();
        Bot.Prepared.Subscribe(OnBotReady);
    }

    public GuildedBotClient Bot { get; }

    public async Task Initialize(string token)
    {
        WriteLine("Initializing Guilded Bot...", ConsoleColor.Yellow);
        if (string.IsNullOrWhiteSpace(token))
        {
            WriteLine("Failed to initialize guilded bot. Missing Token.", ConsoleColor.Red);
            return;
        }

        Bot.AddCommands(new BotCommands(), "!");

        await Bot.ConnectAsync(token);
    }

    private void OnBotReady(Me me)
    {
        WriteLine("Guilded Bot Initialized !", ConsoleColor.Green);
    }
}