using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers;

public class AmmoProvider : DataProvider<string, Ammo>
{
    public AmmoProvider() : base(GraphQlQueryBuilder.BuildQuery<Ammo>()!)
    {
    }

    public override async Task<bool> UpdateCache()
    {
        TarkovCore.WriteLine("[CACHE] Caching ammos...", ConsoleColor.Yellow);
        Ammo[]? ammoInfos = await Query.ExecuteAs<Ammo[]>("lang: en");
        if (ammoInfos == null || ammoInfos.Length == 0)
        {
            TarkovCore.WriteLine("[CACHE] Failed to cache ammos !", ConsoleColor.Red);
            return false;
        }

        Cache.Clear();
        foreach (Ammo ammoInfo in ammoInfos)
        {
            Cache.TryAdd(ammoInfo.Item.Id, ammoInfo);
        }

        TarkovCore.WriteLine($"[CACHE] Successfully cached {Count} ammos !", ConsoleColor.Green);
        return true;
    }
}