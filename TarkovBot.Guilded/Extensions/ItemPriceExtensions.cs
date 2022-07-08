using TarkovBot.Core.Data;

namespace TarkovBot.Guilded.Extensions;

public static class ItemPriceExtensions
{
    public static char GetCurrencyChar(this ItemPrice price)
    {
        return price.Currency switch
        {
                "RUB" => '₽',
                "USD" => '$',
                "EUR" => '€',
        };
    }
}