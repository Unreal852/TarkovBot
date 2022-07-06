using TarkovBot.Core.TarkovData.Ammos;
using TarkovBot.Core.Extensions;
using TarkovBot.Core.TarkovData;

namespace TarkovBot.Core.Caches;

public class AmmoCache : TarkovCache<string, Ammo>
{
    public override async Task<bool> UpdateCache()
    {
        TarkovCore.WriteLine("[CACHE] Caching ammos...", ConsoleColor.Yellow);
        Ammo[]? ammoInfos = await TarkovCore.AmmoQuery.ExecuteAs<Ammo[]>("lang: en");
        if (ammoInfos == null || ammoInfos.Length == 0)
        {
            TarkovCore.WriteLine("[CACHE] Failed to cache ammos !", ConsoleColor.Red);
            return false;
        }

        Cache.Clear();
        foreach (Ammo ammoInfo in ammoInfos)
        {
            // Cache armor class penetration
            (int Real, int Effective) armorClass = ammoInfo.GetArmorClass();
            ammoInfo.RealArmorClassPen = armorClass.Real;
            ammoInfo.EffectiveArmorClassPen = armorClass.Effective;
            Cache.TryAdd(ammoInfo.Item.Id, ammoInfo);
        }

        TarkovCore.WriteLine($"[CACHE] Successfully cached {Count} ammos !", ConsoleColor.Green);
        return true;
    }
}