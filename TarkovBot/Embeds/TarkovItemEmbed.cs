using Guilded.Base.Embeds;
using TarkovBot.Data;

namespace TarkovBot.Embeds;

public class TarkovItemEmbed : Embed
{
    public TarkovItemEmbed(TarkovItem item)
    {
        Item = item;
        Title = item.Name;
        Description = item.Description;
        Footer = new EmbedFooter(item.Id);
        Author = new EmbedAuthor("Data provided by tarkov.dev", "https://tarkov.dev/");
        if (item.InspectImageLink != null)
            Image = new EmbedMedia(item.InspectImageLink);
        if (item.WikiLink != null)
            Url = new Uri(item.WikiLink);
        AddField("Price", item.Avg24HPrice, true);
        AddField("Price per slot",
                item.Slots > 1
                        ? $"{item.PricePerSlots}\n_({item.Slots} slots)_"
                        : $"{item.PricePerSlots}\n_({item.Slots} slot)_", true);
    }

    public TarkovItem Item { get; }
}