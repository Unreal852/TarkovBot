using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class FleaMarketReputationLevel
{
    [JsonPropertyName("offers")] public int   Offers { get; set; }
    [JsonPropertyName("minRep")] public float MinRep { get; set; }
    [JsonPropertyName("maxRep")] public float MaxRep { get; set; }
}