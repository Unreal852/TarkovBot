using System.Text.Json.Serialization;
using TarkovBot.Core.GraphQL.Attributes;

namespace TarkovBot.Core.Data;

[GraphQl("ammo")]
public class Ammo
{
    [JsonPropertyName("item")]                public IdOnly  Item                { get; set; }
    [JsonPropertyName("weight")]              public float   Weight              { get; set; }
    [JsonPropertyName("caliber")]             public string? Caliber             { get; set; }
    [JsonPropertyName("stackMaxSize")]        public int     StackMaxSize        { get; set; }
    [JsonPropertyName("tracer")]              public bool    Tracer              { get; set; }
    [JsonPropertyName("tracerColor")]         public string? TracerColor         { get; set; }
    [JsonPropertyName("ammoType")]            public string  AmmoType            { get; set; }
    [JsonPropertyName("projectileCount")]     public int?    ProjectileCount     { get; set; }
    [JsonPropertyName("damage")]              public int     Damage              { get; set; }
    [JsonPropertyName("armorDamage")]         public int     ArmorDamage         { get; set; }
    [JsonPropertyName("fragmentationChance")] public float   FragmentationChance { get; set; }
    [JsonPropertyName("ricochetChance")]      public float   RicochetChance      { get; set; }
    [JsonPropertyName("penetrationChance")]   public float   PenetrationChance   { get; set; }
    [JsonPropertyName("penetrationPower")]    public int     PenetrationPower    { get; set; }
    [JsonPropertyName("accuracy")]            public int     Accuracy            { get; set; }
    [JsonPropertyName("recoil")]              public int?    Recoil              { get; set; }
    [JsonPropertyName("initialSpeed")]        public int     InitialSpeed        { get; set; }
    [JsonPropertyName("lightBleedModifier")]  public float   LightBleedModifier  { get; set; }
    [JsonPropertyName("heavyBleedModifier")]  public float   HeavyBleedModifier  { get; set; }
}