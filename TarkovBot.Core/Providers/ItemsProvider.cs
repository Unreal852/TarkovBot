using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers;

public class ItemsProvider : DataProvider<string, Item>
{
    public ItemsProvider() : base(GraphQlQueryBuilder.BuildQuery<Item>()!)
    {
    }

    public override async Task<bool> UpdateCache()
    {
        return true; // Ignored
    }

    public async Task<Item?> QueryById(string id, LanguageCode lang)
    {
        if (Cache.TryGetValue(id, out Item? item))
            return item;
        Item[]? items = await Query.ExecuteAs<Item[]>($"lang: {lang.ToString()}, ids: [\"{id}\"]");
        if (items is not { Length: > 0 })
            return default;
        Cache.TryAdd(id, items[0]);
        return items[0];
    }

    public Task<Item[]?> QueryByName(string itemName, LanguageCode lang)
    {
        return Query.ExecuteAs<Item[]>($"lang: {lang.ToString()}, names: [\"{itemName}\"]");
    }
}