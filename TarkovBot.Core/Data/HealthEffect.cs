using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class HealthEffect
{
    [JsonPropertyName("bodyParts")] public string[]       BodyParts { get; set; }
    [JsonPropertyName("effects")]   public string[]       Effects   { get; set; }
    [JsonPropertyName("time")]      public NumberCompare? Time      { get; set; }
}