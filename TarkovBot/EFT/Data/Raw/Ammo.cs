using TarkovBot.Core.GraphQL.Attributes;

namespace TarkovBot.EFT.Data.Raw;

[GraphQl("ammo")]
public class Ammo
{
    public IdOnly  Item                { get; set; }
    public float   Weight              { get; set; }
    public string? Caliber             { get; set; }
    public int     StackMaxSize        { get; set; }
    public bool    Tracer              { get; set; }
    public string? TracerColor         { get; set; }
    public string  AmmoType            { get; set; }
    public int?    ProjectileCount     { get; set; }
    public int     Damage              { get; set; }
    public int     ArmorDamage         { get; set; }
    public float   FragmentationChance { get; set; }
    public float   RicochetChance      { get; set; }
    public float   PenetrationChance   { get; set; }
    public int     PenetrationPower    { get; set; }
    public int     Accuracy            { get; set; }
    public int?    Recoil              { get; set; }
    public int     InitialSpeed        { get; set; }
    public float   LightBleedModifier  { get; set; }
    public float   HeavyBleedModifier  { get; set; }
}