using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class QuestItem
{
    [JsonPropertyName("id")]   public string? Id   { get; set; }
    [JsonPropertyName("name")] public string  Name { get; set; }
}