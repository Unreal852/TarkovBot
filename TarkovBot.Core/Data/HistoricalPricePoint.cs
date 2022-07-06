using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class HistoricalPricePoint
{
    [JsonPropertyName("price")]     public int?    Price     { get; set; }
    [JsonPropertyName("timestamp")] public string? Timestamp { get; set; }
}