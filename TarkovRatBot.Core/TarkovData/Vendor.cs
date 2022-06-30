using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData;

public class Vendor
{
    [JsonPropertyName("name")] public string Name { get; set; }
}