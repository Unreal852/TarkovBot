using System.Text.Json.Serialization;

namespace TarkovRatBot.Tarkov;

public class JsonData
{
    [JsonPropertyName("itemsByName")] public ItemInfo[] ItemsByName { get; set; }
}