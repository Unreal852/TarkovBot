using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class RequirementHideoutStationLevel
{
    [JsonPropertyName("id")]      public string? Id      { get; set; }
    [JsonPropertyName("station")] public IdOnly  Station { get; set; }
    [JsonPropertyName("level")]   public int     Level   { get; set; }
}