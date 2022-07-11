using System.Collections.Concurrent;
using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers.Implementations;

public class ItemsProvider : LocalizedDataProvider<string, Item>
{
    public ItemsProvider() : base(GraphQlQueryBuilder.BuildQuery<Item>()!)
    {
    }

    public override async Task<bool> UpdateCache()
    {
        await UpdateCache(LanguageCode.en);
        await UpdateCache(LanguageCode.fr);
        return true;
    }

    public override async Task<bool> UpdateCache(LanguageCode lang)
    {
        TarkovCore.WriteLine($"[CACHE] Caching localized ({lang.ToString().ToUpper()}) items...", ConsoleColor.Yellow);
        Item[]? items = await QueryAll(lang);
        if (items == null || items.Length == 0)
        {
            TarkovCore.WriteLine("[CACHE] Failed to cache items !", ConsoleColor.Red);
            return false;
        }

        if (!Cache.TryGetValue(lang, out ConcurrentDictionary<string, Item>? innerCache))
            Cache.TryAdd(lang, innerCache ??= new());

        innerCache.Clear();
        foreach (Item item in items)
            innerCache.TryAdd(item.Id, item);

        TarkovCore.WriteLine($"[CACHE] Successfully cached {innerCache.Count} ({lang.ToString().ToUpper()}) items !", ConsoleColor.Green);
        return true;
    }

    public Item? QueryById(LanguageCode lang, string id)
    {
        if (!Cache.TryGetValue(lang, out ConcurrentDictionary<string, Item>? innerCache) ||
            !innerCache.TryGetValue(id, out Item? item))
            return default;
        return item;
    }

    private Task<Item[]?> QueryAll(LanguageCode lang)
    {
        return Query.ExecuteAs<Item[]>($"lang: {lang}");
    }
}