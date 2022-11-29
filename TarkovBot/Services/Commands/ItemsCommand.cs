using Guilded;
using Guilded.Base.Embeds;
using Guilded.Commands;
using TarkovBot.Services.Abstractions;

namespace TarkovBot.Services.Commands;

public class ItemsCommand : CommandModule, IGuildedCommand
{
    private readonly IItemsProvider _itemsProvider;

    public ItemsCommand(IItemsProvider itemsProvider)
    {
        _itemsProvider = itemsProvider;
    }

    [Command("item", Aliases = new[] { "i" })]
    public async Task ItemCommand(CommandEvent e, [CommandParam] string name)
    {
        var items = _itemsProvider.FindByName(name).ToArray();
        var embed = new Embed();

        if (items.Length == 1)
        {
            var item = items[0];
            embed.Title = item.Name;
            //embed.Description = item.Description;
            embed.Footer = new EmbedFooter(item.Id);
            embed.Author = new EmbedAuthor("Data provided by tarkov.dev", "https://tarkov.dev/");
            if (item.InspectImageLink != null)
                embed.Image = new EmbedMedia(item.InspectImageLink);
            if (item.WikiLink != null)
                embed.Url = new Uri(item.WikiLink);
            embed.AddField("Price", item.Avg24HPrice, true);
            embed.AddField("Price per slot", $"{item.PricePerSlots}\n_({item.Slots} slot(s))_", true);
            await e.ReplyAsync(embeds: embed);
            return;
        }

        embed.Title = "Select an item by reaction to this message";

        for (var i = 0; i < items.Length; i++)
        {
            embed.AddField($"{i}. {items[i].Name!}", string.Empty);
        }

        var message = await e.ReplyAsync(embeds: embed);


        for (var i = 0; i < items.Length; i++)
            await message.AddReactionAsync(Emotes.IndexEmotes[i]);
    }
}