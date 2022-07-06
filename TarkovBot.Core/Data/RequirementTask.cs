using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class RequirementTask
{
    [JsonPropertyName("id")]   public string? Id   { get; set; }
    [JsonPropertyName("task")] public Task    Task { get; set; }
}