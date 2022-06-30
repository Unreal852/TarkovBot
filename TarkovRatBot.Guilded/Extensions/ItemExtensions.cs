using System.Collections.ObjectModel;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using TarkovRatBot.Core;
using TarkovRatBot.Core.Extensions;
using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Core.TarkovData.Ammos;
using TarkovRatBot.Core.TarkovData.Items;

namespace TarkovRatBot.Guilded.Extensions;

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

        embed.AddField(new EmbedField("Base Price", item.BasePrice.ToString(), true));
        ItemPrice sellFor = item.SellFor.Where(s => s.Price is > 0).MaxBy(s => s.Price);
        ItemPrice buyFor = item.BuyFor.Where(s => s.Price is > 0).MinBy(s => s.Price);

        if (buyFor != null)
        {
            // var loyaltyRequirement = "";
            // if (buyFor.Requirements is { Length: > 0 })
            // {
            //     PriceRequirement requirement = buyFor.Requirements.FirstOrDefault(r => r.RequirementType == RequirementType.LoyaltyLevel);
            //     if (requirement != null)
            //         loyaltyRequirement = $"(LL {requirement.Value ?? 0})";
            // }

            embed.AddField(new EmbedField($"Buy From {buyFor.Vendor.Name.FirstCharToUpperCase()}",
                    $"{buyFor.Price} {buyFor.Currency}", true));
        }

        if (sellFor != null)
        {
            embed.AddField(new EmbedField($"Sell To {sellFor.Vendor.Name.FirstCharToUpperCase()}",
                    $"{sellFor.Price} {sellFor.Currency}", true));
        }

        messageContent.Embeds.Add(embed);

        if (item.Types.Contains(ItemType.Ammo) && TarkovCore.AmmoCache.Cache.TryGetValue(item.Id, out Ammo ammoInfo))
        {
            Embed ammoEmbed = ammoInfo.BuildAmmoEmbed(item);
            messageContent.Embeds.Add(ammoEmbed);
            embed.Color = ammoEmbed.Color;
        }

        return messageContent;
    }
}