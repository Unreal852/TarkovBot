using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesFoodDrink
{
    [JsonPropertyName("energy")]    public int? Energy    { get; set; }
    [JsonPropertyName("hydration")] public int? Hydration { get; set; }
    [JsonPropertyName("units")]     public int? Units     { get; set; }
}