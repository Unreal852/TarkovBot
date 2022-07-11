using System.Drawing;
using TarkovBot.Core.Data;

namespace TarkovBot.Core.Extensions;

public static class AmmoExtensions
{
    public static (int Real, int Effective) GetArmorClass(this Ammo ammoInfo)
    {
        return ammoInfo.PenetrationPower switch
        {
                >= 70 => (7, 7),
                >= 67 => (6, 7),
                >= 60 => (6, 6),
                >= 57 => (5, 6),
                >= 50 => (5, 5),
                >= 47 => (4, 5),
                >= 40 => (4, 4),
                >= 37 => (3, 4),
                >= 30 => (3, 3),
                >= 27 => (2, 3),
                >= 20 => (2, 2),
                >= 17 => (1, 2),
                >= 10 => (1, 1),
                >= 7  => (0, 1),
                _     => (0, 0)
        };
    }

    public static Color GetPenetrationClassColor(this Ammo ammoInfo)
    {
        return GetPenetrationClassColor(ammoInfo.GetArmorClass().Effective);
    }

    public static Color GetPenetrationClassColor(int effective)
    {
        return effective switch
        {
                >= 6 => Color.Red,
                >= 5 => Color.Orange,
                >= 4 => Color.Purple,
                >= 3 => Color.Blue,
                >= 2 => Color.Green,
                _    => Color.LightGray
        };
    }
}