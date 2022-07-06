using TarkovBot.Core.TarkovData;
using TarkovBot.Core.Extensions;
using TarkovBot.Core.TarkovData.Ammos;

namespace TarkovBot.Core.Caches;

public class HideoutStationsCache : TarkovCache<string, HideoutStation>
{
    public override async Task<bool> UpdateCache()
    {
        TarkovCore.WriteLine("[CACHE] Caching hideout stations...", ConsoleColor.Yellow);
        HideoutStation[]? hideouts = await TarkovCore.HideoutStationQuery.ExecuteAs<HideoutStation[]>("lang: en");
        if (hideouts == null || hideouts.Length == 0)
        {
            TarkovCore.WriteLine("[CACHE] Failed to cache hideout stations !", ConsoleColor.Red);
            return false;
        }

        Cache.Clear();
        foreach (HideoutStation hideoutStation in hideouts)
            Cache.TryAdd(hideoutStation.Id, hideoutStation);

        TarkovCore.WriteLine($"[CACHE] Successfully cached {Count} hideout stations !", ConsoleColor.Green);
        return true;
    }
}