using Guilded.Base.Content;
using TarkovBot.Core.Data;

namespace TarkovBot.Guilded.Messages.Implementations;

public class ItemsMessageSelector : IMessageInfos
{
    public ItemsMessageSelector(Message commandMessage, Message message, Item[] items)
    {
        CommandMessage = commandMessage;
        Message = message;
        Items = items;
    }

    public Message CommandMessage { get; }
    public Message Message        { get; }
    public Item[]  Items          { get; }
}