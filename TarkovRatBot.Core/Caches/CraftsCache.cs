using TarkovRatBot.Core.Extensions;
using TarkovRatBot.Core.TarkovData.Ammos;
using TarkovRatBot.Core.TarkovData.Crafts;

namespace TarkovRatBot.Core.Caches;

public class CraftsCache : TarkovCache<string, Craft>
{
    public override async Task<bool> UpdateCache()
    {
        WriteLine("[CACHE] Caching crafts...", ConsoleColor.Yellow);
        Craft[] crafts = await CraftQuery.ExecuteAs<Craft[]>();
        if (crafts == null || crafts.Length == 0)
        {
            WriteLine("[CACHE] Failed to cache crafts...", ConsoleColor.Red);
            return false;
        }

        Cache.Clear();
        foreach (Craft craft in crafts)
        {
            Cache.TryAdd(craft.Id, craft);
        }

        WriteLine($"[CACHE] Successfully cached {Count} crafts !", ConsoleColor.Green);
        return true;
    }
}