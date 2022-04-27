using System.Drawing;
using Guilded.Base.Embeds;
using TarkovRatBot.Core.TarkovData.Ammos;

namespace TarkovRatBot.Guilded.Extensions;

public static class AmmoExtensions
{
    public static Embed BuildAmmoEmbed(this Ammo ammoInfo)
    {
        var embed = new Embed
        {
                Title = $"{ammoInfo.Item.Name} ({ammoInfo.Item.ShortName})",
                Url = new Uri(ammoInfo.Item.WikiLink),
                Thumbnail = new EmbedMedia(ammoInfo.Item.ImageLink),
                Footer = new EmbedFooter("Last Updated"),
                Timestamp = ammoInfo.Item.Updated,
                Author = new EmbedAuthor("Provided by tarkov.dev","https://tarkov.dev/"),
                Fields = new List<EmbedField>(),
                Color = ammoInfo.GetPenetrationClassColor()
        };

        embed.AddField("Damages (Flesh)", ammoInfo.Damage      ?? 0, true);
        embed.AddField("Damages (Armor)", ammoInfo.ArmorDamage ?? 0, true);
        embed.AddField("Velocity ", $"{ammoInfo.InitialSpeed ?? 0} m/s", true);
        embed.AddField("Penetration Power", ammoInfo.PenetrationPower             ?? 0, true);
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