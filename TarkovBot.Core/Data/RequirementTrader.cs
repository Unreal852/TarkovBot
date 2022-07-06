using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class RequirementTrader
{
    [JsonPropertyName("id")]     public string? Id     { get; set; }
    [JsonPropertyName("trader")] public Trader  Trader { get; set; }
    [JsonPropertyName("level")]  public int     Level  { get; set; }
}