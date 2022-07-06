using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ArmorMaterial
{
    [JsonPropertyName("id")]                       public string? Id                       { get; set; }
    [JsonPropertyName("name")]                     public string? Name                     { get; set; }
    [JsonPropertyName("destructibility")]          public float?  Destructibility          { get; set; }
    [JsonPropertyName("minRepairDegradation")]     public float?  MinRepairDegradation     { get; set; }
    [JsonPropertyName("maxRepairDegradation")]     public float?  MaxRepairDegradation     { get; set; }
    [JsonPropertyName("explosionDestructibility")] public float?  ExplosionDestructibility { get; set; }
    [JsonPropertyName("minRepairKitDegradation")]  public float?  MinRepairKitDegradation  { get; set; }
    [JsonPropertyName("maxRepairKitDegradation")]  public float?  MaxRepairKitDegradation  { get; set; }
}