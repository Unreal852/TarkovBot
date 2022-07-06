using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesSurgicalKit
{
    [JsonPropertyName("uses")]          public int?      Uses          { get; set; }
    [JsonPropertyName("useTime")]       public int?      UseTime       { get; set; }
    [JsonPropertyName("cures")]         public string[]? Cures         { get; set; }
    [JsonPropertyName("minLimbHealth")] public float?    MinLimbHealth { get; set; }
    [JsonPropertyName("maxLimbHealth")] public float?    MaxLimbHealth { get; set; }
}