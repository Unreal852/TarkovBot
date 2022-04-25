using TarkovRatBot.Core.TarkovData.Ammos;

namespace TarkovRatBot.Core.Extensions;

public static class AmmoInfoExtensions
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
}