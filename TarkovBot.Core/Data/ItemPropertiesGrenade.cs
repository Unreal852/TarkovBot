using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesGrenade
{
    [JsonPropertyName("type")]                 public string? Type                 { get; set; }
    [JsonPropertyName("fuse")]                 public float?  Fuse                 { get; set; }
    [JsonPropertyName("minExplosionDistance")] public int?    MinExplosionDistance { get; set; }
    [JsonPropertyName("maxExplosionDistance")] public int?    MaxExplosionDistance { get; set; }
    [JsonPropertyName("fragments")]            public int?    Fragments            { get; set; }
    [JsonPropertyName("contusionRadius")]      public int?    ContusionRadius      { get; set; }
}