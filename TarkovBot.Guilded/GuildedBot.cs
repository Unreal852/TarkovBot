// ReSharper disable IdentifierTypo

using Guilded;
using Guilded.Base.Events;
using Guilded.Base.Users;
using Guilded.Commands;
using TarkovBot.Guilded.Commands;
using TarkovBot.Guilded.Reactions;
using static TarkovBot.Core.TarkovCore;

// ReSharper disable InvertIf

namespace TarkovBot.Guilded;

public class GuildedBot
{
    public GuildedBot()
    {
        Bot = new GuildedBotClient();
        Bot.Prepared.Subscribe(OnBotReady);
    }

    public  GuildedBotClient Bot { get; }
    private Me?              Me  { get; set; }

    public async Task Initialize(string token)
    {
        WriteLine("Initializing Guilded BotClient...", ConsoleColor.Yellow);
        if (string.IsNullOrWhiteSpace(token))
        {
            WriteLine("Failed to initialize guilded botClient. Missing Token.", ConsoleColor.Red);
            return;
        }

#if RELEASE
        Bot.AddCommands(new BotCommands(this), "!");
#elif DEBUG
        Bot.AddCommands(new BotCommands(this), "?");
#endif
        Bot.ReactionAdded.Subscribe(OnMessageReactionAdded);

        await Bot.ConnectAsync(token);
    }

    private void OnBotReady(Me me)
    {
        Me = me;
        WriteLine("Guilded BotClient Initialized !", ConsoleColor.Green);
    }

    private async void OnMessageReactionAdded(MessageReactionEvent e)
    {
        if (e.CreatedBy == Me.Id)
            return;
        await ReactionsManager.HandleReaction(e.Emote.Id, Bot, e);
    }
}