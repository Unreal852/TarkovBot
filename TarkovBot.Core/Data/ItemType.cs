using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public enum ItemType
{
    [JsonPropertyName("ammo")]       Ammo,
    [JsonPropertyName("ammoBox")]    AmmoBox,
    [JsonPropertyName("any")]        Any,
    [JsonPropertyName("armor")]      Armor,
    [JsonPropertyName("backpack")]   Backpack,
    [JsonPropertyName("barter")]     Barter,
    [JsonPropertyName("container")]  Container,
    [JsonPropertyName("glasses")]    Glasses,
    [JsonPropertyName("grenade")]    Grenade,
    [JsonPropertyName("gun")]        Gun,
    [JsonPropertyName("headphones")] Headphones,
    [JsonPropertyName("helmet")]     Helmet,
    [JsonPropertyName("injectors")]  Injectors,
    [JsonPropertyName("keys")]       Keys,
    [JsonPropertyName("markedOnly")] MarkedOnly,
    [JsonPropertyName("meds")]       Meds,
    [JsonPropertyName("mods")]       Mods,
    [JsonPropertyName("noFlea")]     NoFlea,
    [JsonPropertyName("pistolGrip")] PistolGrip,
    [JsonPropertyName("preset")]     Preset,
    [JsonPropertyName("provisions")] Provisions,
    [JsonPropertyName("rig")]        Rig,
    [JsonPropertyName("suppressor")] Suppressor,
    [JsonPropertyName("wearable")]   Wearable,
}