using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesMedKit
{
    [JsonPropertyName("hitpoints")]           public int?      Hitpoints           { get; set; }
    [JsonPropertyName("useTime")]             public int?      UseTime             { get; set; }
    [JsonPropertyName("maxHealPerUse")]       public int?      MaxHealPerUse       { get; set; }
    [JsonPropertyName("cures")]               public string[]? Cures               { get; set; }
    [JsonPropertyName("hpCostLightBleeding")] public int?      HpCostLightBleeding { get; set; }
    [JsonPropertyName("hpCostHeavyBleeding")] public int?      HpCostHeavyBleeding { get; set; }
}