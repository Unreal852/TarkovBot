using Guilded.Base.Embeds;
using TarkovBot.Data;

namespace TarkovBot.Embeds;

public class TarkovItemEmbed : Embed
{
    public TarkovItemEmbed(TarkovItem item)
    {
        Item = item;
        Title = item.Name;
        Footer = new EmbedFooter(item.Id);
        Author = new EmbedAuthor("Data provided by tarkov.dev", "https://tarkov.dev/");
        if (item.InspectImageLink != null)
            Image = new EmbedMedia(item.InspectImageLink);
        if (item.WikiLink != null)
            Url = new Uri(item.WikiLink);
        AddField("Price", item.Avg24HPrice, true);
        AddField("Price per slot",
                item.Slots > 1
                        ? $"{item.PricePerSlots}\n_({item.Slots} slots)_"
                        : $"{item.PricePerSlots}\n_({item.Slots} slot)_", true);
        if (item.Ammo != null)
        {
            AddField("Damages", item.Ammo.Damage, true);
            AddField("Armor Damages", item.Ammo.ArmorDamage, true);
            AddField("Penetration Power", item.Ammo.PenetrationPower, true);
            AddField("Penetration Chance", item.Ammo.PenetrationChance, true);
        }
    }

    public TarkovItem Item { get; }
}