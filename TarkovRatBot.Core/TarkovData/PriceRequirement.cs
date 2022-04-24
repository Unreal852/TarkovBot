using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovRatBot.Core.TarkovData;

public class PriceRequirement
{
    [JsonPropertyName("type")]  public ERequirementType RequirementType { get; set; }
    [JsonPropertyName("value")] public int?             Value           { get; set; }
}