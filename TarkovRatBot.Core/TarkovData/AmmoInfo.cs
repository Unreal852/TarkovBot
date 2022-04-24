using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData;

public class AmmoInfo
{
    [JsonPropertyName("ammoType")]     public string AmmoType     { get; set; }
    [JsonPropertyName("caliber")]      public string Caliber      { get; set; }
    [JsonPropertyName("maxStackSize")] public int?   MaxStackSize { get; set; }
    [JsonPropertyName("damage")]       public int?   Damage       { get; set; }
    [JsonPropertyName("armorDamage")]  public int?   ArmorDamage  { get; set; }

    [JsonPropertyName("penetrationChance")]
    public float? PenetrationChance { get; set; }

    [JsonPropertyName("penetrationPower")] public int?   PenetrationPower { get; set; }
    [JsonPropertyName("ricochetChance")]   public float? RicochetChance   { get; set; }

    [JsonPropertyName("fragmentationChance")]
    public float? FragmentationChance { get; set; }

    [JsonPropertyName("initialSpeed")] public int? InitialSpeed { get; set; }

    [JsonPropertyName("lightBleedModifier")]
    public float? LightBleedModifier { get; set; }

    [JsonPropertyName("heavyBleedModifier")]
    public float? HeavyBleedModifier { get; set; }

    [JsonPropertyName("tracer")] public bool     Tracer { get; set; }
    [JsonPropertyName("item")]   public ItemInfo Item   { get; set; }

    [JsonIgnore] public int RealArmorClassPen      { get; set; }
    [JsonIgnore] public int EffectiveArmorClassPen { get; set; }
}