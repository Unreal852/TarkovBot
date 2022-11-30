using Serilog;
using TarkovBot.Data;
using TarkovBot.Services.Abstractions;
using TarkovBot.Utils;

namespace TarkovBot.Services;

public class TarkovItemsProviderService : IItemsProvider
{
    private readonly ILogger                        _logger;
    private readonly IGraphQlClientService          _graphQlClientService;
    private readonly BackgroundTimer                _timer;
    private          Dictionary<string, TarkovItem> _items = new();

    public TarkovItemsProviderService(ILogger logger, IGraphQlClientService graphQlClientService)
    {
        _logger = logger;
        _graphQlClientService = graphQlClientService;
        _timer = new BackgroundTimer(TimeSpan.FromHours(1), UpdateItems);
        _timer.Start(true);
    }

    public bool IsUpdating { get; private set; }

    public async Task UpdateItems()
    {
        IsUpdating = true;
        var itemsResponse = await _graphQlClientService.RequestAs<ItemsResponseData>(
                "items{id,name,description,wikiLink,inspectImageLink,types,avg24hPrice,width,height}");
        if (itemsResponse == null)
            return;
        _items.Clear();
        _items = itemsResponse.Items.ToDictionary(item => item.Id);

        var ammos = await _graphQlClientService.RequestAs<AmmosResponseData>(
                "ammo{item{id},damage,armorDamage,penetrationPower,penetrationChance}");
        if (ammos != null)
        {
            foreach (var ammo in ammos.Ammo)
                _items[ammo.Item.Id].Ammo = ammo;
        }

        _logger.Information("Updated {ItemsCount} items and {AmmosCount} ammos", _items.Count, ammos?.Ammo.Length ?? 0);
        IsUpdating = false;
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