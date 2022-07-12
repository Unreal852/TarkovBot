namespace TarkovBot.Core;

public class Timer
{
    public Timer(Func<Task> callback, TimeSpan timeSpan)
    {
        Callback = callback;
        PeriodicTimer = new PeriodicTimer(timeSpan);
    }

    private Task?                   TimerTask               { get; set; }
    private PeriodicTimer           PeriodicTimer           { get; }
    private Func<Task>              Callback                { get; }
    private CancellationTokenSource CancellationTokenSource { get; } = new();

    public void Start()
    {
        TimerTask = WaitPeriodic();
    }

    public async Task StopAsync()
    {
        if (TimerTask is null)
            return;
        CancellationTokenSource.Cancel();
        await TimerTask;
        CancellationTokenSource.Dispose();
    }

    private async Task WaitPeriodic()
    {
        try
        {
            while (await PeriodicTimer.WaitForNextTickAsync(CancellationTokenSource.Token))
            {
                await Callback();
            }
        }
        catch (OperationCanceledException ex)
        {
        }
    }
}