using TarkovBot.Core.Caches;
using TarkovBot.Core.GraphQL;
using TarkovBot.Core.TarkovData;
using TarkovBot.Core.TarkovData.Ammos;
using TarkovBot.Core.TarkovData.Crafts;
using TarkovBot.Core.TarkovData.Items;

namespace TarkovBot.Core;

public static class TarkovCore
{
    public static GraphQlQuery         AmmoQuery            { get; } = GraphQlQueryBuilder.BuildQuery<Ammo>()!;
    public static GraphQlQuery         CraftsQuery          { get; } = GraphQlQueryBuilder.BuildQuery<Craft>()!;
    public static GraphQlQuery         ItemsQuery           { get; } = GraphQlQueryBuilder.BuildQuery<Item>()!;
    public static GraphQlQuery         HideoutStationQuery  { get; } = GraphQlQueryBuilder.BuildQuery<HideoutStation>()!;
    public static AmmoCache            AmmoCache            { get; } = new();
    public static CraftsCache          CraftsCache          { get; } = new();
    public static HideoutStationsCache HideoutStationsCache { get; } = new();

    public static async Task Initialize()
    {
        await AmmoCache.UpdateCache();
        await CraftsCache.UpdateCache();
        await HideoutStationsCache.UpdateCache();
    }

    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"[{DateTime.UtcNow:T}] {message}");
        Console.ResetColor();
    }
}