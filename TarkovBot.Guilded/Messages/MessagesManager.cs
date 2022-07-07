using System.Collections.Concurrent;

namespace TarkovBot.Guilded.Messages;

public class MessagesManager
{
    public MessagesManager(GuildedBot bot)
    {
        Bot = bot;
        OnTimerTick();
    }

    public  GuildedBot                                Bot           { get; }
    private PeriodicTimer                             PeriodicTimer { get; } = new(TimeSpan.FromMinutes(1));
    private ConcurrentDictionary<Guid, IMessageInfos> Messages      { get; } = new();

    public void AddMessage(Guid id, IMessageInfos messageInfos)
    {
        if (Messages.ContainsKey(id))
            return;
        Messages.TryAdd(id, messageInfos);
    }

    public bool TryTake(Guid id, out IMessageInfos messageInfos)
    {
        return Messages.TryRemove(id, out messageInfos);
    }

    private async void OnTimerTick()
    {
        while (await PeriodicTimer.WaitForNextTickAsync())
        {
            var now = DateTime.Now;
            foreach (KeyValuePair<Guid, IMessageInfos> message in Messages)
            {
                if ((now - message.Value.Message.CreatedAt).TotalMinutes >= 5)
                {
                    Messages.TryRemove(message.Key, out _);
                }
            }
        }
    }
}