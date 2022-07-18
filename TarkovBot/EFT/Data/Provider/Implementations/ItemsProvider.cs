using TarkovBot.EFT.Data.Raw;
using TarkovBot.GraphQL;

namespace TarkovBot.EFT.Data.Provider.Implementations;

public class ItemsProvider : LocalizedDataProvider<Item>
{
    public ItemsProvider() : base(GraphQlQueryBuilder.FromType<Item>()!)
    {
    }
}