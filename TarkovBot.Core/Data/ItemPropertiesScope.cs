using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesScope
{
    [JsonPropertyName("ergonomics")] public float?   Ergonomics { get; set; }
    [JsonPropertyName("recoil")]     public float?   Recoil     { get; set; }
    [JsonPropertyName("zoomLevels")] public float[]? ZoomLevels { get; set; }
}