using Discord;
using TarkovRatBot.Core.TarkovData.Ammos;

namespace TarkovRatBot.Discord.Extensions;

public static class AmmoExtensions
{
    public static Embed BuildAmmoEmbed(this Ammo ammoInfo)
    {
        var embedBuilder = new EmbedBuilder
        {
                Title = $"{ammoInfo.Item.Name} ({ammoInfo.Item.ShortName})",
                Url = ammoInfo.Item.WikiLink,
                ThumbnailUrl = ammoInfo.Item.ImageLink,
                Footer = new EmbedFooterBuilder { Text = "Last Updated" },
                Timestamp = ammoInfo.Item.Updated,
                Author = new EmbedAuthorBuilder { Name = "Provided by tarkov.dev", Url = "https://tarkov.dev/" },
                Fields = new List<EmbedFieldBuilder>(),
                Color = ammoInfo.GetPenetrationClassColor()
        };

        embedBuilder.AddField("Damages (Flesh)", ammoInfo.Damage      ?? 0, true);
        embedBuilder.AddField("Damages (Armor)", ammoInfo.ArmorDamage ?? 0, true);
        embedBuilder.AddField("Velocity ", $"{ammoInfo.InitialSpeed ?? 0} m/s", true);
        embedBuilder.AddField("Penetration Power", ammoInfo.PenetrationPower             ?? 0, true);
        embedBuilder.AddField("Frag Chances", (int?)(ammoInfo.FragmentationChance * 100) ?? 0, true);
        if (ammoInfo.LightBleedModifier is > 0)
            embedBuilder.AddField("Light Bleed Chances", (int?)(ammoInfo.LightBleedModifier * 100) ?? 0, true);
        if (ammoInfo.HeavyBleedModifier is > 0)
            embedBuilder.AddField("Heavy Bleed Chances", (int?)(ammoInfo.HeavyBleedModifier * 100) ?? 0, true);

        embedBuilder.AddField("Armor Class Real (Effective)",
                $"{ammoInfo.RealArmorClassPen} {(ammoInfo.RealArmorClassPen != ammoInfo.EffectiveArmorClassPen ? $"({ammoInfo.EffectiveArmorClassPen})" : string.Empty)}",
                true);
        return embedBuilder.Build();
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
                _    => Color.LightGrey
        };
    }
}