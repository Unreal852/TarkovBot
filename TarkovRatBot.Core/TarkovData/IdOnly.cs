using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData;

public class IdOnly
{
    [JsonPropertyName("id")] public string Id { get; set; }
}