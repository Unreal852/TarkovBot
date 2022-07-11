using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers.Implementations;

public class HideoutStationsProvider : DataProvider<string, HideoutStation>
{
    public HideoutStationsProvider() : base(GraphQlQueryBuilder.BuildQuery<HideoutStation>()!)
    {
    }

    public override async Task<bool> UpdateCache()
    {
        TarkovCore.WriteLine("[CACHE] Caching hideout stations...", ConsoleColor.Yellow);
        HideoutStation[]? hideouts = await Query.ExecuteAs<HideoutStation[]>("lang: en");
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