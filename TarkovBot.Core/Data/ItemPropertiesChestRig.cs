using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesChestRig
{
    [JsonPropertyName("class")]        public int?               Class        { get; set; }
    [JsonPropertyName("durability")]   public int?               Durability   { get; set; }
    [JsonPropertyName("repairCost")]   public int?               RepairCost   { get; set; }
    [JsonPropertyName("speedPenalty")] public float?             SpeedPenalty { get; set; }
    [JsonPropertyName("turnPenalty")]  public float?             TurnPenalty  { get; set; }
    [JsonPropertyName("ergoPenalty")]  public int?               ErgoPenalty  { get; set; }
    [JsonPropertyName("zones")]        public string[]?          Zones        { get; set; }
    [JsonPropertyName("material")]     public ArmorMaterial?     Material     { get; set; }
    [JsonPropertyName("capacity")]     public int?               Capacity     { get; set; }
    [JsonPropertyName("pouches")]      public ItemStorageGrid[]? Pouches      { get; set; }
}