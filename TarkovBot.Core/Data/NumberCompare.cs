using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class NumberCompare
{
    [JsonPropertyName("compareMethod")] public string CompareMethod { get; set; }
    [JsonPropertyName("value")]         public float  Value         { get; set; }
}