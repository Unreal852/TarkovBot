using System.Text.Json.Serialization;

namespace TarkovBot.Core.TarkovData;

public class ItemAttribute
{
    [JsonPropertyName("type")]  public string Type  { get; set; }
    [JsonPropertyName("value")] public string Value { get; set; }
}