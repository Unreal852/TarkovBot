using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TaskStatusRequirement
{
    [JsonPropertyName("task")]   public Task     Task   { get; set; }
    [JsonPropertyName("status")] public string[] Status { get; set; }
}