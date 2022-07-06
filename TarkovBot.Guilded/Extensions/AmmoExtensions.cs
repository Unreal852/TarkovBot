using System.Drawing;
using Guilded.Base.Embeds;
using TarkovBot.Core.Extensions;
using TarkovBot.Core.TarkovData;
using TarkovBot.Core.TarkovData.Ammos;
using TarkovBot.Core.TarkovData.Items;

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
                Color = ammoInfo.GetPenetrationClassColor()
        };

        embed.AddField("Damages (Flesh)", ammoInfo.Damages, true);
        embed.AddField("Damages (Armor)", ammoInfo.ArmorDamages, true);
        embed.AddField("Velocity ", $"{ammoInfo.InitialSpeed} m/s", true);
        embed.AddField("Penetration Power", ammoInfo.PenetrationPower, true);
        embed.AddField("Frag Chances", (int?)(ammoInfo.FragmentationChance * 100) ?? 0, true);
        if (ammoInfo.LightBleedModifier is > 0)
            embed.AddField("Light Bleed Chances", (int?)(ammoInfo.LightBleedModifier * 100) ?? 0, true);
        if (ammoInfo.HeavyBleedModifier is > 0)
            embed.AddField("Heavy Bleed Chances", (int?)(ammoInfo.HeavyBleedModifier * 100) ?? 0, true);

        embed.AddField("Armor Class Real (Effective)",
                $"{ammoInfo.RealArmorClassPen} {(ammoInfo.RealArmorClassPen != ammoInfo.EffectiveArmorClassPen ? $"({ammoInfo.EffectiveArmorClassPen})" : string.Empty)}",
                true);
        return embed;
    }

    public static Color GetPenetrationClassColor(this Ammo ammo)
    {
        return ammo.EffectiveArmorClassPen switch
        {
                >= 6 => Color.Red,
                >= 5 => Color.Orange,
                >= 4 => Color.Purple,
                >= 3 => Color.Blue,
                >= 2 => Color.Green,
                _    => Color.LightGray
        };
    }
}