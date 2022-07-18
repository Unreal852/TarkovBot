using TarkovBot.EFT.Data;
using TarkovBot.EFT.Data.Raw;

namespace TarkovBot.Extensions;

/// <summary>
/// Extensions for the <see cref="ItemPrice"/> type.
/// </summary>
public static class ItemPriceExtensions
{
    public static char GetCurrencyChar(this ItemPrice price)
    {
        return price.Currency switch
        {
                "RUB" => CurrencyConstants.Rub,
                "USD" => CurrencyConstants.Usd,
                "EUR" => CurrencyConstants.Eur,
                _     => throw new ArgumentOutOfRangeException()
        };
    }
}