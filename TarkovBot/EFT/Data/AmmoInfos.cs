using System.Drawing;
using TarkovBot.EFT.Data.Raw;
using TarkovBot.Extensions;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace TarkovBot.EFT.Data;

public class AmmoInfos : IIdentifiable
{
    public static AmmoInfos FromAmmo(Ammo ammo)
    {
        (int Real, int Effective) armorPenetration = ammo.GetArmorClass();
        return new AmmoInfos
        {
                RealArmorPenetration = armorPenetration.Real,
                EffectiveArmorPenetration = armorPenetration.Effective,
                Ammo = ammo,
                ArmorPenetrationColor = armorPenetration.Effective switch
                {
                        >= 6 => Color.Red,
                        >= 5 => Color.Orange,
                        >= 4 => Color.Purple,
                        >= 3 => Color.Blue,
                        >= 2 => Color.Green,
                        _    => Color.LightGray
                }
        };
    }

    public string Id                        => Ammo.Item.Id;
    public int    RealArmorPenetration      { get; init; }
    public int    EffectiveArmorPenetration { get; init; }
    public Color  ArmorPenetrationColor     { get; init; }
    public Ammo   Ammo                      { get; init; }
}