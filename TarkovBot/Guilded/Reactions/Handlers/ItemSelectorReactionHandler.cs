using Guilded;
using Guilded.Base.Content;
using Guilded.Base.Events;
using Serilog;
using TarkovBot.EFT.Data;
using TarkovBot.Extensions;
using TarkovBot.Guilded.Messages;
using TarkovBot.Guilded.Messages.Implementations;

namespace TarkovBot.Guilded.Reactions.Handlers;

public class ItemSelectorReactionHandler : IMessageReactionHandler
{
    public async Task OnMessageReactionAdded(GuildedBotClient bot, MessageReactionEvent e)
    {
        if (MessagesManager.TryTake(e.MessageId, out IMessageInfos? messageInfos) && messageInfos is ItemsMessageSelector messageSelector)
        {
            int index = e.Emote.ToSelectorIndex();
            if (index >= messageSelector.Items.Length)
            {
                Log.Error("Message reaction was out of bounds {Value}", index);
                return;
            }

            ItemInfos item = messageSelector.Items[index];
            MessageContent messageContent = item.BuildMessageContent(item.Language);
            messageContent.ReplyMessageIds = messageSelector.Message.ReplyMessageIds;
            Message message = await bot.CreateMessageAsync(e.ChannelId, messageContent);
            await message.AppendReactions(item);

            await messageSelector.Message.DeleteAsync();
        }
    }
}