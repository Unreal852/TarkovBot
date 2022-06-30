using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovRatBot.Core.TarkovData;

public class RequirementHideoutStationLevel
{
    [JsonPropertyName("id")]      public string? Id             { get; set; }
    [JsonPropertyName("level")]   public int     Level          { get; set; }
    [JsonPropertyName("station")] public IdOnly  HideoutStation { get; set; }
}