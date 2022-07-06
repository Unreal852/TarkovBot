using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesStim
{
    [JsonPropertyName("useTime")]     public int?         UseTime     { get; set; }
    [JsonPropertyName("cures")]       public string[]?    Cures       { get; set; }
    [JsonPropertyName("stimEffects")] public StimEffect[] StimEffects { get; set; }
}