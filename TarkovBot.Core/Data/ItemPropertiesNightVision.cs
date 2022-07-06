using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesNightVision
{
    [JsonPropertyName("intensity")]        public float? Intensity        { get; set; }
    [JsonPropertyName("noiseIntensity")]   public float? NoiseIntensity   { get; set; }
    [JsonPropertyName("noiseScale")]       public float? NoiseScale       { get; set; }
    [JsonPropertyName("diffuseIntensity")] public float? DiffuseIntensity { get; set; }
}