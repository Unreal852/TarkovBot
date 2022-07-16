using System.Collections.ObjectModel;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using TarkovBot.Core.Data;
using TarkovBot.Core.Extensions;

namespace TarkovBot.Guilded.Extensions;

public static class ItemExtensions
{
    public static MessageContent BuildMessageContent(this Item item)
    {
        var messageContent = new MessageContent
        {
                Embeds = new Collection<Embed>()
        };

        var embed = new Embed
        {
                Title = $"{item.Name} ({item.ShortName})",
                Url = new Uri(item.WikiLink                   ?? ""),
                Thumbnail = new EmbedMedia(item.GridImageLink ?? ""),
                Footer = new EmbedFooter(item.Id),
                Timestamp = item.Updated,
                Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/"),
                Fields = new List<EmbedField>(),
        };

        embed.AddField("Price", $"{item.Infos.LowestPriceRub:N0}**₽**\n*(lowest price)*", true);
        embed.AddField("Price Per Slot", $"{item.Infos.PricePerSlotRub:N0}\n*({item.Infos.TotalSlots} slot{(item.Infos.TotalSlots > 1 ? "s" : "")})*", true);

        if (item.Infos.BestSellFor != null)
        {
            ItemPrice sellFor = item.Infos.BestSellFor;
            embed.AddField($"Sell to {sellFor.Vendor.Name}", $"{sellFor.PriceRUB:N0}{sellFor.GetCurrencyChar()}", true);
        }

        messageContent.Embeds.Add(embed);

        if (item.IsAmmo(out Ammo? ammo))
            embed.Color = ammo!.GetPenetrationClassColor();

        return messageContent;
    }

    public static ItemPrice? GetBestSellingTrader(this Item item)
    {
        return item.SellFor?.MaxBy(s => s.PriceRUB);
    }

    public static ItemPrice? GetBestBuyingTrader(this Item item)
    {
        return item.BuyFor?.Where(s => s.PriceRUB is > 0).MinBy(s => s.PriceRUB);
    }
}