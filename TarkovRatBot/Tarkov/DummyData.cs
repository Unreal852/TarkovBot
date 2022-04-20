using System.Text.Json.Serialization;

namespace TarkovRatBot.Tarkov;

public class DummyData
{
    [JsonPropertyName("data")] public JsonData Data { get; set; }
}