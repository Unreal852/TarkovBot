using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers;

public class CraftsProvider : DataProvider<string, Craft>
{
    public CraftsProvider() : base(GraphQlQueryBuilder.BuildQuery<Craft>()!)
    {
    }

    public override async Task<bool> UpdateCache()
    {
        TarkovCore.WriteLine("[CACHE] Caching crafts...", ConsoleColor.Yellow);
        Craft[]? crafts = await Query.ExecuteAs<Craft[]>("lang: en");
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