using Guilded.Base.Content;
using TarkovBot.EFT.Data;

namespace TarkovBot.Guilded.Messages.Implementations;

public class ItemsMessageSelector : IMessageInfos
{
    public ItemsMessageSelector(Message commandMessage, Message message, ItemInfos[] items)
    {
        CommandMessage = commandMessage;
        Message = message;
        Items = items;
    }

    public Message     CommandMessage { get; }
    public Message     Message        { get; }
    public ItemInfos[] Items          { get; }
}