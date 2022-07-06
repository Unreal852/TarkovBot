using TarkovBot.Core.TarkovData.Crafts;
using TarkovBot.Core.TarkovData;

namespace TarkovBot.Core.Caches;

public class CraftsCache : TarkovCache<string, Craft>
{
    public override async Task<bool> UpdateCache()
    {
        TarkovCore.WriteLine("[CACHE] Caching crafts...", ConsoleColor.Yellow);
        Craft[]? crafts = await TarkovCore.CraftsQuery.ExecuteAs<Craft[]>("lang: en");
        if (crafts == null || crafts.Length == 0)
        {
            TarkovCore.WriteLine("[CACHE] Failed to cache crafts !", ConsoleColor.Red);
            return false;
        }

        Cache.Clear();
        foreach (Craft craft in crafts)
            Cache.TryAdd(craft.Id, craft);

        TarkovCore.WriteLine($"[CACHE] Successfully cached {Count} crafts !", ConsoleColor.Green);
        return true;
    }
}