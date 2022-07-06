using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public enum LanguageCode
{
    [JsonPropertyName("cz")] cz,
    [JsonPropertyName("de")] de,
    [JsonPropertyName("en")] en,
    [JsonPropertyName("es")] es,
    [JsonPropertyName("fr")] fr,
    [JsonPropertyName("hu")] hu,
    [JsonPropertyName("ru")] ru,
    [JsonPropertyName("tr")] tr,
    [JsonPropertyName("zh")] zh,
}