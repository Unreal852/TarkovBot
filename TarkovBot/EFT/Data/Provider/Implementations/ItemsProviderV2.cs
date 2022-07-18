using System.Collections.Concurrent;
using Serilog;
using TarkovBot.EFT.Data.Raw;
using TarkovBot.GraphQL;

namespace TarkovBot.EFT.Data.Provider.Implementations;

public class ItemsProviderV2 : LocalizedDataProvider<ItemInfos>
{
    public ItemsProviderV2() : base(GraphQlQueryBuilder.FromType<Item>()!)
    {
    }

    public override async Task<int> UpdateCache(LanguageCode lang)
    {
        Item[]? elements = await Query.ExecuteAs<Item[]>($"lang: {lang.ToString().ToLower()}");
        if (elements == null || elements.Length == 0)
        {
            Log.Error("Failed to cache {Type}, the results returned where null or empty", nameof(Item));
            return 0;
        }

        if (!Cache.TryGetValue(lang, out ConcurrentDictionary<string, ItemInfos>? innerCache))
            Cache.TryAdd(lang, innerCache ??= new());

        innerCache.Clear();
        foreach (Item element in elements)
        {
            ItemInfos? item = ItemInfos.FromItem(element);
            if (item == null)
                continue;
            innerCache.TryAdd(element.Id, item);
        }

        return innerCache.Count;
    }
}