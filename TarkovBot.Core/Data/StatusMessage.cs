using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class StatusMessage
{
    [JsonPropertyName("content")]    public string  Content    { get; set; }
    [JsonPropertyName("time")]       public string  Time       { get; set; }
    [JsonPropertyName("type")]       public int     Type       { get; set; }
    [JsonPropertyName("solveTime")]  public string? SolveTime  { get; set; }
    [JsonPropertyName("statusCode")] public string  StatusCode { get; set; }
}