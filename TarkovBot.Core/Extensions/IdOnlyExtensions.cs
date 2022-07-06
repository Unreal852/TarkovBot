using TarkovBot.Core.TarkovData;
using TarkovBot.Core.TarkovData.Items;

namespace TarkovBot.Core.Extensions;

public static class IdOnlyExtensions
{
    public static async Task<Item?> GetItemAsync(this IdOnly idOnly)
    {
        return (await TarkovCore.ItemsQuery.ExecuteAs<Item[]>($"id: {idOnly.Id}"))?.FirstOrDefault();
    }
}