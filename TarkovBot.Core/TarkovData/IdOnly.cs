using System.Text.Json.Serialization;

namespace TarkovBot.Core.TarkovData;

public class IdOnly
{
    [JsonPropertyName("id")] public string Id { get; set; }
}