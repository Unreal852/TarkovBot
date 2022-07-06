using System.Text.Json.Serialization;
using TarkovBot.Core.GraphQL.Attributes;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovBot.Core.TarkovData.Ammos;

[GraphQl("ammo")]
public class Ammo
{
    [JsonPropertyName("ammoType")]            public string  AmmoType               { get; set; }
    [JsonPropertyName("caliber")]             public string? Caliber                { get; set; }
    [JsonPropertyName("tracerColor")]         public string? TracerColor            { get; set; }
    [JsonPropertyName("damage")]              public int     Damages                { get; set; }
    [JsonPropertyName("armorDamage")]         public int     ArmorDamages           { get; set; }
    [JsonPropertyName("initialSpeed")]        public int     InitialSpeed           { get; set; }
    [JsonPropertyName("recoil")]              public int?    Recoil                 { get; set; }
    [JsonPropertyName("accuracy")]            public int     Accuracy               { get; set; }
    [JsonPropertyName("projectileCount")]     public int?    ProjectilesCount       { get; set; }
    [JsonPropertyName("stackMaxSize")]        public int     StackMaxSize           { get; set; }
    [JsonPropertyName("penetrationPower")]    public int     PenetrationPower       { get; set; }
    [JsonPropertyName("weight")]              public float   Weight                 { get; set; }
    [JsonPropertyName("penetrationChance")]   public float   PenetrationChance      { get; set; }
    [JsonPropertyName("fragmentationChance")] public float   FragmentationChance    { get; set; }
    [JsonPropertyName("ricochetChance")]      public float   RicochetChance         { get; set; }
    [JsonPropertyName("lightBleedModifier")]  public float   LightBleedModifier     { get; set; }
    [JsonPropertyName("heavyBleedModifier")]  public float   HeavyBleedModifier     { get; set; }
    [JsonPropertyName("tracer")]              public bool    Tracer                 { get; set; }
    [JsonPropertyName("item")]                public IdOnly  Item                   { get; set; }
    [JsonIgnore]                              public int     RealArmorClassPen      { get; set; }
    [JsonIgnore]                              public int     EffectiveArmorClassPen { get; set; }
}