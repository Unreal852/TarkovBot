using System.Collections.Concurrent;
using Guilded;
using Guilded.Base.Embeds;
using Guilded.Commands;
using Guilded.Content;
using Guilded.Events;
using TarkovBot.Data;
using TarkovBot.Embeds;
using TarkovBot.Services.Abstractions;

namespace TarkovBot.Services.Commands;

public class ItemsCommand : CommandModule, IGuildedCommand
{
    private readonly IItemsProvider                                _itemsProvider;
    private readonly ConcurrentDictionary<Guid, ItemSelectionData> _selections = new();

    public ItemsCommand(IItemsProvider itemsProvider)
    {
        _itemsProvider = itemsProvider;
    }

    [Command("item", Aliases = new[] { "i" })]
    public async Task ItemCommand(CommandEvent e, [CommandParam] string name)
    {
        var items = _itemsProvider.FindByName(name).ToArray();

        if (items.Length == 1)
        {
            var itemEmbed = new TarkovItemEmbed(items[0]);
            await e.ReplyAsync(embeds: itemEmbed);
            return;
        }

        var embed = new Embed
        {
                Title = "Select an item by reaction to this message",
                Footer = new EmbedFooter("Expire in one minute")
        };

        for (var i = 0; i < items.Length; i++)
        {
            embed.AddField($"{i}. {items[i].Name!}", string.Empty);
        }

        var message = await e.ReplyAsync(embeds: embed);

        if (_selections.TryAdd(message.Id, new ItemSelectionData { Message = message, Items = items }))
        {
            message.ReactionAdded.ElapseOn(TimeSpan.FromSeconds(10)).Subscribe(OnReactionAdded,
                    () =>
                    {
                        _selections.TryRemove(message.Id, out var itemSelection);
                        itemSelection!.Message.DeleteAsync();
                    });
        }

        for (var i = 0; i < items.Length; i++)
            await message.AddReactionAsync(Emotes.IndexEmotes[i]);
    }

    private void OnReactionAdded(MessageReactionEvent e)
    {
        if (e.CreatedBy == e.ParentClient.Id)
            return;
        if (e.Emote.Id is < Emotes.ZeroIndexEmote or > Emotes.MaxIndexEmote)
            return;
        if (_selections.TryGetValue(e.MessageId, out var itemSelection))
        {
            var index = e.Emote.Id - Emotes.ZeroIndexEmote;
            if (index >= itemSelection.Items.Length)
                return;
            itemSelection.Message.UpdateAsync(embeds: new TarkovItemEmbed(itemSelection.Items[index]));
        }
    }
}

public class ItemSelectionData
{
    public required Message      Message { get; init; }
    public required TarkovItem[] Items   { get; init; }
}