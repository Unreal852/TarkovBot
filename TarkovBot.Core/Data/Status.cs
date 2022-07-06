using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class Status
{
    [JsonPropertyName("name")]       public string  Name       { get; set; }
    [JsonPropertyName("message")]    public string? Message    { get; set; }
    [JsonPropertyName("status")]     public int     Statuss    { get; set; }
    [JsonPropertyName("statusCode")] public string  StatusCode { get; set; }
}