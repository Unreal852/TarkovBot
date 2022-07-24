// ReSharper disable IdentifierTypo

using Guilded;
using Guilded.Base;
using Guilded.Base.Events;
using Guilded.Base.Users;
using Guilded.Commands;
using Serilog;
using TarkovBot.Guilded.Commands;
using TarkovBot.Guilded.Reactions;

// ReSharper disable InvertIf

namespace TarkovBot.Guilded;

public class GuildedBot
{
    public GuildedBot()
    {
        Bot = new GuildedBotClient();
        Bot.Connected.Subscribe(OnBotConnected);
        Bot.Prepared.Subscribe(OnBotPrepared);
    }

    public  GuildedBotClient Bot { get; }
    private Me?              Me  { get; set; }

    /// <summary>
    /// Initialize the bot.
    /// </summary>
    /// <param name="botConfig">The bot configuration</param>
    public async Task Initialize(BotConfig botConfig)
    {
        Log.Information("Initializing bot");
        if (!botConfig.IsValid)
        {
            Log.Error("Failed to initialize ! The configuration file is invalid");
            return;
        }

        Bot.AddCommands(new SearchItemCommand(this), botConfig.Prefix);
        Bot.AddCommands(new HelpCommand(this), botConfig.Prefix);

        Bot.ReactionAdded.Subscribe(OnMessageReactionAdded);

        await Bot.ConnectAsync(botConfig.GuildedToken);
    }

    /// <summary>
    /// Called when the bot is connected to guilded.
    /// </summary>
    private void OnBotConnected(BaseGuildedClient obj)
    {
        Log.Information("Connected to guilded");
    }

    /// <summary>
    /// Called when the bot is prepared and ready to be used.
    /// </summary>
    private void OnBotPrepared(Me me)
    {
        Me = me;
        Log.Information("The bot is operational");
    }

    /// <summary>
    /// Called when a emote reaction is added to a message.
    /// </summary>
    private async void OnMessageReactionAdded(MessageReactionEvent e)
    {
        if (e.CreatedBy == Me!.Id)
            return;
        await ReactionsManager.HandleReaction(e.Emote.Id, Bot, e);
    }
}