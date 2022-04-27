using Guilded.Base.Embeds;
using TarkovRatBot.Core;
using TarkovRatBot.Core.Extensions;
using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Core.TarkovData.Ammos;

namespace TarkovRatBot.Guilded.Extensions;

public static class ItemExtensions
{
    public static Embed[] BuildMessageContent(this Item item)
    {
        Embed[] embeds = new Embed[2];

        var embed = new Embed    
        {
            Title = $"{item.Name} ({item.ShortName})",
            Url =  new Uri(item.WikiLink),
            Thumbnail = new EmbedMedia(item.ImageLink),
            Footer = new EmbedFooter("Last Updated"),
            Timestamp = item.Updated,
            Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/"),
            Fields = new List<EmbedField>()
        };

        embed.AddField(new EmbedField("Base Price", (item.BasePrice ?? 0).ToString(), true));
        ItemPrice sellFor = item.SellFor.Where(s => s.Price is > 0).MaxBy(s => s.Price);
        ItemPrice buyFor = item.BuyFor.Where(s => s.Price is > 0).MinBy(s => s.Price);
        if (buyFor != null)
        {
            var loyaltiRequirement = "";
            if (buyFor.Requirements is { Length: > 0 })
            {
                PriceRequirement requirement = buyFor.Requirements.FirstOrDefault(r => r.RequirementType == ERequirementType.LoyaltyLevel);
                if (requirement != null)
                    loyaltiRequirement = $"(LL {requirement.Value ?? 0})";
            }

            embed.AddField(new EmbedField($"Buy From {buyFor.ItemSourceName.FirstCharToUpperCase()} {loyaltiRequirement}",
                $"{buyFor.Price} {buyFor.Currency}", true));
        }
        
        if (sellFor != null)
        {
            embed.AddField(new EmbedField($"Sell To {sellFor.ItemSourceName.FirstCharToUpperCase()}",
                $"{sellFor.Price} {sellFor.Currency}", true));
        }

        embeds[0] = embed;

        if (item.ItemTypes.Contains(EItemType.Ammo) && TarkovCore.AmmoCache.Cache.TryGetValue(item.Id, out Ammo ammoInfo))
        {
            embeds[1] = ammoInfo.BuildAmmoEmbed();
            embed.Color = embeds[1].Color;
        }
        
        return embeds;
    }
}