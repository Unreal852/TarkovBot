using TarkovBot.EFT.Data;
using TarkovBot.EFT.Data.Provider;
using TarkovBot.EFT.Data.Raw;

namespace TarkovBot.Extensions;

/// <summary>
/// Extensions for the <see cref="IdOnly"/> type.
/// </summary>
public static class IdOnlyExtensions
{
    public static ItemInfos? GetItemInfos(this IdOnly idOnly, LanguageCode languageCode)
    {
        return DataProviders.ItemsProvider.GetByKey(languageCode, idOnly.Id);
    }

    public static AmmoInfos? GetAmmoInfos(this IdOnly idOnly, LanguageCode languageCode)
    {
        return DataProviders.AmmoProvider.GetByKey(idOnly.Id);
    }
}