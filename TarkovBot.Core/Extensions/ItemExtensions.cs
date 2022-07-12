using TarkovBot.Core.Data;

namespace TarkovBot.Core.Extensions;

public static class ItemExtensions
{
    public static bool IsAmmo(this Item item)
    {
        return item.Types.Contains(ItemType.Ammo) && TarkovCore.AmmoProvider.Cache.TryGetValue(item.Id, out _);
    }

    public static bool IsAmmo(this Item item, out Ammo? ammo)
    {
        bool tryGet = TarkovCore.AmmoProvider.Cache.TryGetValue(item.Id, out ammo);
        return item.Types.Contains(ItemType.Ammo) && tryGet;
    }
}