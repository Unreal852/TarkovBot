using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovRatBot.Core.TarkovData;

public class ItemPrice
{
    public int?    Price        { get; set; }
    public int?    PriceRUB     { get; set; }
    public string? Currency     { get; set; }
    public IdOnly? CurrencyItem { get; set; }
    public Vendor  Vendor       { get; set; }
}