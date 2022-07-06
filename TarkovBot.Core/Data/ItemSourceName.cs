using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public enum ItemSourceName
{
    [JsonPropertyName("prapor")]      prapor,
    [JsonPropertyName("therapist")]   therapist,
    [JsonPropertyName("fence")]       fence,
    [JsonPropertyName("skier")]       skier,
    [JsonPropertyName("peacekeeper")] peacekeeper,
    [JsonPropertyName("mechanic")]    mechanic,
    [JsonPropertyName("ragman")]      ragman,
    [JsonPropertyName("jaeger")]      jaeger,
    [JsonPropertyName("fleaMarket")]  fleaMarket,
}