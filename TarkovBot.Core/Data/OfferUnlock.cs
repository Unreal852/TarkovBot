using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class OfferUnlock
{
    [JsonPropertyName("id")]     public string Id     { get; set; }
    [JsonPropertyName("trader")] public Trader Trader { get; set; }
    [JsonPropertyName("level")]  public int    Level  { get; set; }
    [JsonPropertyName("item")]   public Item   Item   { get; set; }
}