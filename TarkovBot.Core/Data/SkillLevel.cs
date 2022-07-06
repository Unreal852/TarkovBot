using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class SkillLevel
{
    [JsonPropertyName("name")]  public string Name  { get; set; }
    [JsonPropertyName("level")] public float  Level { get; set; }
}