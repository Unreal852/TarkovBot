using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovRatBot.Tarkov;

public class PriceRequirement
{
    [JsonPropertyName("type")]  public string Type  { get; set; }
    [JsonPropertyName("value")] public int?   Value { get; set; }
}