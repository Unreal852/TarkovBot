using Serilog;
using SerilogTimings;
using TarkovBot.EFT.Data.Provider.Implementations;
using Timer = TarkovBot.Misc.Timer;

// ReSharper disable MemberCanBePrivate.Global

namespace TarkovBot.EFT.Data.Provider;

public static class DataProviders
{
    private static Timer           Timer         { get; } = new(OnTimerTick, TimeSpan.FromHours(2.0));
    public static  AmmoProvider    AmmoProvider  { get; } = new();
    public static  ItemsProviderV2 ItemsProvider { get; } = new();
    public static  TasksProvider   TasksProvider { get; } = new();

    public static async Task Initialize()
    {
        await UpdateCaches().ConfigureAwait(false);
        Timer.Start();
    }

    private static async Task UpdateCaches()
    {
        Log.Information("Updating providers cache");
        using (Operation.Time("Updated data providers cache"))
        {
            await AmmoProvider.UpdateCache().ConfigureAwait(false);
            await ItemsProvider.UpdateCache().ConfigureAwait(false);
            await TasksProvider.UpdateCache().ConfigureAwait(false);
        }
    }

    private static Task OnTimerTick()
    {
        return UpdateCaches();
    }
}