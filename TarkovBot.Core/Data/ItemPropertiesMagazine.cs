using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesMagazine
{
    [JsonPropertyName("ergonomics")]        public float? Ergonomics        { get; set; }
    [JsonPropertyName("recoil")]            public float? Recoil            { get; set; }
    [JsonPropertyName("capacity")]          public int?   Capacity          { get; set; }
    [JsonPropertyName("loadModifier")]      public float? LoadModifier      { get; set; }
    [JsonPropertyName("ammoCheckModifier")] public float? AmmoCheckModifier { get; set; }
    [JsonPropertyName("malfunctionChance")] public float? MalfunctionChance { get; set; }
}