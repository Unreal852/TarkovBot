using Serilog;
using TarkovBot.EFT.Data.Raw;
using TarkovBot.GraphQL;
using Task = System.Threading.Tasks.Task;

namespace TarkovBot.EFT.Data.Provider.Implementations;

public class ItemsProvider : DataProvider<string, ItemInfos>
{
    public ItemsProvider() : base(GraphQlQueryBuilder.FromType<Item>()!)
    {
    }

    private GraphQlQuery ItemNamesQuery { get; } = GraphQlQueryBuilder.FromResource("items_names.ql", "items")!;

    public override async Task<int> UpdateCache()
    {
        LanguageCode lang = LanguageCode.en;
        Item[]? elements = await Query.ExecuteAs<Item[]>($"lang: {lang.ToString().ToLower()}").ConfigureAwait(false);
        if (elements == null || elements.Length == 0)
        {
            Log.Error("Failed to cache {Type}, the results returned where null or empty", nameof(Item));
            return 0;
        }

        Cache.Clear();
        foreach (Item element in elements)
        {
            ItemInfos? item = ItemInfos.FromItem(element, lang);
            if (item == null)
                continue;
            Cache.TryAdd(element.Id, item);
        }

        await UpdateCache(LanguageCode.fr).ConfigureAwait(false);

        return Cache.Count;
    }

    public async Task UpdateCache(LanguageCode lang)
    {
        LocalizedItemInfos[]? localizedNames = await ItemNamesQuery.ExecuteAs<LocalizedItemInfos[]>($"lang: {lang.ToString().ToLower()}").ConfigureAwait(false);
        if (localizedNames == null || localizedNames.Length == 0)
        {
            Log.Error("Failed to cache localized item names, the results returned where null or empty");
            return;
        }

        foreach (LocalizedItemInfos itemName in localizedNames)
        {
            if (Cache.TryGetValue(itemName.Id, out ItemInfos? item))
                item.AddLocalizedInfos(lang, itemName);
        }
    }

    public IEnumerable<ItemInfos> FindDefaultName(LanguageCode lang, string name)
    {
        foreach (var entry in Cache)
        {
            if (entry.Value.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                yield return entry.Value;
        }
    }

    public IEnumerable<ItemInfos> FindByLocalizedName(LanguageCode lang, string name)
    {
        foreach (var entry in Cache)
        {
            if (entry.Value.GetLocalizedInfos(lang).Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                yield return entry.Value;
        }
    }
}