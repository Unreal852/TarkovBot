using Discord;
using TarkovRatBot.Core.Extensions;
using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Core.TarkovData.Ammos;
using TarkovRatBot.Core.TarkovData.Items;

namespace TarkovRatBot.Discord.Extensions;

public static class AmmoExtensions
{
    public static Embed BuildAmmoEmbed(this Ammo ammoInfo)
    {
        Item ammoItem = ammoInfo.Item.Get();
        var embedBuilder = new EmbedBuilder
        {
                Title = $"{ammoItem.Name} ({ammoItem.ShortName})",
                Url = ammoItem.WikiLink,
                ThumbnailUrl = ammoItem.ImageLink,
                Footer = new EmbedFooterBuilder { Text = "Last Updated" },
                Timestamp = ammoItem.Updated,
                Author = new EmbedAuthorBuilder { Name = "Provided by tarkov.dev", Url = "https://tarkov.dev/" },
                Fields = new List<EmbedFieldBuilder>(),
                Color = ammoInfo.GetPenetrationClassColor()
        };

        embedBuilder.AddField("Damages (Flesh)", ammoInfo.Damages , true);
        embedBuilder.AddField("Damages (Armor)", ammoInfo.ArmorDamages, true);
        embedBuilder.AddField("Velocity ", $"{ammoInfo.InitialSpeed} m/s", true);
        embedBuilder.AddField("Penetration Power", ammoInfo.PenetrationPower, true);
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