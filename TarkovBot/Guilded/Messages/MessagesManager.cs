using System.Collections.Concurrent;
using Timer = TarkovBot.Misc.Timer;

namespace TarkovBot.Guilded.Messages;

public static class MessagesManager
{
    private static readonly Timer                                     Timer    = new(OnTimerTick, TimeSpan.FromMinutes(1));
    private static readonly ConcurrentDictionary<Guid, IMessageInfos> Messages = new();

    static MessagesManager()
    {
        Timer.Start();
    }

    public static void AddMessage(Guid id, IMessageInfos messageInfos)
    {
        if (Messages.ContainsKey(id))
            return;
        Messages.TryAdd(id, messageInfos);
    }

    public static bool TryTake(Guid id, out IMessageInfos? messageInfos)
    {
        return Messages.TryRemove(id, out messageInfos);
    }

    private static Task OnTimerTick()
    {
        var now = DateTime.Now;
        foreach (KeyValuePair<Guid, IMessageInfos> message in Messages)
        {
            if ((now - message.Value.Message.CreatedAt).TotalMinutes >= 5)
            {
                Messages.TryRemove(message.Key, out _);
            }
        }

        return Task.CompletedTask;
    }
}