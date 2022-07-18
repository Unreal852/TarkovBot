using Serilog;
using TarkovBot.EFT.Data.Raw;
using TarkovBot.GraphQL;

namespace TarkovBot.EFT.Data.Provider.Implementations;

public class AmmoProvider : DataProvider<string, AmmoInfos>
{
    public AmmoProvider() : base(GraphQlQueryBuilder.FromType<Ammo>()!)
    {
    }

    public override async Task<int> UpdateCache()
    {
        Ammo[]? ammos = await Query.ExecuteAs<Ammo[]>("lang: en");
        if (ammos == null || ammos.Length == 0)
        {
            Log.Error("Failed to cache {Type}, the results returned where null or empty", nameof(Ammo));
            return 0;
        }

        Cache.Clear();
        foreach (Ammo ammo in ammos)
            Cache.TryAdd(ammo.Item.Id, AmmoInfos.FromAmmo(ammo));

        return Count;
    }
}