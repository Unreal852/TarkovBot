using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovBot.Core.TarkovData;

public class PriceRequirement
{
    [JsonPropertyName("type")] public RequirementType RequirementType { get; set; }
    [JsonPropertyName("value")] public int?             Value           { get; set; }
}