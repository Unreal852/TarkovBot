using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Core.TarkovData.Items;

namespace TarkovRatBot.Core.Extensions;

public static class IdOnlyExtensions
{
    public static async Task<Item?> GetItemAsync(this IdOnly idOnly)
    {
        return (await TarkovCore.ItemsQuery.ExecuteAs<Item[]>($"id: {idOnly.Id}"))?.FirstOrDefault();
    }
}