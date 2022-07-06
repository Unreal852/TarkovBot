using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesMedicalItem
{
    [JsonPropertyName("uses")]    public int?      Uses    { get; set; }
    [JsonPropertyName("useTime")] public int?      UseTime { get; set; }
    [JsonPropertyName("cures")]   public string[]? Cures   { get; set; }
}