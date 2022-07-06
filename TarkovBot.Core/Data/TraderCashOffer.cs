using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TraderCashOffer
{
    [JsonPropertyName("item")]           public Item    Item           { get; set; }
    [JsonPropertyName("minTraderLevel")] public int?    MinTraderLevel { get; set; }
    [JsonPropertyName("price")]          public int?    Price          { get; set; }
    [JsonPropertyName("currency")]       public string? Currency       { get; set; }
    [JsonPropertyName("currencyItem")]   public Item?   CurrencyItem   { get; set; }
    [JsonPropertyName("priceRUB")]       public int?    PriceRUB       { get; set; }
    [JsonPropertyName("taskUnlock")]     public Task?   TaskUnlock     { get; set; }
}