using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TaskKey
{
    [JsonPropertyName("keys")] public Item[] Keys { get; set; }
    [JsonPropertyName("map")]  public Map?   Map  { get; set; }
}