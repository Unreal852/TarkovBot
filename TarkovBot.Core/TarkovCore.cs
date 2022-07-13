using TarkovBot.Core.Providers.Implementations;

namespace TarkovBot.Core;

public static class TarkovCore
{
    private static Timer         Timer         { get; } = new(OnTimerTick, TimeSpan.FromHours(2.0));
    public static  AmmoProvider  AmmoProvider  { get; } = new();
    public static  ItemsProvider ItemsProvider { get; } = new();
    public static  TasksProvider TasksProvider { get; } = new();

    public static async Task Initialize()
    {
        await UpdateCaches();
        Timer.Start();
    }

    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"[{DateTime.UtcNow:T}] {message}");
        Console.ResetColor();
    }

    private static async Task UpdateCaches()
    {
        WriteLine("Updating providers cache...", ConsoleColor.Yellow);
        await AmmoProvider.UpdateCache().ConfigureAwait(false);
        await ItemsProvider.UpdateCache().ConfigureAwait(false);
        await TasksProvider.UpdateCache().ConfigureAwait(false);
    }

    private static Task OnTimerTick()
    {
        return UpdateCaches();
    }
}