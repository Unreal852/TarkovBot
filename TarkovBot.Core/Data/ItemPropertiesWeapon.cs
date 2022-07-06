using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesWeapon
{
    [JsonPropertyName("caliber")]           public string?   Caliber           { get; set; }
    [JsonPropertyName("defaultAmmo")]       public Item?     DefaultAmmo       { get; set; }
    [JsonPropertyName("effectiveDistance")] public int?      EffectiveDistance { get; set; }
    [JsonPropertyName("ergonomics")]        public float?    Ergonomics        { get; set; }
    [JsonPropertyName("fireModes")]         public string[]? FireModes         { get; set; }
    [JsonPropertyName("fireRate")]          public int?      FireRate          { get; set; }
    [JsonPropertyName("maxDurability")]     public int?      MaxDurability     { get; set; }
    [JsonPropertyName("recoilVertical")]    public int?      RecoilVertical    { get; set; }
    [JsonPropertyName("recoilHorizontal")]  public int?      RecoilHorizontal  { get; set; }
    [JsonPropertyName("repairCost")]        public int?      RepairCost        { get; set; }
    [JsonPropertyName("sightingRange")]     public int?      SightingRange     { get; set; }
}