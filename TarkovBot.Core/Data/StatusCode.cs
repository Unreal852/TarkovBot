using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public enum StatusCode
{
    [JsonPropertyName("OK")]       OK,
    [JsonPropertyName("Updating")] Updating,
    [JsonPropertyName("Unstable")] Unstable,
    [JsonPropertyName("Down")]     Down,
}