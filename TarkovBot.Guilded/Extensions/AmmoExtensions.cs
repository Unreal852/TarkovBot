using Guilded.Base.Embeds;
using TarkovBot.Core.Data;
using TarkovBot.Core.Extensions;

namespace TarkovBot.Guilded.Extensions;

public static class AmmoExtensions
{
    public static Embed BuildAmmoEmbed(this Ammo ammoInfo, Item ammoItem)
    {
        var embed = new Embed
        {
                Title = $"{ammoItem.Name} ({ammoItem.ShortName})",
                Url = new Uri(ammoItem.WikiLink                   ?? ""),
                Thumbnail = new EmbedMedia(ammoItem.GridImageLink ?? ""),
                Footer = new EmbedFooter("Last Updated"),
                Timestamp = ammoItem.Updated,
                Author = new EmbedAuthor("Provided by tarkov.dev", "https://tarkov.dev/"),
                Fields = new List<EmbedField>(),
        };

        embed.AddField("Damages (Flesh)", ammoInfo.Damage, true);
        embed.AddField("Damages (Armor)", ammoInfo.ArmorDamage, true);
        embed.AddField("Velocity ", $"{ammoInfo.InitialSpeed} m/s", true);
        embed.AddField("Penetration Power", ammoInfo.PenetrationPower, true);
        embed.AddField("Frag Chances", (int?)(ammoInfo.FragmentationChance * 100) ?? 0, true);
        if (ammoInfo.LightBleedModifier is > 0)
            embed.AddField("Light Bleed Chances", (int?)(ammoInfo.LightBleedModifier * 100) ?? 0, true);
        if (ammoInfo.HeavyBleedModifier is > 0)
            embed.AddField("Heavy Bleed Chances", (int?)(ammoInfo.HeavyBleedModifier * 100) ?? 0, true);

        (int Real, int Effective) ammoArmor = ammoInfo.GetArmorClass();
        embed.AddField("Armor Class Real (Effective)",
                $"{ammoArmor.Real} {(ammoArmor.Real != ammoArmor.Effective ? $"({ammoArmor.Effective})" : string.Empty)}",
                true);
        embed.Color = Core.Extensions.AmmoExtensions.GetPenetrationClassColor(ammoArmor.Effective);
        return embed;
    }
}