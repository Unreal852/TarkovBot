using System.Text.Json.Serialization;

namespace TarkovBot.Core.TarkovData;

public class Vendor
{
    [JsonPropertyName("name")] public string Name { get; set; }
}