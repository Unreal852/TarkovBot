using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesHelmet
{
    [JsonPropertyName("class")]        public int?           Class        { get; set; }
    [JsonPropertyName("durability")]   public int?           Durability   { get; set; }
    [JsonPropertyName("repairCost")]   public int?           RepairCost   { get; set; }
    [JsonPropertyName("speedPenalty")] public float?         SpeedPenalty { get; set; }
    [JsonPropertyName("turnPenalty")]  public float?         TurnPenalty  { get; set; }
    [JsonPropertyName("ergoPenalty")]  public int?           ErgoPenalty  { get; set; }
    [JsonPropertyName("headZones")]    public string[]?      HeadZones    { get; set; }
    [JsonPropertyName("material")]     public ArmorMaterial? Material     { get; set; }
    [JsonPropertyName("deafening")]    public string?        Deafening    { get; set; }
}