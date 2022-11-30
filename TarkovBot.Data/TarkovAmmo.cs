using System.Text.Json.Serialization;

namespace TarkovBot.Data;

public class TarkovAmmo : IJsonOnDeserialized
{
    public TarkovItemId Item                  { get; set; } = default!;
    public int          Damage                { get; set; }
    public int          ArmorDamage           { get; set; }
    public int          RealAgainstArmor      { get; set; }
    public int          EffectiveAgainstArmor { get; set; }
    public int          PenetrationPower      { get; set; }
    public float        PenetrationChance     { get; set; }

    public void OnDeserialized()
    {
    }
}