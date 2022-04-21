using System.Collections.Concurrent;
using TarkovRatBot.Extensions;

namespace TarkovRatBot.Tarkov;

public static class TarkovCache
{
    public static ConcurrentDictionary<string, AmmoInfo> AmmoCache { get; } = new();

    public static async Task<bool> CacheAmmoInfos()
    {
        AmmoInfo[] ammoInfos = await Queries.AmmoQuery.ExecuteAs<AmmoInfo[]>();
        if (ammoInfos == null || ammoInfos.Length == 0)
            return false;
        AmmoCache.Clear();
        foreach (AmmoInfo ammoInfo in ammoInfos)
        {
            // Cache armor class penetration
            (int Real, int Effective) armorClass = ammoInfo.GetArmorClass();
            ammoInfo.RealArmorClassPen = armorClass.Real;
            ammoInfo.EffectiveArmorClassPen = armorClass.Effective;
            AmmoCache.TryAdd(ammoInfo.Item.Name, ammoInfo);
        }

        return true;
    }
}