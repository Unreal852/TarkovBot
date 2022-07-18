using System.Collections.ObjectModel;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using TarkovBot.EFT.Data;
using TarkovBot.EFT.Data.Raw;

namespace TarkovBot.Extensions;

/// <summary>
/// Extensions for the <see cref="ItemInfos"/> type.
/// </summary>
public static class ItemInfosExtensions
{
    public static bool IsAmmo(this ItemInfos item)
    {
        return item.Ammo != null;
    }

    public static MessageContent BuildMessageContent(this ItemInfos item)
    {
        var messageContent = new MessageContent
        {
                Embeds = new Collection<Embed>()
        };

        var embed = new Embed
        {
                Title = $"{item.Name} ({item.ShortName})",
                Url = new Uri(item.WikiLink                        ?? ""),
                Thumbnail = new EmbedMedia(item.Item.GridImageLink ?? ""),
                Footer = new EmbedFooter(item.Id),
                Timestamp = item.Item.Updated,
                Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/"),
                Fields = new List<EmbedField>(),
        };

        embed.AddField("Price", $"{item.LowestPriceRub:N0}**₽**\n*(lowest price)*", true);
        embed.AddField("Price Per Slot", $"{item.PricePerSlotRub:N0}\n*({item.TotalSlots} slot{(item.TotalSlots > 1 ? "s" : "")})*", true);

        if (item.BestSellFor != null)
        {
            ItemPrice sellFor = item.BestSellFor;
            embed.AddField($"Sell to {sellFor.Vendor.Name}", $"{sellFor.PriceRUB:N0}{sellFor.GetCurrencyChar()}", true);
        }

        messageContent.Embeds.Add(embed);

        if (item.IsAmmo())
            embed.Color = item.Ammo!.ArmorPenetrationColor;

        return messageContent;
    }

    public static Embed? BuildAmmoEmbed(this ItemInfos item)
    {
        if (!item.IsAmmo())
            return default;
        AmmoInfos ammo = item.Ammo!;

        var embed = new Embed
        {
                Title = $"{item.Name} ({item.ShortName})",
                Url = new Uri(item.WikiLink                        ?? ""),
                Thumbnail = new EmbedMedia(item.Item.GridImageLink ?? ""),
                Footer = new EmbedFooter("Last Updated"),
                Timestamp = item.Item.Updated,
                Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/"),
                Fields = new List<EmbedField>(),
        };

        embed.AddField("Damages (Flesh)", ammo.Ammo.Damage, true);
        embed.AddField("Damages (Armor)", ammo.Ammo.ArmorDamage, true);
        embed.AddField("Velocity ", $"{ammo.Ammo.InitialSpeed} m/s", true);
        embed.AddField("Penetration Power", ammo.Ammo.PenetrationPower, true);
        embed.AddField("Frag Chances", (int?)(ammo.Ammo.FragmentationChance * 100) ?? 0, true);
        if (ammo.Ammo.LightBleedModifier is > 0)
            embed.AddField("Light Bleed Chances", (int?)(ammo.Ammo.LightBleedModifier * 100) ?? 0, true);
        if (ammo.Ammo.HeavyBleedModifier is > 0)
            embed.AddField("Heavy Bleed Chances", (int?)(ammo.Ammo.HeavyBleedModifier * 100) ?? 0, true);

        embed.AddField("Armor Class Real (Effective)",
                $"{ammo.RealArmorPenetration} {(ammo.RealArmorPenetration != ammo.EffectiveArmorPenetration ? $"({ammo.EffectiveArmorPenetration})" : string.Empty)}",
                true);
        embed.Color = ammo.ArmorPenetrationColor;
        return embed;
    }

    public static MessageContent? BuildAmmoMessageContent(this ItemInfos item)
    {
        Embed? embed = item.BuildAmmoEmbed();
        if (embed == null)
            return default;
        var messageContent = new MessageContent
        {
                Embeds = new Collection<Embed>() { embed }
        };
        return messageContent;
    }
}