namespace TarkovBot.Core.Data.Infos;

public class ItemInfos
{
    public string     Id              { get; init; }
    public string     Name            { get; init; }
    public string     NormalizedName  { get; init; }
    public int        Width           { get; init; }
    public int        Height          { get; init; }
    public int        TotalSlots      { get; init; }
    public int        LowestPriceRub  { get; init; }
    public int        PricePerSlotRub { get; init; }
    public ItemPrice? BestBuyFor      { get; init; }
    public ItemPrice? BestSellFor     { get; init; }
}