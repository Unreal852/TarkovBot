// ReSharper disable IdentifierTypo

using Guilded;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Events;
using Guilded.Base.Users;
using Guilded.Commands;
using TarkovBot.Core.Data;
using TarkovBot.Guilded.Commands;
using TarkovBot.Guilded.Extensions;
using TarkovBot.Guilded.Messages;
using TarkovBot.Guilded.Messages.Implementations;
using static TarkovBot.Core.TarkovCore;
using Task = System.Threading.Tasks.Task;

// ReSharper disable InvertIf

namespace TarkovBot.Guilded;

public class GuildedBot
{
    public GuildedBot()
    {
        Bot = new GuildedBotClient();
        MessagesManager = new MessagesManager(this);
        Bot.Prepared.Subscribe(OnBotReady);
    }

    public GuildedBotClient Bot             { get; }
    public MessagesManager  MessagesManager { get; }

    private Me Me { get; set; }

    public async Task Initialize(string token)
    {
        WriteLine("Initializing Guilded Bot...", ConsoleColor.Yellow);
        if (string.IsNullOrWhiteSpace(token))
        {
            WriteLine("Failed to initialize guilded bot. Missing Token.", ConsoleColor.Red);
            return;
        }

        Bot.AddCommands(new BotCommands(this), "!");
        Bot.ReactionAdded.Subscribe(OnMessageReactionAdded);

        await Bot.ConnectAsync(token);
    }

    private void OnBotReady(Me me)
    {
        Me = me;
        WriteLine("Guilded Bot Initialized !", ConsoleColor.Green);
    }

    private async void OnMessageReactionAdded(MessageReactionEvent e)
    {
        if (e.CreatedBy == Me.Id || !Constants.EmotesIds.Contains(e.Emote.Id))
            return;
        if (MessagesManager.TryTake(e.MessageId, out IMessageInfos messageInfos) && messageInfos is ItemsMessageSelector messageSelector)
        {
            int index = e.Emote.ToSelectorIndex();
            if (index >= messageSelector.Items.Length)
            {
                WriteLine("Message reaction out of bounds.", ConsoleColor.Red);
                return;
            }

            Item item = messageSelector.Items[index];
            MessageContent messageContent = item.BuildMessageContent();
            if (messageContent.Embeds is { Count: >= 1 })
            {
                foreach (Embed embed in messageContent.Embeds)
                    await messageSelector.CommandMessage.ReplyAsync(embeds: embed);
            }

            await messageSelector.Message.DeleteAsync();
        }
    }
}