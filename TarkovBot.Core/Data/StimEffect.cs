using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class StimEffect
{
    [JsonPropertyName("type")]      public string  Type      { get; set; }
    [JsonPropertyName("chance")]    public float   Chance    { get; set; }
    [JsonPropertyName("delay")]     public int     Delay     { get; set; }
    [JsonPropertyName("duration")]  public int     Duration  { get; set; }
    [JsonPropertyName("value")]     public float   Value     { get; set; }
    [JsonPropertyName("percent")]   public bool    Percent   { get; set; }
    [JsonPropertyName("skillName")] public string? SkillName { get; set; }
}