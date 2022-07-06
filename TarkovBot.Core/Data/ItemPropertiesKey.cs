using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesKey
{
    [JsonPropertyName("uses")] public int? Uses { get; set; }
}