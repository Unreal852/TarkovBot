using System.Text.Json.Serialization;

namespace TarkovBot.Core.TarkovData;

public class RequirementSkill
{
    [JsonPropertyName("id")]    public string? Id    { get; set; }
    [JsonPropertyName("name")]  public string  Name  { get; set; }
    [JsonPropertyName("level")] public int     Level { get; set; }
}