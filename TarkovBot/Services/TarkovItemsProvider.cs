using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Serilog;
using TarkovBot.Data;
using TarkovBot.Services.Abstractions;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace TarkovBot.Services;

public class TarkovItemsProvider : IItemsProvider
{
    private readonly IGraphQlClientService          _graphQlClientService;
    private readonly Dictionary<string, TarkovItem> _items = new();

    public TarkovItemsProvider(IGraphQlClientService graphQlClientService)
    {
        _graphQlClientService = graphQlClientService;
    }

    public async Task Initialize()
    {
        var items = await _graphQlClientService.RequestAs<TarkovItem[]>(
                "items{id,name,description,wikiLink,inspectImageLink,types,avg24hPrice,width,height}");
        if (items == null)
            return;
        foreach (var item in items)
        {
            if (item.Name == null)
                continue;
            _items.Add(item.Id, item);
        }

        Log.Information("Added {ItemsCount} items", _items.Count);

        //Log.Information("Root: {Data}", itemsRoot);
        return;
    }

    public IEnumerable<TarkovItem> FindByName(string itemName, int maxItems = 10)
    {
        var found = 0;
        foreach (var item in _items.Values)
        {
            if (found > maxItems)
                break;
            if (item.Name != null &&
                item.Name.Contains(itemName, StringComparison.InvariantCultureIgnoreCase))
            {
                found++;
                yield return item;
            }
        }
    }

    public TarkovItem? FindById(string itemId)
    {
        return _items.TryGetValue(itemId, out var item) ? item : default;
    }
}