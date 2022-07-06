using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesPainkiller
{
    [JsonPropertyName("uses")]               public int?      Uses               { get; set; }
    [JsonPropertyName("useTime")]            public int?      UseTime            { get; set; }
    [JsonPropertyName("cures")]              public string[]? Cures              { get; set; }
    [JsonPropertyName("painkillerDuration")] public int?      PainkillerDuration { get; set; }
    [JsonPropertyName("energyImpact")]       public int?      EnergyImpact       { get; set; }
    [JsonPropertyName("hydrationImpact")]    public int?      HydrationImpact    { get; set; }
}