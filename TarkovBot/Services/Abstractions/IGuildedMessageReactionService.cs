namespace TarkovBot.Services.Abstractions;

public interface IGuildedMessageReactionService
{
    public bool TryAdd(MessageData messageData);
    public bool TryGet(Guid messageId, out MessageData? messageData);
    public bool TryRemove(Guid messageId, out MessageData? messageData);
}