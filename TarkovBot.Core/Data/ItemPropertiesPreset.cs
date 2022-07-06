using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesPreset
{
    [JsonPropertyName("baseItem")] public Item BaseItem { get; set; }
}