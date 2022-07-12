using Guilded;
using Guilded.Base.Content;
using Guilded.Base.Events;
using TarkovBot.Core;
using TarkovBot.Core.Data;
using TarkovBot.Core.Extensions;
using TarkovBot.Guilded.Extensions;
using TarkovBot.Guilded.Messages;
using TarkovBot.Guilded.Messages.Implementations;
using Task = System.Threading.Tasks.Task;

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
                TarkovCore.WriteLine("Message reaction out of bounds.", ConsoleColor.Red);
                return;
            }

            Item item = messageSelector.Items[index];
            MessageContent messageContent = item.BuildMessageContent();
            messageContent.ReplyMessageIds = messageSelector.Message.ReplyMessageIds;
            Message message = await bot.CreateMessageAsync(e.ChannelId, messageContent);
            if (item.IsAmmo())
                await message.AddReactionAsync(Constants.EmoteLargeRedSquare);

            await messageSelector.Message.DeleteAsync();
        }
    }
}