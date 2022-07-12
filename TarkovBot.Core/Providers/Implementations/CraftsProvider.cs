using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers.Implementations;

public class CraftsProvider : LocalizedDataProvider<Craft>
{
    public CraftsProvider() : base(GraphQlQueryBuilder.BuildQuery<Craft>()!)
    {
    }
}