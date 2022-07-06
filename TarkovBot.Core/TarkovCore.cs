using TarkovBot.Core.Providers;

namespace TarkovBot.Core;

public static class TarkovCore
{
    public static AmmoProvider AmmoProvider { get; } = new();

    public static ItemsProvider ItemsProvider { get; } = new();
    //public static CraftsProvider          CraftsProvider          { get; } = new();
    //public static HideoutStationsProvider HideoutStationsProvider { get; } = new();

    public static async Task Initialize()
    {
        await AmmoProvider.UpdateCache();
        // await CraftsProvider.UpdateCache();
        // await HideoutStationsProvider.UpdateCache();
    }

    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"[{DateTime.UtcNow:T}] {message}");
        Console.ResetColor();
    }
}