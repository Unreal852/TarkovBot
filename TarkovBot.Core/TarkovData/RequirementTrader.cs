using System.Text.Json.Serialization;

namespace TarkovBot.Core.TarkovData;

public class RequirementTrader
{
    [JsonPropertyName("id")]     public string? Id     { get; set; }
    [JsonPropertyName("level")]  public int     Level  { get; set; }
    [JsonPropertyName("trader")] public IdOnly  Trader { get; set; }
}