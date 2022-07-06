using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class Trader
{
    [JsonPropertyName("id")]           public string            Id           { get; set; }
    [JsonPropertyName("name")]         public string            Name         { get; set; }
    [JsonPropertyName("resetTime")]    public string?           ResetTime    { get; set; }
    [JsonPropertyName("currency")]     public IdOnly            Currency     { get; set; }
    [JsonPropertyName("discount")]     public float             Discount     { get; set; }
    [JsonPropertyName("levels")]       public TraderLevel[]     Levels       { get; set; }
    [JsonPropertyName("barters")]      public Barter[]          Barters      { get; set; }
    [JsonPropertyName("cashOffers")]   public TraderCashOffer[] CashOffers   { get; set; }
    [JsonPropertyName("tarkovDataId")] public int?              TarkovDataId { get; set; }
}