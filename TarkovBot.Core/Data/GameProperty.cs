using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class GameProperty
{
    [JsonPropertyName("key")]          public string    Key          { get; set; }
    [JsonPropertyName("numericValue")] public float?    NumericValue { get; set; }
    [JsonPropertyName("stringValue")]  public string?   StringValue  { get; set; }
    [JsonPropertyName("arrayValue")]   public string[]? ArrayValue   { get; set; }
    [JsonPropertyName("objectValue")]  public string?   ObjectValue  { get; set; }
}