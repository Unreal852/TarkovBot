using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers.Implementations;

public class ItemsProvider : LocalizedDataProvider<Item>
{
    public ItemsProvider() : base(GraphQlQueryBuilder.BuildQuery<Item>()!)
    {
    }
}