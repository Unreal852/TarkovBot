using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData;

public enum ItemSourceName
{
    [JsonPropertyName("prapor")]      Prapor,
    [JsonPropertyName("therapist")]   Therapist,
    [JsonPropertyName("fence")]       Fence,
    [JsonPropertyName("skier")]       Skier,
    [JsonPropertyName("peacekeeper")] Peacekeeper,
    [JsonPropertyName("mechanic")]    Mechanic,
    [JsonPropertyName("ragman")]      Ragman,
    [JsonPropertyName("jaeger")]      Jaeger,
    [JsonPropertyName("fleaMarket")]  FleaMarket
}