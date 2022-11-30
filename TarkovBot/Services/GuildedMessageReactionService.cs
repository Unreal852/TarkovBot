using System.Collections.Concurrent;
using Guilded.Content;
using Guilded.Events;
using TarkovBot.Services.Abstractions;
using TarkovBot.Utils;

namespace TarkovBot.Services;

public class GuildedMessageReactionService : IGuildedMessageReactionService
{
    private readonly ConcurrentDictionary<Guid, MessageData> _storedMessages = new();
    private readonly BackgroundTimer                         _timer;

    public GuildedMessageReactionService()
    {
        _timer = new BackgroundTimer(TimeSpan.FromMinutes(1), OnTaskTick);
        _timer.Start();
    }

    public bool TryAdd(MessageData messageData)
    {
        return _storedMessages.TryAdd(messageData.Message.Id, messageData);
    }

    public bool TryGet(Guid messageId, out MessageData? messageData)
    {
        return _storedMessages.TryGetValue(messageId, out messageData);
    }

    public bool TryRemove(Guid messageId, out MessageData? messageData)
    {
        return _storedMessages.TryRemove(messageId, out messageData);
    }

    private void OnTaskTick()
    {
        var now = DateTime.Now;
        foreach (var storedMessage in _storedMessages)
        {
            if ((now - storedMessage.Value.Message.CreatedAt).TotalMinutes >= 1)
            {
                _storedMessages.TryRemove(storedMessage);
            }
        }
    }
}

public class MessageData
{
    public required Message                                   Message  { get; init; }
    public required Action<MessageReactionEvent, MessageData> Callback { get; init; }
    public          object?                                   Data     { get; set; }
}