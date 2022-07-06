using Discord;
using TarkovRatBot.Core;
using TarkovRatBot.Core.Extensions;
using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Core.TarkovData.Ammos;

namespace TarkovBot.Discord.Extensions;

public static class ItemExtensions
{
    public static (Embed Embed, MessageComponent Component) BuildItemEmbed(this Item item)
    {
        ComponentBuilder componentBuilder = null;
        var embedBuilder = new EmbedBuilder
        {
                Title = $"{item.Name} ({item.ShortName})",
                Url = item.WikiLink,
                ThumbnailUrl = item.ImageLink,
                Footer = new EmbedFooterBuilder { Text = "Last Updated" },
                Timestamp = item.Updated,
                Author = new EmbedAuthorBuilder { Name = "Provided by tarkov.dev", Url = "https://tarkov.dev/" },
                Fields = new List<EmbedFieldBuilder>()
        };
        embedBuilder.AddField(new EmbedFieldBuilder { Name = "Base Price", Value = item.BasePrice ?? 0, IsInline = true });
        ItemPrice sellFor = item.SellFor.Where(s => s.Price is > 0).MaxBy(s => s.Price);
        ItemPrice buyFor = item.BuyFor.Where(s => s.Price is > 0).MinBy(s => s.Price);
        if (buyFor != null)
        {
            var loyaltiRequirement = "";
            if (buyFor.Requirements is { Length: > 0 })
            {
                PriceRequirement requirement = buyFor.Requirements.FirstOrDefault(r => r.RequirementType == RequirementType.LoyaltyLevel);
                if (requirement != null)
                    loyaltiRequirement = $"(LL {requirement.Value ?? 0})";
            }

            embedBuilder.AddField(new EmbedFieldBuilder
            {
                    Name = $"Buy From {buyFor.ItemSourceName.FirstCharToUpperCase()} {loyaltiRequirement}",
                    Value = $"{buyFor.Price} {buyFor.Currency}", IsInline = true
            });
        }

        if (sellFor != null)
            embedBuilder.AddField(new EmbedFieldBuilder
            {
                    Name = $"Sell To {sellFor.ItemSourceName.FirstCharToUpperCase()}",
                    Value = $"{sellFor.Price} {sellFor.Currency}", IsInline = true
            });

        if (item.ItemTypes.Contains(ItemType.Ammo) && TarkovCore.AmmoCache.Cache.TryGetValue(item.Id, out Ammo ammoInfo))
        {
            embedBuilder.Color = ammoInfo.GetPenetrationClassColor();
            componentBuilder = new ComponentBuilder().WithButton("Ammo Infos", $"{Consts.ButtonAmmoMoreInfosId}@{item.Id}");
        }

        return (embedBuilder.Build(), componentBuilder?.Build());
    }
}