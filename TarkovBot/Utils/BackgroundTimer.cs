namespace TarkovBot.Utils;

public class BackgroundTimer
{
    private          Task?                   _timerTask;
    private readonly Action?                 _callback;
    private readonly Func<Task>?             _asyncCallback;
    private readonly PeriodicTimer           _timer;
    private readonly CancellationTokenSource _cts = new();

    private BackgroundTimer(TimeSpan interval)
    {
        Interval = interval;
        _timer = new PeriodicTimer(interval);
    }

    public BackgroundTimer(TimeSpan interval, Action callback) : this(interval)
    {
        _callback = callback;
    }

    public BackgroundTimer(TimeSpan interval, Func<Task> asyncCallback) : this(interval)
    {
        _asyncCallback = asyncCallback;
    }

    public TimeSpan Interval { get; }

    public void Start(bool runCallback = false)
    {
        if (_timerTask != null)
            return;
        _timerTask = WorkerAsync(runCallback);
    }

    public async Task StopAsync()
    {
        if (_timerTask == null)
            return;
        _cts.Cancel();
        await _timerTask;
        _cts.Dispose();
    }

    private async Task WorkerAsync(bool runCallback)
    {
        try
        {
            if (runCallback)
            {
                if (_asyncCallback != null)
                    await _asyncCallback();
                else
                    _callback?.Invoke();
            }

            while (await _timer.WaitForNextTickAsync(_cts.Token))
            {
                if (_asyncCallback != null)
                    await _asyncCallback();
                else
                    _callback?.Invoke();
            }
        }
        catch (OperationCanceledException)
        {
        }
    }
}