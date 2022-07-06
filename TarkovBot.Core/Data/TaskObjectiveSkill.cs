using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TaskObjectiveSkill
{
    [JsonPropertyName("id")]          public string?    Id          { get; set; }
    [JsonPropertyName("type")]        public string     Type        { get; set; }
    [JsonPropertyName("description")] public string     Description { get; set; }
    [JsonPropertyName("maps")]        public Map[]      Maps        { get; set; }
    [JsonPropertyName("optional")]    public bool       Optional    { get; set; }
    [JsonPropertyName("skillLevel")]  public SkillLevel SkillLevel  { get; set; }
}