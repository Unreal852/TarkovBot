using TarkovRatBot.Core.Caches;
using TarkovRatBot.Core.GraphQL;

namespace TarkovRatBot.Core;

public static class TarkovCore
{
    public static GraphQlQuery ItemsByNameQuery { get; } = new("ItemsByName");
    public static GraphQlQuery ItemsByIdsQuery  { get; } = new("ItemsByIDs");
    public static GraphQlQuery AmmoQuery        { get; } = new("Ammo");
    public static GraphQlQuery CraftQuery       { get; } = new("Crafts");
    public static AmmoCache    AmmoCache        { get; } = new();
    public static CraftsCache  CraftsCache      { get; } = new();

    public static async Task Initialize()
    {
        await AmmoCache.UpdateCache();
        await CraftsCache.UpdateCache();
    }

    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White, string prefix = null)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"[{DateTime.UtcNow:T}] {message}");
        Console.ResetColor();
    }
}