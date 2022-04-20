using System.Collections.Concurrent;

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
            AmmoCache.TryAdd(ammoInfo.Item.Name, ammoInfo);
    
        return true;
    }
}