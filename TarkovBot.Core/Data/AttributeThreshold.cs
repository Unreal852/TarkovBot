using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class AttributeThreshold
{
    [JsonPropertyName("name")]        public string        Name        { get; set; }
    [JsonPropertyName("requirement")] public NumberCompare Requirement { get; set; }
}