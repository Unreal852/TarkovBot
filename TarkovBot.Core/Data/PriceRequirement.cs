using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class PriceRequirement
{
    [JsonPropertyName("type")]        public RequirementType Type        { get; set; }
    [JsonPropertyName("value")]       public int?            Value       { get; set; }
    [JsonPropertyName("stringValue")] public string?         StringValue { get; set; }
}