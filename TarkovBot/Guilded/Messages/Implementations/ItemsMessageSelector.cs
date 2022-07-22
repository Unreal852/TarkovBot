using Guilded.Base.Content;
using TarkovBot.EFT.Data;
using TarkovBot.EFT.Data.Raw;

namespace TarkovBot.Guilded.Messages.Implementations;

public class ItemsMessageSelector : IMessageInfos
{
    public ItemsMessageSelector(LanguageCode languageCode, Message commandMessage, Message message, ItemInfos[] items)
    {
        Language = languageCode;
        CommandMessage = commandMessage;
        Message = message;
        Items = items;
    }

    public LanguageCode Language       { get; }
    public Message      CommandMessage { get; }
    public Message      Message        { get; }
    public ItemInfos[]  Items          { get; }
}