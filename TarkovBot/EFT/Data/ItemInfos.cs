using TarkovBot.EFT.Data.Provider;
using TarkovBot.EFT.Data.Raw;

namespace TarkovBot.EFT.Data;

public class ItemInfos : IIdentifiable
{
    public static ItemInfos? FromItem(Item item, LanguageCode languageCode)
    {
        if (string.IsNullOrWhiteSpace(item.Name) || string.IsNullOrWhiteSpace(item.ShortName) || string.IsNullOrWhiteSpace(item.WikiLink))
            return default;

        int lowestPriceRub = item.LastLowPrice ?? 0;

        ItemPrice? bestBuying = item.BuyFor?.Where(ip => ip.PriceRUB   > 0).MinBy(ip => ip.PriceRUB);
        ItemPrice? bestSelling = item.SellFor?.Where(ip => ip.PriceRUB > 0).MaxBy(ip => ip.PriceRUB);

        if (lowestPriceRub == 0)
        {
            if (bestBuying is { PriceRUB: > 0 } && bestBuying.PriceRUB < lowestPriceRub)
                lowestPriceRub = bestBuying.PriceRUB.Value;

            if (lowestPriceRub == 0 && bestSelling is { PriceRUB: > 0 })
                lowestPriceRub = bestSelling.PriceRUB.Value;

            if (lowestPriceRub == 0)
                lowestPriceRub = item.BasePrice;
        }

        ItemInfos itemInfos = new()
        {
                TotalSlots = item.Width * item.Height,
                LowestPriceRub = lowestPriceRub,
                PricePerSlotRub = lowestPriceRub / (item.Width * item.Height),
                BestBuyFor = bestBuying,
                BestSellFor = bestSelling,
                Item = item,
                Ammo = DataProviders.AmmoProvider.GetByKey(item.Id)
        };

        itemInfos.AddLocalizedInfos(languageCode, new LocalizedItemInfos { Name = item.Name, ShortName = item.ShortName });

        return itemInfos;
    }

    private readonly Dictionary<LanguageCode, LocalizedItemInfos> _localizedNames = new();

    public string     Id              => Item.Id;
    public string     Name            => Item.Name      ?? string.Empty;
    public string     ShortName       => Item.ShortName ?? string.Empty;
    public string     WikiLink        => Item.WikiLink  ?? string.Empty;
    public int        Width           => Item.Width;
    public int        Height          => Item.Height;
    public int        TotalSlots      { get; init; }
    public int        LowestPriceRub  { get; init; }
    public int        PricePerSlotRub { get; init; }
    public Item       Item            { get; init; }
    public AmmoInfos? Ammo            { get; init; }
    public ItemPrice? BestBuyFor      { get; init; }
    public ItemPrice? BestSellFor     { get; init; }

    public void AddLocalizedInfos(LanguageCode languageCode, LocalizedItemInfos localizedItemInfos)
    {
        _localizedNames.Add(languageCode, localizedItemInfos);
    }

    public LocalizedItemInfos GetLocalizedInfos(LanguageCode languageCode)
    {
        return _localizedNames.ContainsKey(languageCode) ? _localizedNames[languageCode] : _localizedNames[LanguageCode.en];
    }
}