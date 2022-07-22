using TarkovBot.EFT.Data;
using TarkovBot.EFT.Data.Provider;

namespace TarkovBot.Extensions;

/// <summary>
/// Extensions for the <see cref="IdOnly"/> type.
/// </summary>
public static class IdOnlyExtensions
{
    public static ItemInfos? GetItemInfos(this IdOnly idOnly)
    {
        return DataProviders.ItemsProvider.GetByKey(idOnly.Id);
    }

    public static AmmoInfos? GetAmmoInfos(this IdOnly idOnly)
    {
        return DataProviders.AmmoProvider.GetByKey(idOnly.Id);
    }
}