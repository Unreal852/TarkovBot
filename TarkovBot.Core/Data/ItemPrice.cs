using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPrice
{
    [JsonPropertyName("vendor")]       public Vendor  Vendor       { get; set; }
    [JsonPropertyName("price")]        public int?    Price        { get; set; }
    [JsonPropertyName("priceRUB")]     public int?    PriceRUB     { get; set; }
    [JsonPropertyName("currency")]     public string? Currency     { get; set; }
    [JsonPropertyName("currencyItem")] public IdOnly? CurrencyItem { get; set; }
}