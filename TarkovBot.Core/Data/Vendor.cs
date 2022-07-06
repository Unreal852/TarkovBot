using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class Vendor
{
    [JsonPropertyName("name")] public string Name { get; set; }
}