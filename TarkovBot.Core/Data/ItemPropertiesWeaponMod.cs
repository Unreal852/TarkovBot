using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemPropertiesWeaponMod
{
    [JsonPropertyName("ergonomics")] public float? Ergonomics { get; set; }
    [JsonPropertyName("recoil")]     public float? Recoil     { get; set; }
}