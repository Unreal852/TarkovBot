using System.Collections.ObjectModel;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using TarkovBot.Core;
using TarkovBot.Core.Data;

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
                Footer = new EmbedFooter("Last Updated"),
                Timestamp = item.Updated,
                Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/"),
                Fields = new List<EmbedField>()
        };

        embed.AddField("Price", $"{item.BasePrice}\n*(base price)*", true);
        embed.AddField("Price Per Slot", $"{item.BasePrice / (item.Width * item.Height)}\n*({item.Width * item.Height} slots)*", true);
        ItemPrice sellFor = item.GetBestSellingTrader();
        ItemPrice buyFor = item.GetBestBuyingTrader();

        if (sellFor != null)
            embed.AddField($"Sell to {sellFor.Vendor.Name}", $"{sellFor.Price}{sellFor.GetCurrencyChar()}", true);

        if (buyFor != null)
            embed.AddField($"Buy From {buyFor.Vendor.Name}", $"{buyFor.Price}{buyFor.GetCurrencyChar()}", true);

        messageContent.Embeds.Add(embed);

        if (item.Types.Contains(ItemType.Ammo) && TarkovCore.AmmoProvider.Cache.TryGetValue(item.Id, out Ammo ammoInfo))
        {
            Embed ammoEmbed = ammoInfo.BuildAmmoEmbed(item);
            messageContent.Embeds.Add(ammoEmbed);
            embed.Color = ammoEmbed.Color;
        }

        return messageContent;
    }

    public static ItemPrice GetBestSellingTrader(this Item item)
    {
        return item.SellFor?.Where(s => s.Price is > 0).MaxBy(s => s.Price);
    }

    public static ItemPrice GetBestBuyingTrader(this Item item)
    {
        return item.BuyFor?.Where(s => s.Price is > 0).MinBy(s => s.Price);
    }
}