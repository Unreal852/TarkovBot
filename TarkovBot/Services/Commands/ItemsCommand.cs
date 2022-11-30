using Guilded.Base.Embeds;
using Guilded.Commands;
using Guilded.Events;
using TarkovBot.Data;
using TarkovBot.Embeds;
using TarkovBot.Services.Abstractions;

namespace TarkovBot.Services.Commands;

public class ItemsCommand : CommandModule, IGuildedCommand
{
    private readonly IItemsProvider                 _itemsProvider;
    private readonly IGuildedMessageReactionService _guildedReactionService;

    public ItemsCommand(IItemsProvider itemsProvider, IGuildedMessageReactionService guildedMessageReactionService)
    {
        _itemsProvider = itemsProvider;
        _guildedReactionService = guildedMessageReactionService;
    }

    [Command("help", Aliases = new[] { "h" })]
    public Task HelpCommand(CommandEvent e)
    {
        var embed = new Embed
        {
                Title = "EFT Commands"
        };
        embed.AddField("t!item <item_name> or t!i",
                "Search for a specific item infos. \n Example: t!i afak");
        return e.ReplyAsync(embeds: embed);
    }

    [Command("item", Aliases = new[] { "i" })]
    public async Task ItemCommand(CommandEvent e, [CommandParam] string name)
    {
        if (_itemsProvider.IsUpdating)
        {
            await e.ReplyAsync("I'm updating items data ! Please retry in a minute");
            return;
        }

        var items = _itemsProvider.FindByName(name).ToArray();

        if (items.Length == 1)
        {
            var itemEmbed = new TarkovItemEmbed(items[0]);
            await e.ReplyAsync(embeds: itemEmbed);
            return;
        }

        var embed = new Embed
        {
                Title = "Select an item by reacting to this message",
                Footer = new EmbedFooter("Expire in one minute")
        };

        for (var i = 0; i < items.Length; i++)
        {
            embed.AddField($"{i}. {items[i].Name!}", string.Empty);
        }

        var message = await e.ReplyAsync(embeds: embed);

        if (_guildedReactionService.TryAdd(new MessageData
                    { Message = message, Callback = OnReactionAdded, Data = items }))
        {
            for (var i = 0; i < items.Length; i++)
                await message.AddReactionAsync(Emotes.IndexEmotes[i]);
        }
    }

    private static async void OnReactionAdded(MessageReactionEvent e, MessageData messageData)
    {
        if (e.Emote.Id is < Emotes.ZeroIndexEmote or > Emotes.MaxIndexEmote)
            return;
        if (messageData.Data is not TarkovItem[] items)
            return;
        var index = e.Emote.Id - Emotes.ZeroIndexEmote;
        if (index >= items.Length)
            return;
        await messageData.Message.UpdateAsync(embeds: new TarkovItemEmbed(items[index]));
    }
}